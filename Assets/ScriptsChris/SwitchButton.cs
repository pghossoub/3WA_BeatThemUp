using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private Sprite _activeSprite;
    [SerializeField]
    private Sprite _inactiveSprite;
    [SerializeField]
    private GameEvent _activeEffect;
    [SerializeField]
    private GameEvent _inactiveEffect;
    #endregion

    #region Private
    private Image _image;
    private bool _ownState = true;
    #endregion

    #region Unity Events
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    #endregion

    #region MyFunctions
    public void SwitchMode()
    {
        switch (_ownState)
        {
            case true:
                _image.sprite = _inactiveSprite;
                _activeEffect.Raise();
                break;
            case false:
                _image.sprite = _activeSprite;
                _inactiveEffect.Raise();
                break;
        }
        _ownState = !_ownState;
    }
    #endregion
}
