using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int playerLife;
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
   
}
