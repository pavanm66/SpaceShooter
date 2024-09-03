using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUIManager : MonoBehaviour
{
public Scenemanager scenemanager;
    public void PlayButton()
    {
        scenemanager.LoadGameScene();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
