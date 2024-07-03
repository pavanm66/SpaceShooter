using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int playerLife;
    [SerializeField] AleinManager aleinManager;
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
    }

    //This is where we make level settings.
    // Level progression depends on time.
    // each level needs to have some breathing time where we clear all enemies and reinstantiate new list 
    public enum Levels { level1, level2, level3, level4, level5 }
    public void LevelStart(List<GameObject> _alienList, Levels levels)
    {
        switch (levels)
        {
            case Levels.level1:
                _alienList = new List<GameObject>(5);
                break;
            case Levels.level2:
                _alienList = new List<GameObject>(10);
                break;
            case Levels.level3:
                _alienList = new List<GameObject>(15);
                break;
            case Levels.level4:
                _alienList = new List<GameObject>(20);
                break;
            case Levels.level5:
                _alienList = new List<GameObject>(25);
                break;
            default:
                break;
        }


    }
    public void AlienSpawnerForLevels(Levels levels, AlienType alienType)
    {
        switch (levels)
        {
            case Levels.level1:
                alienType = AlienType.beginner;
                break;
            case Levels.level2:
                alienType = AlienType.beginner;
                break;
            case Levels.level3:
                alienType = AlienType.intermediate;
                break;
            case Levels.level4:
                alienType = AlienType.intermediate;
                break;
            case Levels.level5:
                alienType = AlienType.hard;
                break;
            default:
                alienType = AlienType.beginner;
                break;
        }
    }

}

