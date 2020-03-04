using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePauseCanvas : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private GameObject _gameObject;
    #endregion

    #region MyFunctions
    public void ShowPauseCanvas()
    {
        _gameObject.SetActive(true);
    }
    public void HidePauseCanvas()
    {
        _gameObject.SetActive(false);
    }
    #endregion
}
