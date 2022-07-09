using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>�N�C�Y��ʂ̊Ǘ�</summary>
public class QuizManager : MonoBehaviour
{
    [SerializeField] GameObject m_quizPanel;
    [SerializeField] Transform m_buttonParent;
    [SerializeField] Text m_questionText;
    [SerializeField] ChoiseButton m_choiceButtonPrefab;
    //private QuizData m_quizData;
    private List<QuizDataBase> m_quizdatas;
    /// <summary>���ݏo�蒆�̃N�C�Y�f�[�^</summary>
    private QuizDataBase m_nowQuizData;
    public List<QuizDataBase> SetQuizDatas { set => m_quizdatas = value; }

    public void Setup()
    {
        m_quizPanel.SetActive(false);
        //m_quizData = quizData;
    }

    /// <summary>
    /// �N�C�Y�J�n
    /// </summary>
    public void QuizStart(int questionNum)
    {
        m_quizPanel.SetActive(true);
        //�o���ʂ̕\���@�Ƃ肠����1�₾��
        m_nowQuizData = m_quizdatas[0];
        m_questionText.text = m_nowQuizData.Sentence + "\n" + m_nowQuizData.Question;
        for (int i = 0; i <= m_nowQuizData.Choices.Length; i++)
        {
            ChoiseButton b = Instantiate(m_choiceButtonPrefab);
            b.transform.SetParent(m_buttonParent, false);
            if (i == m_nowQuizData.Choices.Length)
            {
                b.Setup(m_nowQuizData.correct);
                b.OnClick.Subscribe(_ => Answer(true)).AddTo(b);
            }
            else
            {
                b.Setup(m_nowQuizData.Choices[i]);
                b.OnClick.Subscribe(_ => Answer(false)).AddTo(b);
            }
        }
    }

    public void Answer(bool answer)
    {
        string s = answer ? "����" : "�s����";
        Debug.Log(s);
    }
}
