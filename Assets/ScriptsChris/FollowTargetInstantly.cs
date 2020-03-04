using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetInstantly : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private Transform _targetTransform;
    #endregion

    #region Private
    private Transform _transform;
    #endregion

    #region Unity Events
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.position = _targetTransform.position;
    }
    #endregion
}
