using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPrefab : MonoBehaviour
{
    [SerializeField] Text m_text;
    [SerializeField] InputField m_inputField;
    /// <summary>InputField‚Ì’l‚ðŽæ“¾</summary>
    public int InputFieldNum => int.Parse(m_inputField.text);

    public void Setup(string titleText)
    {
        m_text.text = titleText;
    }
}
