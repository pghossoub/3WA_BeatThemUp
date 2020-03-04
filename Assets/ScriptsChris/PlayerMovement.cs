using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Serialized
#pragma warning disable CS0649
    [SerializeField]
    private float _speed = 50f;
    [SerializeField]
    private float _maxVelocity = 20f;

    [SerializeField]
    private bool _activateGamepad = true;
    [SerializeField]
    private bool _activateKeyboard = false;

    [SerializeField]
    private BoolVariable _playerIsDead;

    [SerializeField]
    private Animator _animator;
#pragma warning restore CS0649
    #endregion

    #region Private
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private float _speedM = 2;

    private int _playerSpeedID;
    #endregion

    #region Unity Event
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _playerSpeedID = Animator.StringToHash("PlayerSpeed");
    }

    private void Update()
    {
        if (_playerIsDead.value) return;
        if (_activateGamepad) GamepadMovement();
        if (_activateKeyboard) KeyboardMovement();
    }
    #endregion

    #region Gamepad
    private void GamepadMovement()
    {
        RunMovement();
        WalkMovement();
        if (Input.GetAxisRaw("Sprint") < -0.5f)  _animator.SetFloat(_playerSpeedID, _rigidbody.velocity.normalized.sqrMagnitude * _speedM);
        else _animator.SetFloat(_playerSpeedID, _rigidbody.velocity.normalized.sqrMagnitude);
    }

    private void RunMovement()
    {
        if(Input.GetAxisRaw("Sprint") < -0.5f)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            //Change la direction de l'objet selon la velocite de l'objet
            if (directionX > 0) _transform.localScale = new Vector3(1, 1, 1);
            else if (directionX < 0) _transform.localScale = new Vector3(-1, 1, 1);

            //Si on ne touche pas au joystick, la velocite revient a zero
            if (directionX == 0) _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            if (directionY == 0) _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);

            float newSpeedX = directionX * _speed * Time.deltaTime * _speedM;
            float newSpeedY = directionY * _speed * Time.deltaTime * _speedM;

            //Si on change de direction
            if ((_rigidbody.velocity.x > 0 && newSpeedX < 0) ||
                 (_rigidbody.velocity.x < 0 && newSpeedX > 0))
            {
                _rigidbody.velocity = new Vector2(newSpeedX, _rigidbody.velocity.y);
            }
            //Si on change de direction
            if ((_rigidbody.velocity.y > 0 && newSpeedY < 0) ||
                 (_rigidbody.velocity.y < 0 && newSpeedY > 0))
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, newSpeedY);
            }


            //Sinon on vérifie que l'on ne depasse pas la velocite max
            else
            {
                bool newVelocityX = Mathf.Abs(_rigidbody.velocity.x) + Mathf.Abs(newSpeedX) > _maxVelocity * _speedM;
                bool newVelocityY = Mathf.Abs(_rigidbody.velocity.y) + Mathf.Abs(newSpeedY) > _maxVelocity * _speedM;

                /* 
                 * Si on va depasser la velocite max, on donne a newSpeedX la difference entre la velocite actuelle et la velocite max
                 * afin d'atteindre la velocite max
                 */
                if (newVelocityX && _rigidbody.velocity.x < _maxVelocity * _speedM)
                {
                    //Si on se dirige vers la gauche ( x négatif )
                    if (newSpeedX < 0) newSpeedX = -(_maxVelocity * _speedM) - _rigidbody.velocity.x;
                    //Sinon, on se dirige vers la droite ( x positif )
                    else newSpeedX = _maxVelocity * _speedM - _rigidbody.velocity.x;
                }
                //Si la velocite max est depassee et que la velocite actuelle est deja au max
                else if (newVelocityX) newSpeedX = 0;
                /* 
                 * Si on va depasser la velocite max, on donne a newSpeedY la difference entre la velocite actuelle et la velocite max
                 * afin d'atteindre la velocite max
                 */
                if (newVelocityY && _rigidbody.velocity.y < _maxVelocity * _speedM)
                {
                    //Si on se dirige vers le bas ( y négatif )
                    if (newSpeedY < 0) newSpeedY = -(_maxVelocity * _speedM) - _rigidbody.velocity.y;
                    //Sinon, on se dirige vers le haut ( y positif )
                    else newSpeedY = _maxVelocity * _speedM - _rigidbody.velocity.y;
                }
                //Si la velocite max est depassee et que la velocite actuelle est deja au max
                else if (newVelocityY) newSpeedY = 0;

                _rigidbody.velocity += new Vector2(newSpeedX, newSpeedY);
            }
        }
    }

    private void WalkMovement()
    {
        if (Input.GetAxisRaw("Sprint") < -0.5f) return; 

        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        //Change la direction de l'objet selon la velocite de l'objet
        if (directionX > 0) _transform.localScale = new Vector3(1, 1, 1);
        else if (directionX < 0) _transform.localScale = new Vector3(-1, 1, 1);

        //Si on ne touche pas au joystick, la velocite revient a zero
        if (directionX == 0) _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        if (directionY == 0) _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);

        float newSpeedX = directionX * _speed * Time.deltaTime;
        float newSpeedY = directionY * _speed * Time.deltaTime;

        //Si on change de direction
        if ( (_rigidbody.velocity.x > 0 && newSpeedX < 0) || 
             (_rigidbody.velocity.x < 0 && newSpeedX > 0) )
        {
            _rigidbody.velocity = new Vector2(newSpeedX, _rigidbody.velocity.y);
        }
        //Si on change de direction
        if ((_rigidbody.velocity.y > 0 && newSpeedY < 0) ||
             (_rigidbody.velocity.y < 0 && newSpeedY > 0))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, newSpeedY);
        }


        //Sinon on vérifie que l'on ne depasse pas la velocite max
        else
        {
            bool newVelocityX = Mathf.Abs(_rigidbody.velocity.x) + Mathf.Abs(newSpeedX) > _maxVelocity;
            bool newVelocityY = Mathf.Abs(_rigidbody.velocity.y) + Mathf.Abs(newSpeedY) > _maxVelocity;

            /* 
             * Si on va depasser la velocite max, on donne a newSpeedX la difference entre la velocite actuelle et la velocite max
             * afin d'atteindre la velocite max
             */
            if (newVelocityX && _rigidbody.velocity.x < _maxVelocity)
            {
                //Si on se dirige vers la gauche ( x négatif )
                if (newSpeedX < 0) newSpeedX = -(_maxVelocity) - _rigidbody.velocity.x;
                //Sinon, on se dirige vers la droite ( x positif )
                else newSpeedX = _maxVelocity - _rigidbody.velocity.x;
            }
            //Si la velocite max est depassee et que la velocite actuelle est deja au max
            else if (newVelocityX) newSpeedX = 0;
            /* 
             * Si on va depasser la velocite max, on donne a newSpeedY la difference entre la velocite actuelle et la velocite max
             * afin d'atteindre la velocite max
             */
            if (newVelocityY && _rigidbody.velocity.y < _maxVelocity)
            {
                //Si on se dirige vers le bas ( y négatif )
                if (newSpeedY < 0) newSpeedY = -(_maxVelocity) - _rigidbody.velocity.y;
                //Sinon, on se dirige vers le haut ( y positif )
                else newSpeedY = _maxVelocity - _rigidbody.velocity.y;
            }
            //Si la velocite max est depassee et que la velocite actuelle est deja au max
            else if (newVelocityY) newSpeedY = 0;

            _rigidbody.velocity += new Vector2(newSpeedX, newSpeedY);
        }
    }
    #endregion


    #region Keyboard
    private void KeyboardMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _transform.Translate(Vector2.up * _speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _transform.Translate(Vector2.left * _speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _transform.Translate(Vector2.down * _speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _transform.Translate(Vector2.right * _speed);
        }
    }
    #endregion

#if UNITY_EDITOR
    #region Debug
    private void OnGUI()
    {
        //GUI.Button(new Rect(10, 10, 200, 30), "State: " + _state);
    }
    #endregion
#endif
}
