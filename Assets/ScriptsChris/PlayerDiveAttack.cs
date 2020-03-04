using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiveAttack : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private float _damageInterval = 0.5f;
    #endregion

    #region Private
    private CapsuleCollider2D _collider;
    private float _initialDamageInterval;
    #endregion

    #region Unity Events
    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _initialDamageInterval = _damageInterval;
        _damageInterval = 0;
    }

    private void Update()
    {
        if (_damageInterval > 0) _damageInterval -= Time.deltaTime;
    }
    #endregion

    #region MyFunctions
    public void EnableDiveHitBox()
    {
        _collider.enabled = true;
    }
    public void DisableDiveHitBox()
    {
        _collider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _damageInterval <= 0)
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            enemy.Hurt();
            _damageInterval = _initialDamageInterval;
        }
    }
    #endregion
}
