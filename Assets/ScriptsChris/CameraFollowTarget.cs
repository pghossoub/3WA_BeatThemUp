using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _cameraSpeed = 1.0f;

    [SerializeField]
    private bool _keepCameraX = true;
    [SerializeField]
    private bool _keepCameraY = true;
    [SerializeField]
    private bool _keepCameraZ = true;
#pragma warning restore CS0649
    #endregion

    #region Private
    private Transform _transform;
    private float _positionX;
    private float _positionY;
    private float _positionZ;
    private float fraction = 0f;
    #endregion

    #region Unity Events
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_target == null) return;
        if (fraction < 1) fraction += Time.deltaTime * _cameraSpeed;

        _positionX = _keepCameraX ? _transform.position.x : _target.position.x;
        _positionY = _keepCameraY ? _transform.position.y : _target.position.y;
        _positionZ = _keepCameraZ ? _transform.position.z : _target.position.z;

        Vector3 nextPosition = new Vector3(_positionX, _positionY, _positionZ);
        Vector3 newPosition = Vector3.Lerp(_transform.position, nextPosition, fraction);
        if (!float.IsNaN(newPosition.x) && !float.IsNaN(newPosition.y))
        {
            _transform.position = newPosition;
            fraction = 0;
        }
    }
    #endregion
}
