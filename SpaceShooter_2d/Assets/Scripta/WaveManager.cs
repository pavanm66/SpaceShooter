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
            WaveCount = 0;
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
        Debug.Log(currentWave.waveTimer + " is current wave timer and current wave is " + currentWave.waveIndex);
        while (!isWaveCompleted && currentWave.waveTimer > 0)
        {
            print("here in I run WaveTimer");
            currentWave.waveTimer -= 1f;
            yield return new WaitForSeconds(1f); 
        }

        // If you need to take any action when the wave timer ends
        if (currentWave.waveTimer <= 0)
        {
            if (GameManager.instance.PlayerLife > 0)
            {
                isWaveCompleted = true;
                IncreaseWaves();
                print("is wavecompleted and " + currentWave.waveTimer);
                GameManager.instance.ChangeWave(WaveCount);
                StartCoroutine(ICoolDownForNextWave());

            }
        }
    }
    IEnumerator ICoolDownForNextWave()
    {
        Debug.Log(" in IcoolDown coroutine");

        while (waveCoolDownTime > 0 && isWaveCompleted || currentWave.waveIndex != 0)
        {
            waveCoolDownTime -= 1f;
            yield return new WaitForSeconds(1f);
        }
        isWaveCompleted = false;
        IncreaseWaves();
        yield return new WaitForSeconds(Time.deltaTime);
    }


}
