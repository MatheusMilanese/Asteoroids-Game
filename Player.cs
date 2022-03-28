using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    private Rigidbody2D _rigidbody;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float _thrustSpeed;
    [SerializeField] private float _turnSpeed;

    private bool _isDead = false;
    private bool _thrusting;
    private float _turnDirection;

    public bool isDead {
        get { return _isDead; }
        set { _isDead = value; }
    }

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        OnInput();
        if(Input.GetKeyDown(KeyCode.Space)){
            toShoot();
        }
    }

    private void FixedUpdate() {
        OnMove();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag != "Asteroid") return;
        FindObjectOfType<GameManager>().ObjectDestroyed(transform);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = 0.0f;
        _isDead = true;
        gameObject.SetActive(false);
    }

    void toShoot(){
        Bullet newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        newBullet.Project(transform.up);
    }

    #region Movement
    void OnInput(){
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection = 1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
        }
        else {
            _turnDirection = 0.0f;
        }
    }

    void OnMove(){
        if(_thrusting){
            _rigidbody.AddForce(transform.up * _thrustSpeed);
        }

        if(_turnDirection != 0.0f){
            _rigidbody.AddTorque(_turnDirection * _turnSpeed);
        } 
    }
    #endregion
}
