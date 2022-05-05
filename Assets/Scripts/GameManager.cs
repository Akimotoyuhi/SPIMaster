using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizDataList
    {
        //�f�[�^�̒��g�m�F�p
        //https://script.google.com/macros/s/AKfycbznFRos-8O_iZZn2jFIy3lc3QeiLSqsyUxRJj802PmaGbwVL6o27VCchxGcuGyhUDl5/exec
        [SerializeField] string m_url;
        [SerializeField] QuizData m_quizdata;

        public QuizData QuizData => m_quizdata;

        /// <summary>
        /// �X�v���b�h�V�[�g����f�[�^��ǂݍ���<br/>
        /// �Q�l�T�C�g�@https://qiita.com/simanezumi1989/items/32436230dadf7a123de8
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
                List<string> vs = ConvartStr(request.downloadHandler.text);
                foreach (var s in vs)
                {
                    Debug.Log(s);
                }
            }
        }

        /// <summary>
        /// ������ɕϊ�<br/>
        /// �Q�l�T�C�g�@https://qiita.com/simanezumi1989/items/32436230dadf7a123de8
        /// </summary>
        private List<string> ConvartStr(string text)
        {
            List<string> ret = new List<string>();
            StringReader sr = new StringReader(text);
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();//�����񂩂��s�Âǂݍ���
                ret.Add(line);
            }
            return ret;
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
