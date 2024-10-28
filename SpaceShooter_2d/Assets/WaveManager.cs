using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // this script manages the waves. 
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
        while (!isWaveCompleted && currentWave.waveTimer > 0)
        {
            print("here in I run WaveTimer");
            currentWave.waveTimer -= 1f;
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
        while (waveCoolDownTime > 0 && isWaveCompleted)
        {
            waveCoolDownTime -= 1f;
           
            yield return new WaitForSeconds(1f);
        }
        isWaveCompleted = false;
        yield return StartCoroutine(IRunWaveTimer());
    }


}
