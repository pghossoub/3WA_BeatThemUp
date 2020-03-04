using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public GameEvent pvChange;
    public GameEvent m_healthCanSound;
    public IntVariable m_pv;
    public IntVariable m_pvMax;

    [SerializeField]
    private int _pvGain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFeet"))
        {
            if ((m_pv.value + _pvGain) < m_pvMax.value)
            {
                m_pv.value += _pvGain;
            }
            else m_pv.value += m_pvMax.value - m_pv.value;
            m_healthCanSound.Raise();
            pvChange.Raise();
            Destroy(gameObject);
        }
    }
}
