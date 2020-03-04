using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectArray
    {
        public GameObject[] gameObjectArray;
    }

    public IntVariable m_currentStep;
    public TransformVariable m_trPlayer;

    [SerializeField]
    private Step[] _steps;
    //[SerializeField]
    //private PolygonCollider2D _mainConfiner;
    //[SerializeField]
    //private PolygonCollider2D[] _confiners;
    [SerializeField]
    private GameObject[] _spawners;
    [SerializeField]
    private GameObjectArray[] _invisibleWalls;
    //[SerializeField]
    //private CinemachineConfiner _cinemachineConfiner;

    [SerializeField]
    private CinemachineVirtualCamera _cinemachineVirtualCam;

    //[SerializeField]
    //private CinemachineVirtualCamera _camera;

    //[SerializeField]
    //private BoxCollider2D[] _triggers;

    private int _stepId;
    private int _nbEnemyKilled;

    //Called by event listener: triggerStep
    public void ActivateStep()
    {
        _nbEnemyKilled = 0;
        _stepId = m_currentStep.value - 1;

        //stop camera
        //_cinemachineConfiner.m_BoundingShape2D = _confiners[_stepId];
        //_cinemachineConfiner.InvalidatePathCache();
        _cinemachineVirtualCam.Follow = null;

        //invisible walls
        foreach (GameObject invisibleWall in _invisibleWalls[_stepId].gameObjectArray)
        {
            invisibleWall.SetActive(true);
        }

        //activate spawner
        _spawners[_stepId].SetActive(true);
    }

    private void DeactivateStep()
    {
        //_cinemachineConfiner.m_BoundingShape2D = _mainConfiner;
        //_cinemachineConfiner.InvalidatePathCache();
        _cinemachineVirtualCam.Follow = m_trPlayer.value;

        foreach (GameObject invisibleWall in _invisibleWalls[_stepId].gameObjectArray)
        {
            invisibleWall.SetActive(false);
        }

        //_spawners[_stepId].SetActive(false);
        //Destroy(_spawners[_stepId]);
    }

    //Called by event listener: enemyIsKilled
    public void CheckEndStep()
    {
        _nbEnemyKilled++;
        //Debug.Log(_nbEnemyKilled);

        if (_nbEnemyKilled >= _steps[_stepId].nbEnemy)
        {
            DeactivateStep();
        }
    }




}
