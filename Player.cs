using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField] private Bullet bulletPrefab;
    private Rigidbody2D _rigidbody;

    private bool _thrusting;
    private float _turnDirection;

    [SerializeField] private float _thrustSpeed;
    [SerializeField] private float _turnSpeed;

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

    void toShoot(){
        Bullet newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        newBullet.Project(transform.up);
    }

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
}
