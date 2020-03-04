using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;
    [SerializeField]
    private EnemyPunch _enemyPunch;

    private void Awake()
    {
        //enemyPunch = GetComponentInChildren<EnemyPunch>();
    }

    public void Attack()
    {
        _enemyPunch.Attack();
    }

    public void StopAttack()
    {
        _enemyPunch.StopAttack();
    }

    public void EndAttack()
    {
        _enemy.EndAttack();
    }
}
