using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

/// <summary>
/// クイズ中の選択用ボタンに付ける
/// </summary>
public class ChoiseButton : MonoBehaviour
{
    [SerializeField] Button m_button;
    [SerializeField] Text m_text;
    private Subject<Unit> m_onclick = new Subject<Unit>();
    public IObservable<Unit> OnClick => m_onclick;

    public void Setup(string s)
    {
        m_text.text = s;
        m_button.onClick.AddListener(() => m_onclick.OnNext(Unit.Default));
    }
}
