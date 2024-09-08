using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int playerLife;
    [SerializeField] AleinManager aleinManager;
    [SerializeField] WaveManager waveManager;
    
    public int PlayerLife
    {
        get
        {
            return playerLife;
        }
        set
        {
            playerLife = value;
            uiManager.lifeText.text = "Lives: " + playerLife.ToString();
            if (playerLife == 0)
            {
                Time.timeScale = 0;
                uiManager.gameOverPanel.SetActive(true);
                isGameOver = true;

            }
        }
    }
    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            uiManager.scoreText.text = "Score: " + score.ToString();
        }
    }
    public UIManager uiManager;

    public bool isGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //  uiManager = FindObjectOfType<UIManager>();
        PlayerLife = 3;
        Score = 0;
        uiManager.gameOverPanel.SetActive(false);
        waveManager.IncreaseWaves();
        LevelStart( Waves.wave1, AlienType.beginner);
    }

    //This is where we make level settings.
    // Level progression depends on time.
    // each level needs to have some breathing time where we clear all enemies and reinstantiate new list 
    public enum Waves { wave1, wave2, wave3, wave4, wave5 }
    public void LevelStart( Waves levels, AlienType alienType)//this method decides how many enemies each level should have
    {

        switch (levels)
        {
            case Waves.wave1:
               
                alienType = AlienType.beginner;
                break;
            case Waves.wave2:
                alienType = AlienType.beginner;
                break;
            case Waves.wave3:
                alienType = AlienType.intermediate;
                break;
            case Waves.wave4:
                alienType = AlienType.intermediate;
                break;
            case Waves.wave5:
                alienType = AlienType.hard;
                break;
            default:
                alienType = AlienType.beginner;
                break;
        }
      
        aleinManager.StartGame( 5, alienType);

    }
    //public void AlienSpawnerForLevels(Levels levels, AlienType alienType)//This method decides type of enemies which has to be spawned for different levels
    //{
    //    switch (levels)
    //    {
    //        case Levels.level1:
    //            alienType = AlienType.beginner;
    //            break;
    //        case Levels.level2:
    //            alienType = AlienType.beginner;
    //            break;
    //        case Levels.level3:
    //            alienType = AlienType.intermediate;
    //            break;
    //        case Levels.level4:
    //            alienType = AlienType.intermediate;
    //            break;
    //        case Levels.level5:
    //            alienType = AlienType.hard;
    //            break;
    //        default:
    //            alienType = AlienType.beginner;
    //            break;
    //    }
    //}
    //Combine both enemy type and enemy count for each level;


}

