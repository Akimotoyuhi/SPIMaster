using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 出題内容の設定画面
/// </summary>
public class SettingPrefab : MonoBehaviour
{
    [SerializeField] Text m_text;
    [SerializeField] InputField m_inputField;
    /// <summary>InputFieldの値を取得</summary>
    public int InputFieldNum => int.Parse(m_inputField.text);

    public void Setup(string titleText)
    {
        m_text.text = titleText;
    }
}
