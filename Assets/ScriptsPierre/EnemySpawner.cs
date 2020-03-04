using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Step m_step;
    [SerializeField]
    private float _areaSpawn;
    [SerializeField]
    private GameObject _ennemy;

    //private Transform _tr;
    [SerializeField]
    private Transform[] _transforms;

    private Transform _tr;
    private int _indexTr = 0;

    private void Awake()
    {
        //_tr = GetComponent<Transform>();
        //_transforms = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        _tr = _transforms[0];
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < m_step.nbEnemy; i++)
        {
            Spawn();
            yield return new WaitForSeconds(1f);
            ChangeSpawner();

        }
    }

    private void Spawn()
    {
        Vector2 RandomPositionInArea = new Vector2(_tr.position.x, _tr.position.y)
            + Random.insideUnitCircle * _areaSpawn;

        Instantiate(_ennemy, RandomPositionInArea, Quaternion.identity, _tr);
    }

    private void ChangeSpawner()
    {
        if (_indexTr < _transforms.Length - 1)
            _indexTr++;

        else
            _indexTr = 0;
        _tr = _transforms[_indexTr];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _areaSpawn);
    }
}
