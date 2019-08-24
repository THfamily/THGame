using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalRenderer : MonoBehaviour
{
    [Range(0, 5)]
    private int _value = 0;

    [SerializeField]
    private Image _image = null;

    [SerializeField]
    private Sprite[] _sprites = new Sprite[4];
    [SerializeField]
    public Text Medal_Info;

    public int Value
    {
        get { return _value; }

        set
        {
            int rank = GetRank(value);
            _value = rank;
            Render();
        }
    }

    private void Render()
    {
        if (0 <= _value && _value < _sprites.Length)
        {
            _image.sprite = _sprites[_value];
        }
    }

    private int GetRank(int score)
    {
        if ( 20 < score)
        {
            Medal_Info.text ="PLATINUM";
            return 1;
        }
        else if ( 15 < score)
        {
            Medal_Info.text = "GOLD";
            return 2;
        }
        else if ( 10 < score)
        {
            Medal_Info.text = "SILVER";
            return 3;
        }
        else if ( 5 < score)
        {
            Medal_Info.text = "BLONZE";
            return 4;
        }
        else
        {
            Medal_Info.text = "";
            return 0;
        }
    }
}