using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider vibrationSlider;

    private bool isMusicOn;
    public bool IsMusicOn
    {
        get
        {
            return isMusicOn;
        }
        set
        {
            isMusicOn = value;
            PlayerPrefs.SetInt("Music", isMusicOn ? 1 : 0);
        }
    }
    private bool isSoundOn;
    public bool IsSoundOn
    {
        get
        {
            return isSoundOn;
        }
        set
        {
            isSoundOn = value;
            PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        }
    }
    private bool isVibrationOn;
    public bool IsVibrationOn
    {
        get
        {
            return isVibrationOn;

        }
        set
        {
            isVibrationOn = value;
            PlayerPrefs.SetInt("Vibration", isVibrationOn ? 1 : 0);
        }
    }
    public void IsMusic()
    {
        IsMusicOn =!IsMusicOn;
        if (isMusicOn)
        {
            musicSlider.value = 1;
        }
        else
        {
            musicSlider.value = 0;
        }

    }
    public void IsSound()
    {
        IsSoundOn = !IsSoundOn;
        if (isSoundOn)
        {
            soundSlider.value = 1;
        }
        else
        {
            soundSlider.value = 0;
        }
    }
    public void IsVibration()
    {
        IsVibrationOn = !IsVibrationOn;
        if (!IsVibrationOn)
        {
            vibrationSlider.value = 1;
        }
        else
        {
            vibrationSlider.value = 0;
        }
    }
}
