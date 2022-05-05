using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData")]
public class QuizData : ScriptableObject
{
    [SerializeField] List<QuizDataBase> m_databases = new List<QuizDataBase>();
    public List<QuizDataBase> Databases => m_databases;

}

[System.Serializable]
public class QuizDataBase
{
    [SerializeField] string m_sentence;
    [SerializeField] string m_question;
    [SerializeField] string m_correct;
    [SerializeField] string[] m_choices;
    public string Sentence => m_sentence;
    public string Question => m_question;
    public string correct => m_correct;
    public string[] Choices => m_choices;
    public void Setup(string sentence, string question, string correct, string[] choices)
    {

    }
}
