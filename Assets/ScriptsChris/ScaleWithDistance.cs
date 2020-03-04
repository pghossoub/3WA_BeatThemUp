using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithDistance : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private Transform _targetTransform;
    #endregion

    #region Private
    private Transform _transform;
    private float _originalX;
    private float _originalY;
    private float _originalZ;

    float minDistance = 0.1f;
    float maxDistance = 1f;

    float minScale = 0.5f;
    float maxScale = 2f;
    #endregion

    #region Unity Events
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _originalX = _transform.localScale.x/2;
        _originalY = _transform.localScale.y/2;
        _originalZ = _transform.localScale.z/2;
    }

    private void Update()
    {
        float distance = _targetTransform.position.y - _transform.position.y;
        float scale = Mathf.Lerp(maxScale, minScale, Mathf.InverseLerp(minDistance, maxDistance, distance));
        _transform.localScale = new Vector3(scale * _originalX, scale * _originalY, scale * _originalZ);
    }
    #endregion
}
