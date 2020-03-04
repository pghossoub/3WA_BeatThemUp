using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCamera : MonoBehaviour
{
    [SerializeField]
    private TransformVariable _cameraTransform;

    private void Awake()
    {
        _cameraTransform.value = GetComponent<Transform>();
    }
}
