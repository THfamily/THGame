using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, IGameObject
{
    [SerializeField]
    private Rigidbody _rigidbody = null;

    [SerializeField]
    private float _jumpValue = 10.0f;

 
    public AudioClip Jump;
    public AudioClip Hit;

   // [SerializeField]
    //public AudioSource player_effect = null;


    private Vector3 _startPosition = Vector3.zero;
    private Quaternion _startRotation = Quaternion.identity;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        //player_effect.Stop();
        //player_effect.loop = false;
        //player_effect.clip = Jump;
        
    }

    public void Init()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        gameObject.GetComponent<Animator>().Play("char_alive");
        //player_effect.Stop();
        //player_effect.loop = false;
       // player_effect.clip = Jump;
    }

    public void FreezeAll(bool value)
    {
        _rigidbody.constraints = value ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }
    // Start is called before the first frame update
    void Start()
    {
       // player_effect = gameObject.AddComponent<AudioSource>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    public void GameUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("Input KeyCode.Mouse0 ");
            Vector2 Vector2 = new Vector2(0, _jumpValue);
            _rigidbody.AddForce(Vector2);
            SoundManager.instance.PlaySingle(Jump);
           // player_effect.PlayOneShot(Jump);
            //_rigidbody.AddForce(new Vector3(0, _jumpValue, 0));

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Animator>().Play("Dead_sheep");
        Debug.Log(collision.gameObject.tag);
        switch(collision.gameObject.tag)
        {
            case "Enemy" :

                //player_effect.PlayOneShot(Hit);
                SoundManager.instance.BackGround_Music_Stop();
                SoundManager.instance.PlaySingle(Hit);
                
                Manager.Instance.isPlay = false;
            break;

        }
    }

}
