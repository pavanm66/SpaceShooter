using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText, lifeText,waveText;
    public GameObject gameOverPanel,pausePanel;
   [SerializeField] bool isPaused = false;
   public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = 0;
    }


    public void PauseButton()
    {
        isPaused=!isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pausePanel.SetActive(isPaused);
    }


}
