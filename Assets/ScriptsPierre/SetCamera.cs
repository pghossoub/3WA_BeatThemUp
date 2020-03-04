using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCamera : MonoBehaviour
{
    public TransformVariable m_trPlayer;
    //public PolygonCollider2D m_levelCollider;

    private CinemachineVirtualCamera _cinemachineVirtualCam;
    //private CinemachineConfiner _cinemachineConfiner;

    void Start()
    {
        _cinemachineVirtualCam = GetComponent<CinemachineVirtualCamera>();
        //_cinemachineConfiner = GetComponent<CinemachineConfiner>();

        if(m_trPlayer.value != null)
            _cinemachineVirtualCam.Follow = m_trPlayer.value;
        /*if (m_levelCollider != null)
            _cinemachineConfiner.m_BoundingShape2D = m_levelCollider;
            */
    }
}
//confiner.invalidatePathCache