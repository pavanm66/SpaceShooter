using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Button soundSlider;
    [SerializeField] Button vibrationSlider;
    [SerializeField] Button musicButton;
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;

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
            musicButton.image.sprite = isMusicOn ? onSprite : offSprite;

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
            soundSlider.image.sprite = isSoundOn ? onSprite : offSprite;
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
            vibrationSlider.image.sprite = isVibrationOn ? onSprite : offSprite;
        }
    }
   
    public void IsMusic()
    {
        IsMusicOn =!IsMusicOn;
    }
    public void IsSound()
    {
        IsSoundOn = !IsSoundOn;
    }
    public void IsVibration()
    {
        IsVibrationOn = !IsVibrationOn;
    }
}
