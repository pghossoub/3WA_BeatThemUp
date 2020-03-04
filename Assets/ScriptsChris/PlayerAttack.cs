using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Seriazlized
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _comboTimer = 1f;
    [SerializeField]
    private float _attackInterval = 0.25f;
    [SerializeField]
    private float _damageInterval = 0.1f;
    [SerializeField]
    private BoolVariable _isDiving;
    [SerializeField]
    private BoolVariable _playerIsDead;
    [SerializeField]
    private GameEvent _attackSound;
    #endregion

    #region Private
    private BoxCollider2D _boxCollider;
    private float _initialAttackInterval;
    private float _initialDamageInterval;
    private float _initialComboTimer;
    private ComboState _comboState;
    private int _playerSpeedId;
    private int _isFallingId;
    private int _isDivingId;
    private int _attackId;
    private int _comboId;
    #endregion

    #region ComboState
    public enum ComboState
    {
        STRIKE1,
        STRIKE2,
        STRIKE3,
        STRIKE4
    }
    #endregion

    #region Unity Events
    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();

        _playerSpeedId = Animator.StringToHash("PlayerSpeed");
        _isFallingId = Animator.StringToHash("isFalling");
        _isDivingId = Animator.StringToHash("isDiving");
        _attackId = Animator.StringToHash("Attack");
        _comboId = Animator.StringToHash("Combo");

        _initialAttackInterval = _attackInterval;
        _initialDamageInterval = _damageInterval;
        _initialComboTimer = _comboTimer;

        _isDiving.value = false;

        _attackInterval = 0;
        _damageInterval = 0;
        _comboTimer = 0;
    }

    private void Update()
    {
        if (_playerIsDead.value) return;
        _animator.SetBool(_isDivingId, _isDiving.value);
        if (_comboTimer <= 0)
        {
            _comboState = ComboState.STRIKE1;
            _animator.SetInteger(_comboId, 0);
        }
        Attack();
        if (_comboTimer > 0) _comboTimer -= Time.deltaTime;
        if (_attackInterval > 0) _attackInterval -= Time.deltaTime;
        if (_damageInterval > 0) _damageInterval -= Time.deltaTime;
    }
    #endregion

    #region MyFunctions
    private void Attack()
    {
        //Physics2D.OverlapAreaAll -> Collider2D

        if (Input.GetButtonDown("Fire3") && _attackInterval <= 0)
        {
            _animator.ResetTrigger(_attackId);
            _animator.SetTrigger(_attackId);

            if (_animator.GetFloat(_playerSpeedId) < 0.1 && (_animator.GetBool("isFalling") || _animator.GetBool("isJumping")))
            {
                _isDiving.value = true;
            }

            if (_isDiving.value) return;

            switch (_comboState)
            {
                case ComboState.STRIKE1:
                    _animator.SetInteger(_comboId, 1);
                    _comboState = ComboState.STRIKE2;
                    break;
                case ComboState.STRIKE2:
                    _animator.SetInteger(_comboId, 2);
                    _comboState = ComboState.STRIKE3;
                    break;
                case ComboState.STRIKE3:
                    _animator.SetInteger(_comboId, 3);
                    _comboState = ComboState.STRIKE4;
                    break;
                case ComboState.STRIKE4:
                    _animator.SetInteger(_comboId, 4);
                    _comboState = ComboState.STRIKE1;
                    break;
            }
            _attackSound.Raise();
            _comboTimer = _initialComboTimer;
            _attackInterval = _initialAttackInterval;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _damageInterval <= 0)
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            enemy.Hurt();
        }
        if (collision.CompareTag("VendingMachine") && _damageInterval <= 0)
        {
            VendingMachineBehaviour vendingMachine = collision.GetComponent<VendingMachineBehaviour>();
            vendingMachine.TakeDamage();
        }
        if ((collision.CompareTag("Enemy") || collision.CompareTag("VendingMachine")) && _damageInterval <= 0)
            _damageInterval = _initialDamageInterval;
    }

    public void EnableAttackHitBox()
    {
        _boxCollider.enabled = true;
    }
    public void DisableAttackHitBox()
    {
        _boxCollider.enabled = false;
    }
    #endregion

#if UNITY_EDITOR
    #region Debug
    private void OnGUI()
    {
        //GUI.Button(new Rect(10, 40, 200, 30), "State: " + _comboState);
        //GUI.Button(new Rect(10, 70, 200, 30), "ComboTimer: " + _comboTimer);
    }
    #endregion
#endif
}
