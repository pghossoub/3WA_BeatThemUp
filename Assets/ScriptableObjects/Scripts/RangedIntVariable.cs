using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Ranged Int Variable")]
public class RangedIntVariable : ScriptableObject
{
    [SerializeField]
    private int _value;

    [SerializeField]
    private int _minValue;

    [SerializeField]
    private int _maxValue;

    public int value
    {
        get => _value;
        set => _value = Mathf.Clamp(value, _minValue, _maxValue);
    }
}
