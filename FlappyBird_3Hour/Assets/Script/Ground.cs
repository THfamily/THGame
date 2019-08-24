using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MoveObject
{
    private Vector3 _startPosition = Vector3.zero;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void Init()
    {
        transform.position = _startPosition;
    }
    public void SetPosition(float value)
    {
        Vector3 local_vecter = transform.position;
        local_vecter.x = value;
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
            Manager.Instance.RemoveGround(this);
        }
    }
}
