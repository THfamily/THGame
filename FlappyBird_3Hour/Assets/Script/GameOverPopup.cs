using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField]
    private MedalRenderer _medalRenderer = null;

    [SerializeField]
    private NumbersRenderer _scoreRenderer = null;

    [SerializeField]
    private NumbersRenderer _bestRenderer = null;

    [SerializeField]
    private GameObject _newBestscore = null;

    [SerializeField]
    public Text[] _Records = new Text[4];

    public void Show()
    {
        gameObject.SetActive(true);
        _newBestscore.SetActive(Manager.Instance.IsCurrentBestScore);
        _medalRenderer.Value = Manager.Instance.Score;
        _scoreRenderer.Value = Manager.Instance.Score;
        _bestRenderer.Value = Manager.Instance.BestScore;
        //_Records[0].text = ToString( Manager.Instance.GetScores);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OkButton()
    {
        Manager.Instance.Replay();
        gameObject.SetActive(false);
        SoundManager.instance.BackGround_Music_Start();

    }
}
