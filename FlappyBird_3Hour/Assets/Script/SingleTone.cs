using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTone<T> : MonoBehaviour where T : SingleTone<T>
{
    private static T  _instance = null;
    public static T Instance { get { return _instance; } }


    void Awake()
    {
        _instance = GetComponent<T>();
    }
}
