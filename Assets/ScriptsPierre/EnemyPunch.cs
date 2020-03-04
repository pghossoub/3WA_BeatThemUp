using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    public GameEvent m_playerHit;
    public IntVariable m_damageOnPlayer;
    //public GameObjectVariable m_enemyAttackingPlayer;
    public TransformVariable m_transformEnemy;

    [SerializeField]
    private float _damageRate;
    [SerializeField]
    private int _damage = 10;

    private bool _IsAtacking = false;
    private float _nextDamage= 0f;

    [SerializeField]
    private Enemy _enemy;


    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerStay");
        if (_enemy.m_state == Enemy.State.DEAD)
            return;

        if (collision.CompareTag("Player") && _IsAtacking && Time.time > _nextDamage)
        {
            _nextDamage = Time.time + _damageRate;
            //Debug.Log("playerHit");
            m_damageOnPlayer.value = _damage;
            m_transformEnemy.value = transform;
            //m_enemyAttackingPlayer.value = gameObject;
            m_playerHit.Raise();
        }
    }

    public void Attack()
    {
        _IsAtacking = true;
    }

    public void StopAttack()
    {
        _IsAtacking = false;
    }
/*
#if UNITY_EDITOR
    void OnGUI()
    {
        GUI.Button(new Rect(10, 50, 120, 30), $"IsAtacking: {IsAtacking}");
    }
#endif
*/
}
