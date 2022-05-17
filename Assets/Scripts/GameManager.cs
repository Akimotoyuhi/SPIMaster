using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizDataList
    {
        //データの中身確認用
        //https://script.google.com/macros/s/AKfycbznFRos-8O_iZZn2jFIy3lc3QeiLSqsyUxRJj802PmaGbwVL6o27VCchxGcuGyhUDl5/exec
        [SerializeField] string m_url;
        [SerializeField] QuizData m_quizdata;

        public QuizData QuizData => m_quizdata;

        /// <summary>
        /// スプレッドシートからデータを読み込む<br/>
        /// 参考サイト　https://qiita.com/simanezumi1989/items/32436230dadf7a123de8
        /// </summary>
        public IEnumerator ReadGSAsync()
        {
            UnityWebRequest request = UnityWebRequest.Get(m_url);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var v = JsonUtility.FromJson<MasterDataClass<QuestionDataList>>(request.downloadHandler.text);
                List<List<string>> list = new List<List<string>>();
                foreach (var d in v.Data)
                {
                    list.Add(d.List);
                }
                m_quizdata.Setup(list);
            }
        }
    }
    [SerializeField] List<QuizDataList> m_data;

    private void Start()
    {
    }

    public void MasterDataUpdate()
    {
        foreach (var item in m_data)
        {
            StartCoroutine(item.ReadGSAsync());
        }
    }
}

public class MasterDataClass<T>
{
    public T[] Data;
}

[System.Serializable]
public class QuestionDataList
{
    public string Sentence;
    public string Question;
    public string Correct;
    public string Choice1;
    public string Choice2;
    public string Choice3;
    public string Choice4;
    public string Choice5;
    public string Choice6;
    public string Choice7;
    public string Choice8;
    public List<string> List => new List<string> { Sentence, Question, Correct, Choice1, Choice2, Choice3, Choice4, Choice5, Choice6, Choice7, Choice8 };
}