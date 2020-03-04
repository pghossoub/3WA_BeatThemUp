using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private Player _player;
    [SerializeField]
    private PlayerAttack _playerAttack;
    [SerializeField]
    private PlayerDiveAttack _playerDiveAttack;
    #endregion

    #region MyFunctions
    public void EnableAttackHitBox()
    {
        _playerAttack.EnableAttackHitBox();
    }
    public void DisableAttackHitBox()
    {
        _playerAttack.DisableAttackHitBox();
    }
    public void EnableDiveHitBox()
    {
        _playerDiveAttack.EnableDiveHitBox();
    }
    public void DisableDiveHitBox()
    {
        _playerDiveAttack.DisableDiveHitBox();
    }
    #endregion
}
