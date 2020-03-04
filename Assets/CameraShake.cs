using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private float amplitudeGain = 0.1f;
    private float frequencyGain = 0.3f;

    private CinemachineVirtualCamera _cVCam;
    private CinemachineBasicMultiChannelPerlin _noise;

    void Start()
    {
        _cVCam = GetComponent<CinemachineVirtualCamera>();
        _noise = _cVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void StartShake()
    {
        if(_noise == null) Debug.Log("Noise = none");
        _noise.m_AmplitudeGain = amplitudeGain;
        _noise.m_FrequencyGain = frequencyGain;
    }

    public void StopShake()
    {
        _noise.m_AmplitudeGain = 0f;
        _noise.m_FrequencyGain = 0f;
    }
}
