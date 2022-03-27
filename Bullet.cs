using UnityEngine;

public class Bullet : MonoBehaviour {
    private Rigidbody2D _rigidbody;

    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction){
        _rigidbody.AddForce(direction * speed);
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
