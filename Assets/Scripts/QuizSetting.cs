using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>クイズ設定画面の管理</summary>
public class QuizSetting : MonoBehaviour
{
    //[SerializeField] GameObject m_quizSettingPanel;
    [SerializeField] SettingPrefab m_settingPrefab;
    [SerializeField] Transform m_settingPrefabParent;
    private List<SettingPrefab> m_prefabs = new List<SettingPrefab>();

    public void Setup(List<string> sheetNames)
    {
        //設定項目生成
        foreach (var item in sheetNames)
        {
            SettingPrefab s = Instantiate(m_settingPrefab);
            s.transform.SetParent(m_settingPrefabParent, false);
            s.Setup(item);
            m_prefabs.Add(s);
        }
    }

    /// <summary>
    /// 開始ボタンが押された時<br/>
    /// ボタンから呼ばれる事を想定している
    /// </summary>
    public void OnStartButton()
    {
        List<int> i = new List<int>();
        foreach (var p in m_prefabs)
            i.Add(p.InputFieldNum);
        GameManager.Instance.QuizStart(i);
    }
}
