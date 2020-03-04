using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    #region Serialized
    [SerializeField]
    private Transform _spriteTransform;
    [SerializeField]
    private float _jumpTime = 1f;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _jumpSpeed = 3f;
    [SerializeField]
    private float _fallSpeed = 5f;
    [SerializeField]
    private float _diveSpeed = 10f;
    [SerializeField]
    private GameObject _playerHitBox;
    [SerializeField]
    private GameObject _attackHitBox;
    [SerializeField]
    private BoolVariable _isDiving;
    [SerializeField]
    private BoolVariable _playerIsDead;
    [SerializeField]
    private GameEvent _jumpSound;
    [SerializeField]
    private GameEvent _diveSound;
    #endregion

    #region Private
    private float _initialJumpTime;
    private bool _isJumping = false;
    private bool _isFalling = false;

    private Vector3 _startPosition;
    private Vector3 _finalPosition;

    private int _isJumpingId;
    private int _isFallingId;

    //moche et mechant
    private const int _groundLayer = 0;
    private const int _inAirLayer = 13;
    #endregion

    #region Unity Events
    private void Awake()
    {
        _initialJumpTime = _jumpTime;
        _jumpTime = 0f;

        _startPosition = Vector3.zero;
        _finalPosition = _spriteTransform.localPosition + Vector3.up;

        _isJumpingId = Animator.StringToHash("isJumping");
        _isFallingId = Animator.StringToHash("isFalling");
    }

    private void Update()
    {
        if (_playerIsDead.value) return;
        if (_isDiving.value)
        {
            Dive();
            _isJumping = false;
            return;
        }
        if (_jumpTime >= 0) _jumpTime -= Time.deltaTime;
        if (_isJumping && !_isFalling || Input.GetButtonDown("Fire1") && _jumpTime < 0) Jump();
        if (_isFalling && !_isDiving.value) Fall();
        _animator.SetBool(_isJumpingId, _isJumping);
        _animator.SetBool(_isFallingId, _isFalling);
    }
    #endregion

    #region MyFunctions
    private void Jump()
    {
        if (!_isJumping)
        {
            _jumpSound.Raise();
            _jumpTime = _initialJumpTime;
            _isJumping = true;
            _playerHitBox.layer = _inAirLayer;
            _attackHitBox.layer = _inAirLayer;
        }
        else if (_spriteTransform.localPosition.y >= _finalPosition.y)
        {
            _spriteTransform.localPosition = _finalPosition;
            _isJumping = false;
            _isFalling = true;
        }
        else
        {
            _spriteTransform.localPosition += Vector3.up * _jumpSpeed * Time.deltaTime;
        }
    }

    private void Fall()
    {
        if (_spriteTransform.localPosition.y <= _startPosition.y)
        {
            _spriteTransform.localPosition = _startPosition;
            _playerHitBox.layer = _groundLayer;
            _attackHitBox.layer = _groundLayer;
            _isFalling = false;
        }
        else
        {
            _spriteTransform.localPosition += Vector3.down * _fallSpeed * Time.deltaTime;
        }
    }

    private void Dive()
    {
        if (_spriteTransform.localPosition.y <= _startPosition.y)
        {
            _spriteTransform.localPosition = _startPosition;
            _playerHitBox.layer = _groundLayer;
            _attackHitBox.layer = _groundLayer;
            _isDiving.value = false;
            _isFalling = false;
            _diveSound.Raise();
        }
        else
        {
            _spriteTransform.localPosition += Vector3.down * _diveSpeed * Time.deltaTime;
        }
    }
    #endregion
}
