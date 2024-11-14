using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText, lifeText,waveText;
    public GameObject gameOverPanel;
   public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = 0;
    }

   
}
