using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// クイズ中の選択用ボタンに付ける
/// </summary>
public class ChoiseButton : MonoBehaviour
{
    [SerializeField] Button m_button;
    [SerializeField] Text m_text;
    private Action m_onClick;
    public Button Button => m_button;

    public void Setup(string s, Action onClick)
    {
        m_text.text = s;
        m_onClick = onClick;
    }

    public void OnClick()
    {
        m_onClick();
    }
}
