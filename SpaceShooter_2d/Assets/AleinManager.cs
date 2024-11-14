using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AleinManager : MonoBehaviour
{
    public GameObject alienPrefab, alien_intermediatePrefab;
    public List<GameObject> alienList = new List<GameObject>();
    //public WaveManager waveManager;

    public Transform spawnPoint;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    //for debugging purpose
    [SerializeField] float remainingWaveTime;

    // Start is called before the first frame update
    public void StartGame(int enemyCount, AlienType alienType)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject alien = Instantiate(alienPrefab, transform);
            alien.SetActive(false);
            alienList.Add(alien);
        }
        StartCoroutine(ICoolDownForNextWave());
        // IncreaseWaves();

    }

    //this is to get aliens from the pool of aliens which were instantiated at the start 
    GameObject GetAliensFromPool()
    {
        return alienList.Find(x => !x.activeSelf);
    }
    //This is for spawning aliens
    IEnumerator ISpawnAliens()
    {
        while (!GameManager.instance.isGameOver)
        {
            // Check if the number of active aliens is less than 3
            int activeAliens = alienList.Count(x => x.activeSelf);
            if (activeAliens < activeEnemyCounter)
            {
                GameObject alien = GetAliensFromPool();
                if (alien != null)
                {
                    alien.SetActive(true);
                    alien.transform.position = new Vector2(spawnPoint.position.x, Random.Range(minHeight, maxHeight));
                }
            }
            yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        }
        yield break;
    }
    bool AllAliensAreDead()
    {
        int activeAliens = alienList.Count(x => x.activeSelf);
        print(activeAliens + " is active aliens");
        return activeAliens == 0;
    }



    #region wavesmanger
    public List<Wave> waves;
    public int activeEnemyCounter;
    private int waveCount;
    public int WaveCount
    {
        get
        {
            return waveCount;
        }
        set
        {
            waveCount = value;

        }
    }
    public int prevWave, nextWave;
    public float waveCoolDownTime = 5f;
    public bool isWaveCompleted;
    int index;
    public Wave currentWave;

    public void IncreaseWaves()
    {
        Time.timeScale = 1;
        if (waveCount == 0)
        {
            WaveCount = 1;
            index = 0;
            print(index + " is index and " + waves.Count);
        }
        if (isWaveCompleted)
        {
            WaveCount++;
            index = WaveCount - 1;
        }
        if (index < waves.Count)
        {
            print(index + " is index and " + waves.Count);
            print(" here in current wave");
            currentWave = waves[index];

        }
        else
        {
            Debug.Log("All waves completed. No more waves to load.");
        }
        activeEnemyCounter = currentWave.activeEnemyCounter;

        PlayerPrefs.SetInt("LastWave", waveCount);
        //StartCoroutine(IRunWaveTimer());
    }
    IEnumerator IRunWaveTimer()
    {
        remainingWaveTime = currentWave.waveTimer;
        Debug.Log("Current wave: " + remainingWaveTime);
        Debug.Log(AllAliensAreDead() + " all aliens are dead in wave timer");
        while (AllAliensAreDead()==false)
        {
            yield return null;
        }
        while (!isWaveCompleted && remainingWaveTime > 0)
        {
            print("here in I run WaveTimer");
            remainingWaveTime -= 1f;
            print("timer running : " + remainingWaveTime);
            //remainingWaveTime = currentWave.waveTimer;
            yield return new WaitForSeconds(1f); // Waits until the next second
        }

        // If you need to take any action when the wave timer ends
        if (remainingWaveTime <= 0)
        {
            if (GameManager.instance.PlayerLife > 0)
            {
                isWaveCompleted = true;
                print("is wavecompleted and " + currentWave.waveTimer);
                StartCoroutine(ICoolDownForNextWave());

            }
            else
            {
                GameManager.instance.isGameOver = true;
            }
        }
        yield return StartCoroutine(ISpawnAliens());
    }
    IEnumerator ICoolDownForNextWave()
    {
            Debug.Log(" in cooldownwave timer");
        StopCoroutine(ISpawnAliens());
        while (waveCoolDownTime > 0)
        {
            waveCoolDownTime -= 1f;
            yield return new WaitForSeconds(1f);
        }
        isWaveCompleted = false;
        waveCoolDownTime = 5f;
        StartCoroutine(ISpawnAliens());
        yield return StartCoroutine(IRunWaveTimer());
    }
    #endregion
}
