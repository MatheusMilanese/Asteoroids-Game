using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    [SerializeField] private Player _player;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textReset;
    [SerializeField] private Text textBestScore;
    [SerializeField] private ParticleSystem explosion;
    private int _score;
    private int bestScore;

    public int score {
        get { return _score; }
        set { _score = value; }
    }

    private void Update() {
        if(_score > bestScore) bestScore = score;
        textBestScore.text = "BEST: " + bestScore.ToString();
        textScore.text = "SCORE: " + _score.ToString();
        if(!_player.isDead) return; // se o player não morreu, nada daqui para baixo é executado

        textReset.gameObject.SetActive(true);
        if(Input.GetKeyDown(KeyCode.R)){
            ResetGame();
        }
    }

    public void ObjectDestroyed(Transform _tranform){
        explosion.transform.position = _tranform.position;
        explosion.Play();
    }

    private void ResetGame(){
        textReset.gameObject.SetActive(false);
        _player.gameObject.SetActive(true);
        _player.transform.position = Vector3.zero;
        _player.isDead = false;
        _score = 0;
    }
}
