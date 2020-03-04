using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAndPause : MonoBehaviour
{
    #region Private
    private bool isPaused = false;
    #endregion

    #region MyFunctions
    public void Pause()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }
    public void Play()
    {
        if(isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
    #endregion
}