using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // this script manages the waves. 
    public List<Wave> waves;
    public int waveCount;
    public int prevWave, nextWave;
    public float waveCoolDownTime = 5f;
    public bool isWaveCompleted;
    int index;
    public Wave currentWave;

    public void IncreaseWaves()
    {
        if (waveCount == 0)
        {
            waveCount = 1;
            index = 0;
        }
        if (isWaveCompleted)
        {
            waveCount++;
            index = waveCount - 1;
        }
        if (index < waves.Count)
        {
            currentWave = waves[index];
        }
        else
        {
            Debug.Log("All waves completed. No more waves to load.");
        }

        PlayerPrefs.SetInt("LastWave", waveCount);
    }
    IEnumerator RunWaveTimer()
    {
        while (!isWaveCompleted && currentWave.waveTimer > 0)
        {
            currentWave.waveTimer -= Time.deltaTime;
            yield return null; // Waits until the next frame
        }

        // If you need to take any action when the wave timer ends
        if (currentWave.waveTimer <= 0)
        {
            isWaveCompleted = true;
            // Any additional logic when the wave is completed
        }
    }

}
