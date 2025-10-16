using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   private bool isSoundOn;
   private bool isVibrationOn;
   private bool isMusicOn;

   public bool IsSoundOn{
      get{
         return isSoundOn;
      }
      set{
         isSoundOn = value;
            if (isSoundOn == true)
            {
                PlayerPrefs.SetInt("isSoundOn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("isSoundOn", 0);
            }
        }
   }
   public bool IsVibrationOn{
      get{
         return isVibrationOn;

      }
      set{
         isVibrationOn=value;
            if (isVibrationOn == true)
            {
                PlayerPrefs.SetInt("isVibrationOn", 1);
            }
            else {
                PlayerPrefs.SetInt("isVibrationOn", 0);
            }
      }
   }
   public bool IsMusicOn{
      get{
         return isMusicOn;
      }
      set {
         isMusicOn = value;
            if (isMusicOn == true)
            {
                PlayerPrefs.SetInt("isMusicOn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("isMusicOn", 0);
            }
        }
   }

   
}
