using UnityEngine;

public class Asteroid : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Sprite[] _asteroidSprites;

    [Header("Propriedades")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float _size;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _minSize;

    public float size {
        get { return _size; }
        set { _size = value; }
    }
    public float maxSize {
        get { return _maxSize; }
    }
    public float minSize {
        get { return _minSize; }
    }

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _gameManager = FindObjectOfType<GameManager>();

        _spriteRenderer.sprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * _size;
        _rigidbody.mass = _size;
    }
    
    public void SetTrajectory(Vector2 direction){
        _rigidbody.AddForce(direction * speed);
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag != "Bullet") return;
        if(size * 0.5f > _minSize){
            Split();
            Split();
        }
        _gameManager.ObjectDestroyed(transform);
        _gameManager.score += Mathf.RoundToInt(_size*10);
        Destroy(gameObject);
    }

    private void Split(){
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid newAsteroid = Instantiate(this, position, transform.rotation);
        newAsteroid.size = size * 0.5f;
        newAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
