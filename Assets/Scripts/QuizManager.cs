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
    /// <summary>���ݏo�蒆�̃N�C�Y�f�[�^</summary>
    private QuizDataBase m_nowQuizData;

    public void Setup()
    {

    }

    /// <summary>
    /// �N�C�Y�J�n
    /// </summary>
    public void QuizStart(QuizData quizData, int questionNum)
    {
        m_quizData = quizData;

        //�o���ʂ̕\���@�Ƃ肠����
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
                    Debug.Log("����");
                });
            }
            else
            {
                b.Setup(m_nowQuizData.Choices[i], () =>
                {
                    Debug.Log("�s����");
                });
            }
        }
    }

}
