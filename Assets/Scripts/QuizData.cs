using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData")]
public class QuizData : ScriptableObject
{
    [SerializeField] List<QuizDataBase> m_databases;
    public List<QuizDataBase> Databases => m_databases;
    public void Setup(List<List<string>> dataList)
    {
        m_databases.Clear();
        foreach (var d in dataList)
        {
            QuizDataBase db = new QuizDataBase();
            db.Setup(d);
            m_databases.Add(db);
        }
    }
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
    public void Setup(List<string> texts)
    {
        m_sentence = texts[0];
        texts.RemoveAt(0);
        m_question = texts[0];
        texts.RemoveAt(0);
        m_correct = texts[0];
        texts.RemoveAt(0);
        m_choices = texts.ToArray();
    }
}
