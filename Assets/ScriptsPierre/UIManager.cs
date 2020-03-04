using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public IntVariable m_pv;
    public IntVariable m_pvMax;

    public IntVariable m_score;

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private Slider _pvSlider;


    private void Awake()
    {
        m_score.value = 0;
    }
    public void DisplayHp()
    {
        _pvSlider.value = m_pv.value / (float)m_pvMax.value;
    }

    public void DisplayScore()
    {
        _scoreText.text = m_score.value.ToString("D6");
        //_scoreText.text = string.Format("{0}", m_score.value);
    }
}

