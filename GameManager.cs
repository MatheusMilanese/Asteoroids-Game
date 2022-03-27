using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    [SerializeField] private Player _player;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textReset;

    private int _score;

    public int score {
        set { _score = value; }
    }

    private void ResetGame(){
        textReset.enabled = false;
        _player.gameObject.SetActive(true);
        _player.transform.position = Vector3.zero;
        _player.isDead = false;
        _score = 0;
    }
}
