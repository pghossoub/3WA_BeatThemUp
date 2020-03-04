using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 3;
    [SerializeField]
    private GameObject _healthCan;

    private Animator _animator;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private int _actualDamage = 0;
    private int _destroyedId;
    private int _damagedId;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _transform= GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _destroyedId = Animator.StringToHash("Destroyed");
        _damagedId = Animator.StringToHash("Damaged");
    }

    private void Update()
    {
        if (_actualDamage == _maxHealth) _animator.SetBool(_destroyedId, true);
    }

    public void TakeDamage()
    {
        if (_actualDamage == _maxHealth) return;
        if (_actualDamage < _maxHealth) _actualDamage++;
        GameObject instantiatedCan = Instantiate(_healthCan, _transform);
        instantiatedCan.transform.localPosition = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, -0.1f), 0);
        _animator.ResetTrigger(_damagedId);
        _animator.SetTrigger(_damagedId);
    }
}
