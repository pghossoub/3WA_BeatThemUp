using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Runtime Set")]
public class RuntimeSet : ScriptableObject
{
    private List<GameObject> _list;

    public void Add(GameObject obj)
    {
        if(!_list.Contains(obj))
        {
            _list.Add(obj);
        }
    }

    public void Remove(GameObject obj)
    {
        if(_list.Contains(obj))
        {
            _list.Remove(obj);
        }
    }

    public GameObject[] Elements()
    {
        return _list.ToArray();
    }
}
