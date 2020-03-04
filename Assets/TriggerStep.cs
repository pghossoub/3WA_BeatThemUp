using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStep : MonoBehaviour
{
    public GameEvent m_triggerStepActivated;
    public IntVariable m_currentStepId;

    [SerializeField]
    private int _stepNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_currentStepId.value = _stepNumber;
            m_triggerStepActivated.Raise();
            Destroy(gameObject);
        }
    }
}
