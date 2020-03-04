using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicVolume : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField]
    private string _exposedParameterName;
    #endregion

    #region MyFunctions
    public void DisableMusic()
    {
        _audioMixer.SetFloat(_exposedParameterName, -80f);
    }
    public void EnableMusic()
    {
        _audioMixer.SetFloat(_exposedParameterName, 0f);
    }
    #endregion
}
