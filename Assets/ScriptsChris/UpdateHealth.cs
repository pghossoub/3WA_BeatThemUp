using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private RectTransform _rectTransform;
    #endregion

    #region Private
    private Vector3 _initialOffsetMax;
    private Vector3 _initialOffsetMin;
    private const float _finalOffsetMaxX = -100f;
    #endregion

    #region Unity Events
    private void Awake()
    {
        _initialOffsetMax = _rectTransform.offsetMax;
        _initialOffsetMin = _rectTransform.offsetMin;
    }
    #endregion

    #region MyFunctions
    public void ChangeHP(float percentHealth)
    {
        //Valeur a ajouter a right et a left
        float valueToAdd = (Mathf.Abs(_initialOffsetMax.x) - Mathf.Abs(_finalOffsetMaxX)) * (percentHealth/100);

        //Si on tente de depasser 100% de ses points de vie
        if (_rectTransform.offsetMax.x + valueToAdd > _initialOffsetMax.x)
        {
            valueToAdd = _initialOffsetMax.x - _rectTransform.offsetMax.x;
        }
        //Change les PVs seulement si on reste au dessus de 0% des PVs
        //Ou si on soigne le joueur
        if ((Mathf.Abs(_rectTransform.offsetMax.x) > Mathf.Abs(_finalOffsetMaxX)) || 
            (valueToAdd > 0 && Mathf.Abs(_rectTransform.offsetMax.x) < Mathf.Abs(_initialOffsetMax.x)))
        {
            _rectTransform.offsetMax += new Vector2(valueToAdd, 0);
            _rectTransform.offsetMin += new Vector2(valueToAdd, 0);
        }
    }
    #endregion

#if UNITY_EDITOR
    #region Debug
    [ContextMenu("AddHP")]
    public void AddHP()
    {
        ChangeHP(10);
    }
    [ContextMenu("SubstractHP")]
    public void SubstractHP()
    {
        ChangeHP(-10);
    }
    #endregion
#endif
}
