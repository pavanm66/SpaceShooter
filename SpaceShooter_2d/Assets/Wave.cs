using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveObjects", order = 1)]
public class Wave : ScriptableObject
{
    public int beginnerEnemyCount;
    public int intermediateEnemyCount;
    public float waveTimer;
    public int activeEnemyCounter;
    public int enemySpawnCounter;
    public int waveIndex;

}
