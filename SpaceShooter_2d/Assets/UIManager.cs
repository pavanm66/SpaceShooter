using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText, lifeText;
    public GameObject gameOverPanel;
   public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
  }
