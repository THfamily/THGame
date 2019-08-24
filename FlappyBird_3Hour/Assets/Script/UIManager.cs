using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTone<UIManager>
{
    [SerializeField]
    private GameObject _Title = null;
    
    [SerializeField]
    private Button _startButton = null;
    [SerializeField]
    private Button _tipButton = null;

    [SerializeField]
    private NumbersRenderer _scoreRenderer = null;
   
    [SerializeField]
    private GameOverPopup _gameOverPopup = null;

    [SerializeField]
    private GameObject _newBesetscore = null;

    private bool _bHighScore = false;



    public int score {
        set {
            _newBesetscore.SetActive(Manager.Instance.IsCurrentBestScore);
            _scoreRenderer.Value = value;
        } }
    public bool highscore { set { _bHighScore = value; } }


    public int[] Records
    {
        set
        {
            _gameOverPopup._Records[0].text = "2. " + value[0].ToString();
            _gameOverPopup._Records[1].text = "3. " + value[1].ToString();
            _gameOverPopup._Records[2].text = "4. " + value[2].ToString();
            _gameOverPopup._Records[3].text = "5. " + value[3].ToString();

        }
    }


    public void Init()
    {
        _Title.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
        _tipButton.gameObject.SetActive(false);
        _scoreRenderer.gameObject.SetActive(false);        
        _gameOverPopup.gameObject.SetActive(false);
        _newBesetscore.gameObject.SetActive(false);
    }

    void Start()
    {
        Init();
        ShowTitle();
    }

    public void ShowTitle()
    {
        _Title.gameObject.SetActive(true);
        _startButton.gameObject.SetActive(true);
        _newBesetscore.gameObject.SetActive(false);
    }


    public void StartButton()
    {
        _Title.SetActive(false);
        _startButton.gameObject.SetActive(false);
        ShowTipButton();
        SoundManager.instance.BackGround_Music_Start();


    }


    public void TipButton()
    {
        ShowScore();
        Manager.Instance.isPlay = true;
        _tipButton.gameObject.SetActive(false);
       
    }

    public void ShowTipButton()
    {
        _tipButton.gameObject.SetActive(true);
    }

    public void ShowScore()
    {


        score = 0;
        _scoreRenderer.gameObject.SetActive(true);
        _newBesetscore.gameObject.SetActive(false);
    }

    public void InvokeGameover()
    {    _gameOverPopup.Show();
        _scoreRenderer.gameObject.SetActive(false);
        _newBesetscore.gameObject.SetActive(false);
    }
}
