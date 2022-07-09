using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�N�C�Y�ݒ��ʂ̊Ǘ�</summary>
public class QuizSetting : MonoBehaviour
{
    [SerializeField] GameObject m_quizSettingPanel;
    [SerializeField] SettingPrefab m_settingPrefab;
    [SerializeField] Transform m_settingPrefabParent;
    private List<SettingPrefab> m_prefabs = new List<SettingPrefab>();

    public void Setup(List<string> sheetNames)
    {
        m_quizSettingPanel.SetActive(false);
        //�ݒ荀�ڐ���
        foreach (var item in sheetNames)
        {
            SettingPrefab s = Instantiate(m_settingPrefab);
            s.transform.SetParent(m_settingPrefabParent, false);
            s.Setup(item);
            m_prefabs.Add(s);
        }
    }

    /// <summary>
    /// �ݒ��ʂ�\��
    /// </summary>
    public void ViewSettingPanel()
    {
        m_quizSettingPanel.SetActive(true);
    }

    /// <summary>
    /// �J�n�{�^���������ꂽ��<br/>
    /// �{�^������Ă΂�鎖��z�肵�Ă���
    /// </summary>
    public void OnStartButton()
    {
        m_quizSettingPanel.SetActive(false);
        List<int> i = new List<int>();
        foreach (var p in m_prefabs)
            i.Add(p.InputFieldNum);
        GameManager.Instance.QuizStart(i);
    }
}
