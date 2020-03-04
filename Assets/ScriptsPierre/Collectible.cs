using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameEvent scoreChange;
    public GameEvent m_CollectiblePickUpSound;
    public IntVariable m_score;

    [SerializeField]
    private int _scoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFeet"))
        {
            m_score.value += _scoreValue;
            m_CollectiblePickUpSound.Raise();
            scoreChange.Raise();
            Destroy(gameObject);
        }
    }
}
