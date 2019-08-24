using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MoveObject
{
    [SerializeField]
    private GameObject _TopPipe = null;

    private float _defaultTopPositionY = 0.0f;
    private float _defaultBasePositionY = 0.0f;

    private bool _bCheck = false;

    private void Start()
    {
        _defaultTopPositionY = _TopPipe.transform.localPosition.y;
        _defaultBasePositionY = transform.position.y;
    }

    public void SetHeight( float value)
    {
        Vector3 local_vecter = _TopPipe.transform.localPosition;
        local_vecter.y = value + _defaultTopPositionY;
        _TopPipe.transform.localPosition = local_vecter;
    }
    public void SetPosition( float value)
    {
        Vector3 local_vecter = transform.position;
        //local_vecter.y = _defaultBasePositionY + value;
        local_vecter.y = value;
        transform.position = local_vecter;
    }

    override public void GameUpdate()
    {
        base.GameUpdate();
    }

    override protected void FinishEndPosition()
    {
        if (this != null)
        {
            Manager.Instance.Remove(this);
        }
    }
    /// <summary>
    /// 
    /// brid 와 위치를 검사하는 처리
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool isNeedInvokeScoreCheck(Vector3 target)
    {
        if( !_bCheck)
        {
            if (transform.position.x <= target.x)
            {
                _bCheck = true;
                return true;
            }
            
        }
        return false;

    }

}
