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
         return value;
      }
      set{
         isSoundOn = value;
         PlayerPrefs.SetBool("isSoundOn",isSoundOn);
      }
   }
   public bool IsVibrationOn{
      get{
         return value;

      }
      set{
         isVibrationOn=value;
         PlayerPrefs.SetBool("isVibrationOn",isVibrationOn);
      }
   }

   
}
