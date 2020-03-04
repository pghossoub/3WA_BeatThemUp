using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public float m_respawnTimer = 5f;
    public IntVariable m_pv;
    public IntVariable m_pvMax;
    public IntVariable m_playerLives;
    public IntVariable m_DamageReceived;
    public BoolVariable m_playerIsDead;
    public TransformVariable m_trPlayer;
    public TransformVariable m_trCamera;
    public TransformVariable m_trEnemyAtackingPlayer;
    public GameEvent pvChange;
    public GameEvent m_playerDead;
    public GameEvent m_respawnPlayer;

    private Animator _animator;
    private int _playerHurtID;
    private int _playerIsDeadID;
    private float _initialRespawnTimer;
    private Rigidbody2D _rb;

    [SerializeField]
    private float _forcePush;
    [SerializeField]
    private float _durationPush;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        
        _playerHurtID = Animator.StringToHash("Hurt");
        _playerIsDeadID = Animator.StringToHash("IsDead");

        m_trPlayer.value = transform;
        m_playerLives.value = 3;
        m_playerIsDead.value = false;
        m_pv.value = m_pvMax.value;
        _initialRespawnTimer = m_respawnTimer;
    }

    private void Update()
    {
        if (m_pv.value <= 0 && m_playerLives.value >= 0 && !m_playerIsDead.value)
        {
            m_playerIsDead.value = true;
            _rb.velocity = Vector2.zero;
            _animator.SetBool(_playerIsDeadID, m_playerIsDead.value);
            m_playerDead.Raise();
        }
        else if (m_pv.value <= 0 && m_playerLives.value >= 0 && m_playerIsDead.value)
        {
            if (m_playerLives.value == 0)
            {
                return;
            }
            m_respawnTimer -= Time.deltaTime;
            if(m_respawnTimer < 0)
            {
                RespawnPlayer();
                m_respawnTimer = _initialRespawnTimer;
            }
        }
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(_playerHurtID);

        m_pv.value -= m_DamageReceived.value;

        pvChange.Raise();
        PushPlayer();
    }

    private void PushPlayer()
    {
        if (m_playerIsDead.value) return;

        Vector3 distancePlayerEnemy = transform.position - m_trEnemyAtackingPlayer.value.position;
        Vector3 directionPush;

        //Le joueur se tourne vers l'ennemi qui le frappe
        if (distancePlayerEnemy.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
            directionPush = new Vector3(1, 0, 0);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            directionPush = new Vector3(-1, 0, 0);
        }

        //_rb.DOJump(transform.position + directionPush, _forcePush, 1, _durationPush, false).SetUpdate(UpdateType.Fixed);
        _rb.DOMove(transform.position + directionPush, _durationPush).SetUpdate(UpdateType.Fixed);
    }

    [ContextMenu("RespawnPlayer")]
    public void RespawnPlayer()
    {
        m_trPlayer.value.position = new Vector3(m_trCamera.value.position.x, m_trCamera.value.position.y, m_trPlayer.value.position.z);
        m_pv.value = m_pvMax.value;
        m_playerLives.value = m_playerLives.value - 1;
        m_playerIsDead.value = false;
        _animator.SetBool(_playerIsDeadID, m_playerIsDead.value);
        m_respawnPlayer.Raise();
    }

}

