using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;
using UniRx;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class QuizDataList
    {
        //スプレッドシートURL
        //https://docs.google.com/spreadsheets/d/1QPVKyW4J8wxgl7MFkT_2_T6TbnBQL_HgzBiCTIGkPDs/edit#gid=0
        //データの中身確認用
        //https://script.google.com/macros/s/AKfycbznFRos-8O_iZZn2jFIy3lc3QeiLSqsyUxRJj802PmaGbwVL6o27VCchxGcuGyhUDl5/exec
        [SerializeField] string m_sheetName;
        [SerializeField] string m_url;
        [SerializeField] QuizData m_quizdata;
        //private string m_sheetName;

        public QuizData QuizData => m_quizdata;
        public string SheetName => m_sheetName;

        /// <summary>
        /// スプレッドシートからデータを読み込む<br/>
        /// 参考サイト　https://qiita.com/simanezumi1989/items/32436230dadf7a123de8
        /// </summary>
        public async void ReadGSAsync(string sheetName)
        {
            UnityWebRequest request = UnityWebRequest.Get(m_url);
            await request.SendWebRequest();
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
                Debug.Log($"{sheetName}のデータ読み込み完了");
            }
        }
    }
    [SerializeField] QuizManager m_quizManager;
    [SerializeField] QuizSetting m_quizSetting;
    [SerializeField] Text m_countDownText;
    [SerializeField] List<QuizDataList> m_datas;
    [SerializeField] GameObject m_quizSettingPanel;
    [SerializeField] GameObject m_quizPanel;
    /// <summary>ゲームの状態</summary>
    private ReactiveProperty<GameState> m_gameState = new ReactiveProperty<GameState>();
    private IConnectableObservable<int> m_countDownTimerObservable;
    public IObservable<int> CountDownTimer => m_countDownTimerObservable.AsObservable();
    public IObservable<GameState> GameStateSubject => m_gameState;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_quizManager.Setup();
        List<string> list = new List<string>();
        m_datas.ForEach(d => list.Add(d.SheetName));
        m_quizSetting.Setup(list);
        m_countDownText.gameObject.SetActive(true);
        //3秒カウントダウンするストリーム作成　publishでhot変換
        m_countDownTimerObservable = CountDownTimerObservable(3).Publish();
        m_countDownTimerObservable
            .Subscribe((time) => m_countDownText.text = time.ToString(), 
            () => m_quizManager.QuizStart(1));
        m_countDownText.gameObject.SetActive(false);
        GameStateSubject.Subscribe(s => SetPanel(s)).AddTo(this);
        m_gameState.Value = GameState.QuizSetting;
    }

    /// <summary>
    /// クイズ開始
    /// </summary>
    /// <param name="nums"></param>
    public void QuizStart(List<int> nums)
    {
        List<QuizDataBase> d = new List<QuizDataBase>();
        for (int i = 0; i < nums.Count; i++)
        {
            List<int> vs = ToNumListNoCover(m_datas[i].QuizData.Databases.Count, nums[i]);
            foreach (var v in vs)
            {
                d.Add(m_datas[i].QuizData.Databases[v]);
            }
        }
        m_quizManager.SetQuizDatas = d;
        //Connectでカウント開始
        m_countDownTimerObservable.Connect();
    }

    /// <summary>
    /// 被りなしの整数の配列を返す
    /// </summary>
    /// <param name="range">範囲</param>
    /// <param name="element">要素数</param>
    /// <returns>被りなしの整数の配列</returns>
    private List<int> ToNumListNoCover(int range, int element)
    {
        List<int> vs = new List<int>();
        for (int i = 0; i < element; )
        {
            int r = UnityEngine.Random.Range(0, range);
            bool b = true;
            foreach (var v in vs)
            {
                if (v == r)
                    b = false;
            }
            if (b)
            {
                vs.Add(r);
                i++;
                if (i >= range)//回った回数がrange以上なら抜ける
                {
                    return vs;
                }
            }
        }
        return vs;
    }

    /// <summary>
    /// カウントダウンタイマー<br/>
    /// 参考 https://qiita.com/toRisouP/items/581ffc0ddce7090b275b
    /// </summary>
    /// <param name="time">待機時間</param>
    /// <returns></returns>
    private IObservable<int> CountDownTimerObservable(int time)
    {
        return Observable
            //実行間隔　0秒目から1秒間隔で実行
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            //timeから実行時間分(x)引く　long型なのでキャストする必要有
            .Select(x => (int)(time - x))
            //0秒経過の場合はOnNext、0になったらOnComplete
            .TakeWhile(x => x > 0);
    }

    /// <summary>
    /// クイズデータの更新<br/>
    /// エディタのボタンから呼ばれる事を想定している
    /// </summary>
    public void MasterDataUpdate()
    {
        for (int i = 0; i < m_datas.Count; i++)
        {
            m_datas[i].ReadGSAsync(m_datas[i].SheetName);
        }
    }

    private void SetPanel(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.QuizSetting:
                m_quizSettingPanel.SetActive(true);
                m_quizPanel.SetActive(false);
                break;
            case GameState.Quiz:
                m_quizSettingPanel.SetActive(false);
                m_quizPanel.SetActive(true);
                break;
        }
    }
}

public class MasterDataClass<T>
{
    public T[] Data;
}

[Serializable]
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

public enum GameState
{
    QuizSetting,
    Quiz,
}