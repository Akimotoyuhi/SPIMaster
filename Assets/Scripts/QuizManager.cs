using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] Transform m_buttonParent;
    [SerializeField] Text m_questionText;
    [SerializeField] ChoiseButton m_choiceButtonPrefab;
    private QuizData m_quizData;
    /// <summary>現在出題中のクイズデータ</summary>
    private QuizDataBase m_nowQuizData;

    public void Setup()
    {

    }

    /// <summary>
    /// クイズ開始
    /// </summary>
    public void QuizStart(QuizData quizData, int questionNum)
    {
        m_quizData = quizData;

        //出題画面の表示　とりあえず
        m_nowQuizData = quizData.Databases[0];
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
