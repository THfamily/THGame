using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour, IGameObject
{
    [SerializeField]
    private float _startPositionX = 0.5f;
    [SerializeField]
    private float _endPositionX = -0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    virtual public void GameUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (transform != null)
        {
            Vector3 position = transform.position;
            position.x -= Manager.Instance.Speed;
            if (position.x < _endPositionX)
            {
                FinishEndPosition();
            }
            else
            {
                transform.position = position;
            }
        }

    }

    virtual protected void FinishEndPosition()
    {
        if (transform != null)
        {
            Vector3 position = transform.position;
            position.x = _startPositionX;
            transform.position = position;
        }
    }
}
