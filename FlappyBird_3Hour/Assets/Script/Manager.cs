using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : SingleTone<Manager>
{

   

    [SerializeField]
    private Bird _bird = null;
    [SerializeField]
    private Ground _ground = null;
    [SerializeField]
    private Pipe _pipe = null;

    [SerializeField]
    private float _speed = 0.05f;



    [SerializeField]
    private float _createTime = 1.0f;

    [SerializeField]
    private float _pipeRandomPositionY = 3.5f;

    [SerializeField]
    private float _pipeRandomHeight = 2.5f;

    [SerializeField]
    private float _GroundPositonX = 12;

    [SerializeField]
    private float _GroundGenTime = 12.6f;

    private float _Ground_Move_count = 0.0f;


    private float _curruntTime = 0.0f;

    private float _playTime = 0.0f;


    private float _PreBlockPositonY = 0;


    private List<Pipe> _pipeList = new List<Pipe>();

    private List<Ground> _GroundList = new List<Ground>();

    private bool _bPlay = false;
    private int _score = 0;
    private int _bestScore = 0;

    private int[] _ScoreRecords = new int[4];

    private bool bCurrentBestScore = false;

    private bool  score_check = true;

    public float Speed { get => _speed; }
    public bool isPlay
    {
        get => _bPlay;
        set
        {
            _bPlay = value;
            if( !_bPlay)
            {
                UIManager.Instance.InvokeGameover();
            }
        }
    }

    public int Score { get => _score; }
    public int BestScore { get => _bestScore; }
    public bool IsCurrentBestScore { get => bCurrentBestScore; }
    public int[] GetScores { get => _ScoreRecords; }
    private void Start()
    {
        Init();
        UIManager.Instance.ShowTitle();
       
    }

    private void Init()
    {
        _bestScore = PlayerPrefs.GetInt("_bestScore");
        _ScoreRecords[0] = PlayerPrefs.GetInt("2nd_Score");
        _ScoreRecords[1] = PlayerPrefs.GetInt("3nd_Score");
        _ScoreRecords[2] = PlayerPrefs.GetInt("4nd_Score");
        _ScoreRecords[3] = PlayerPrefs.GetInt("5nd_Score");

        bCurrentBestScore = false;
        _bPlay = false;
        _score = 0;
        _curruntTime = 0;
        _playTime = 0;
        _speed = 0.05f;
        _PreBlockPositonY = 0;
        _bird.Init();
        //_ground.Init();


        //_pipeList.ForEach(x => Remove(x));
        // _pipeList.RemoveAll(s => s.gameObject);
        
        
        for (int i = 0; i <= _pipeList.Count; i++)
        {
            try
            {
                if (_pipeList[0] != null)
                {                   
                    Remove(_pipeList[0]);
                }
            }catch
            {
                
                  Debug.Log("wrong delete");
                
            }
            //_pipeList.ForEach(x => Remove(x));
            //_pipeList.Clear();
        }
        


        UIManager.Instance.Init();
    }
    
    public void Replay()
    {
        Init();
        UIManager.Instance.ShowScore();
        _bPlay = true;
    }

  
    // Update is called once per frame
    void Update()
    {
        /*
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                //game exit
                Time.timeScale = 0;
                Application.Quit();
            }
        }
        */
        _bird.FreezeAll(!_bPlay);       
        if (_bPlay)
        {
            score_check = false;
            _Ground_Move_count += _speed;
            _playTime += Time.deltaTime;

            if (_GroundGenTime <= _Ground_Move_count)
            {
                _Ground_Move_count = 0;
                _ground.SetPosition(_GroundPositonX);
                _GroundList.Add(GameObject.Instantiate(_ground));
            }
            else
            {
                if(_GroundList.Count == 0 )
                {
                    _Ground_Move_count = 0;
                    _ground.SetPosition(_GroundPositonX);
                    _GroundList.Add(GameObject.Instantiate(_ground));
                }
            }

            _curruntTime += Time.deltaTime;
            if(_createTime < _curruntTime)
            {
                if (_playTime > 8)
                {
                    if (_speed < 0.1)
                    {
                        _speed += 0.0025f;
                    }
                }

                _curruntTime = 0;
                float random_Height_Min = 0.0f;
                float random_Height_Max = _pipeRandomHeight;
                float random_Pos_Min = -1.0f;
                float random_Pos_Max = _pipeRandomPositionY;
                /*
                if (_playTime < 5)
                {
                    random_Height_Min = 0.0f;
                    random_Pos_Min = 0.0f;
                    random_Height_Max = 3;
                    random_Pos_Max    = 3;
                }
                else if (_playTime < 10)
                {
                    random_Height_Min = 0.0f;
                    random_Pos_Min = 0.0f;
                    random_Height_Max = 2;
                    random_Pos_Max = 4;
                }
                else
                {
                    random_Height_Min = 0.0f;
                    random_Pos_Min = 0.0f;
                    random_Height_Max = 0;
                    random_Pos_Max = 5;
                    
                }
                */
                _pipe.SetHeight(Random.Range(random_Height_Min, random_Height_Max));
                _pipe.SetPosition(Random.Range(random_Pos_Min, random_Pos_Max));
                if( Mathf.Abs(_PreBlockPositonY - _pipe.gameObject.transform.position.y) > 3)
                {
                    Debug.Log("PreInterval over process");
                    if( (_PreBlockPositonY + 2) >= random_Pos_Max)
                    {
                        _pipe.SetPosition(_PreBlockPositonY - 2);
                    }
                    else
                    {
                        _pipe.SetPosition(_PreBlockPositonY + 2);
                    }
                    
                }
                _PreBlockPositonY = _pipe.gameObject.transform.position.y;
                _pipeList.Add(GameObject.Instantiate(_pipe));

               
            }


            _bird.GameUpdate();
           // _ground.GameUpdate();
            for (int i = 0; i < _pipeList.Count; i++)
            {
                _pipeList[i].GameUpdate();
                if (_pipeList[i].isNeedInvokeScoreCheck(_bird.transform.position))
                {
                    InvokeScore();
                }

            }
            for (int i = 0; i < _GroundList.Count ; i++)
            {
                
                _GroundList[i].GameUpdate();
            }

            /*
                _pipeList.ForEach(
                (x) =>
            {
              
                    x.GameUpdate();

                    if (x.isNeedInvokeScoreCheck(_bird.transform.position))
                    {
                        InvokeScore();
                    }
                
            } );
            */

        }
        else
        {
            if (!score_check)
            {
                score_check = true;
                if (_bestScore < _score)
                {
                    //bCurrentBestScore = true;

                    for (int j = 3; j > 0; j--)
                    {
                        _ScoreRecords[j] = _ScoreRecords[j - 1];
                        Debug.Log(_ScoreRecords[j] + "Rank");
                    }
                    _ScoreRecords[0] = _bestScore;

                    _bestScore = _score;
                    UIManager.Instance.highscore = true;

                    //    PlayerPrefs.Save();
                }
                /*
                else
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        if (_ScoreRecords[i] <= _score)
                        {
                            for (int j = 3; j > i; j--)
                            {
                                _ScoreRecords[j] = _ScoreRecords[j - 1];
                            }
                            _ScoreRecords[i] = _score;
                           // bCurrentBestScore = true;
                            Debug.Log(_ScoreRecords[i] + "Rank");
                            break;
                        }
                    }
                }
               */

                UIManager.Instance.Records = _ScoreRecords;
                PlayerPrefs.SetInt("_bestScore", _bestScore);
                /*
                PlayerPrefs.SetInt("2nd_Score", _ScoreRecords[0]);
                PlayerPrefs.SetInt("3nd_Score", _ScoreRecords[1]);
                PlayerPrefs.SetInt("4nd_Score", _ScoreRecords[2]);
                PlayerPrefs.SetInt("5nd_Score", _ScoreRecords[3]);
                */
                PlayerPrefs.Save();
            }
        }

    }

    public void Remove( Pipe target)
    {
        if (target != null)
        {
            
           // Debug.Log(target.gameObject.name + " / " + _pipeList.Count);
            DestroyImmediate(target.gameObject);
            _pipeList.Remove(target);
        }

    }
    public void RemoveGround(Ground target)
    {
        if (target != null)
        {
            DestroyImmediate(target.gameObject);
            _GroundList.Remove(target);
        }
    }
    private void InvokeScore()
    {
        _score++;

        if(_bestScore < _score)
        {
            bCurrentBestScore = true;
            //_bestScore = _score;
            UIManager.Instance.highscore = true;

  
        //    PlayerPrefs.Save();
        }
        

        UIManager.Instance.score = _score;
        //UIManager.Instance.Records = _ScoreRecords;
        //Debug.Log(_score);
    }
}
