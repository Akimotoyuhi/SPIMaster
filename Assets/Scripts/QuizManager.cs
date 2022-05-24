using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>クイズ画面の管理</summary>
public class QuizManager : MonoBehaviour
{
    [SerializeField] GameObject m_quizPanel;
    [SerializeField] Transform m_buttonParent;
    [SerializeField] Text m_questionText;
    [SerializeField] ChoiseButton m_choiceButtonPrefab;
    //private QuizData m_quizData;
    private List<QuizDataBase> m_quizdatas;
    /// <summary>現在出題中のクイズデータ</summary>
    private QuizDataBase m_nowQuizData;
    public List<QuizDataBase> SetQuizDatas { set => m_quizdatas = value; }

    public void Setup()
    {
        m_quizPanel.SetActive(false);
        //m_quizData = quizData;
    }

    /// <summary>
    /// クイズ開始
    /// </summary>
    public void QuizStart(int questionNum)
    {
        m_quizPanel.SetActive(true);
        //出題画面の表示　とりあえず1問だけ
        m_nowQuizData = m_quizdatas[0];
        m_questionText.text = m_nowQuizData.Sentence + "\n" + m_nowQuizData.Question;
        for (int i = 0; i <= m_nowQuizData.Choices.Length; i++)
        {
            ChoiseButton b = Instantiate(m_choiceButtonPrefab);
            b.transform.SetParent(m_buttonParent, false);
            if (i == m_nowQuizData.Choices.Length)
            {
                b.Setup(m_nowQuizData.correct, () =>
                {
                    Debug.Log("正解");
                });
            }
            else
            {
                b.Setup(m_nowQuizData.Choices[i], () =>
                {
                    Debug.Log("不正解");
                });
            }
        }
    }
}
