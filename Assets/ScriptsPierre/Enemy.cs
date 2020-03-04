using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public State m_state;

    public GameEvent enemyIsKilled;
    //public IntVariable m_damageOnEnemy;
    public TransformVariable m_trPlayer;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private GameObject tape;
    [SerializeField]
    private GameObject disk;
    [SerializeField]
    private float _durationPush;
    [SerializeField]
    private float _forcePush;
    [SerializeField]
    private GameEvent _startCameraShake;
    [SerializeField]
    private GameEvent _stopCameraShake;

    private Transform _tr;
    private Transform _trPlayer;
    private Rigidbody2D _rb;

    private float _directionX;
    private float _directionY;
    private int _enemySpeedID;
    private int _enemyAttackID;
    private int _enemyHurtID;
    private int _enemyIsDeadID;
    private Animator _animator;
    
    private bool _isDead = false;

    public enum State
    {
        MOVING,
        ATTACKING,
        DEAD
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        _trPlayer = m_trPlayer.value;
        _animator = GetComponentInChildren<Animator>();

        _enemySpeedID = Animator.StringToHash("PlayerSpeed");
        _enemyAttackID = Animator.StringToHash("Attack");
        _enemyHurtID = Animator.StringToHash("Hurt");
        _enemyIsDeadID = Animator.StringToHash("IsDead");
    }
    private void Update()
    {
        switch (m_state)
        {
            case (State.MOVING):
                MoveTowardsPlayer();
                AnimateMovement();
                TryToAttack();
                break;

            case (State.ATTACKING):
                break;

            case (State.DEAD):
                break;

        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction;

        if (_trPlayer != null)
        {
            direction = _trPlayer.position - _tr.position;

            if (Mathf.Abs(direction.x) > 0.5f)
            {
                _directionX = direction.normalized.x;
            }
            else
                _directionX = 0f;

            if (Mathf.Abs(direction.y) > 0.0f)
            {
                _directionY = direction.normalized.y;
            }
            else
                _directionY = 0f;
        }
        else
        {
            _directionX = 0f;
            _directionY = 0f;
        }
    }

    private void FixedUpdate()
    {
        switch (m_state)
        {
            case (State.MOVING):
                _rb.velocity = new Vector2(_directionX, _directionY);
                _rb.velocity = _rb.velocity * _speed * Time.fixedDeltaTime;
                if (_rb.velocity.sqrMagnitude < 0.005)
                {
                    _rb.velocity = Vector2.zero;
                }
                break;
        }    
    }

    private void AnimateMovement()
    {
        Vector2 direction = _trPlayer.position - _tr.position;

        _animator.SetFloat(_enemySpeedID, _rb.velocity.normalized.sqrMagnitude);
        if(direction.x > 0f)
            _tr.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0f)
            _tr.localScale = new Vector3(-1, 1, 1);
    }

    private void TryToAttack()
    {
        Vector2 direction = _trPlayer.position - _tr.position;

        if (Mathf.Abs(direction.x) < 0.5f && Mathf.Abs(direction.y) < 0.2f)
        {
            //Debug.Log("Attack")
            _rb.velocity = Vector2.zero;
            _directionX = 0.0f;
            _directionY = 0.0f;
            _animator.SetInteger("Combo", 1);
            _animator.SetTrigger(_enemyAttackID);
            m_state = State.ATTACKING;
        }
    }

    public void EndAttack()
    {
        _animator.SetInteger("Combo", 0);
        m_state = State.MOVING;
    }

    [ContextMenu("HitEnemy")]
    public void Hurt(/*int damage*/)
    {
        int damage = 1;
        _hp -= damage;
        _animator.SetTrigger(_enemyHurtID);
        PushEnemy();

        StartCoroutine(CameraShake());
        _startCameraShake.Raise();

        if (_hp <= 0 && !_isDead)
        {
            _isDead = true;
            _rb.velocity = Vector2.zero;
            _animator.SetBool(_enemyIsDeadID, true);
            m_state = State.DEAD;
            DropItems();
            enemyIsKilled.Raise();
            StartCoroutine(DieCoroutine());
        }

    }

    private void PushEnemy()
    {
        Vector3 distanceEnemyPlayer = transform.position - _trPlayer.position;
        Vector3 directionPush;
        if (distanceEnemyPlayer.x > 0)
        {
            directionPush = new Vector3(_forcePush, 0, 0);
        }
        else
        {
            directionPush = new Vector3(-_forcePush, 0, 0);
        }
        _rb.DOMove(transform.position + directionPush, _durationPush).SetUpdate(UpdateType.Fixed);
    }

    private void DropItems()
    {
        Vector2 position = _tr.position;
        Instantiate(disk, position, Quaternion.identity);
        Instantiate(tape, new Vector2(position.x + 0.25f, position.y + 0.25f), Quaternion.identity);
        Instantiate(tape, new Vector2(position.x - 0.25f, position.y - 0.25f), Quaternion.identity);
    }

    IEnumerator CameraShake()
    {
        _startCameraShake.Raise();
        yield return new WaitForSeconds(0.1f);
        _stopCameraShake.Raise();
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

/*
#if UNITY_EDITOR
    void OnGUI()
    {
        GUI.Button(new Rect(10, 50, 120, 30), $"State: {_state}");
    }
#endif
*/
}
