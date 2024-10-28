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
        StopCoroutine(ISpawnAliens());
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
        StartCoroutine(IRunWaveTimer());
    }
    IEnumerator IRunWaveTimer()
    {
        remainingWaveTime = currentWave.waveTimer;
        while (!isWaveCompleted && currentWave.waveTimer > 0)
        {
            print("here in I run WaveTimer");
            currentWave.waveTimer -= 1f;
            remainingWaveTime = currentWave.waveTimer;
            yield return new WaitForSeconds(1f); // Waits until the next frame
        }

        // If you need to take any action when the wave timer ends
        if (currentWave.waveTimer <= 0)
        {
            if (GameManager.instance.PlayerLife > 0)
            {
                isWaveCompleted = true;
                print("is wavecompleted and " + currentWave.waveTimer);
                StartCoroutine(ICoolDownForNextWave());

            }
        }
    }
    IEnumerator ICoolDownForNextWave()
    {
      
            while (waveCoolDownTime > 0)
            {
                waveCoolDownTime -= 1f;

                yield return new WaitForSeconds(1f);
            }
        isWaveCompleted = false;
        yield return StartCoroutine(ISpawnAliens());
        yield return StartCoroutine(IRunWaveTimer());
    }
    #endregion
}
