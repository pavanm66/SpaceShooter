using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] pWeights;
    public int[] eWeights;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    [SerializeField] private int direction;
    public int maxPlayerBarnHealth;
    public int maxEnemyBarnHealth;

    public int CheckPushingDirection(int laneIndex)
    {
        return eWeights[laneIndex - 1] > pWeights[laneIndex - 1] ? -1 : eWeights[laneIndex - 1] < pWeights[laneIndex - 1] ? 1 : 0;

     

    }
}
