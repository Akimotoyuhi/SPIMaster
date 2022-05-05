using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

//�f�[�^�̒��g�m�F�p
//https://script.google.com/macros/s/AKfycbznFRos-8O_iZZn2jFIy3lc3QeiLSqsyUxRJj802PmaGbwVL6o27VCchxGcuGyhUDl5/exec
public class GameManager : MonoBehaviour
{
    //�Q�l�T�C�g�@https://qiita.com/simanezumi1989/items/32436230dadf7a123de8
    [SerializeField] string m_url;

    private void Start()
    {
        StartCoroutine(ReadGS());
    }

    private IEnumerator ReadGS()
    {
        UnityWebRequest request = UnityWebRequest.Get(m_url);
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log(request.error);
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

    private List<string> ConvartStr(string text)
    {
        List<string> ret = new List<string>();
        StringReader sr = new StringReader(text);
        sr.ReadLine();//�����񂩂��s�Âǂݍ���
        while (sr.Peek() != -1)
        {
            string line = sr.ReadLine();
            ret.Add(line);
        }
        return ret;
    }
}
