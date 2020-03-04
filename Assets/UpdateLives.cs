using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateLives : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private IntVariable _playerLives;

    private void Start()
    {
        _text.text = _playerLives.value.ToString();
    }

    public void RefreshLives()
    {
        _text.text = _playerLives.value.ToString();
    }
}
