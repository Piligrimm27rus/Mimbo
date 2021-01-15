using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;

    public static GameManager instance;

    int _score;
    public int score
    {
        get { return _score; }
        set { _score = value;}
    }

    void Start()
    {
        instance = this;
    }

    public void GameOver()
    {

    }
}

enum GameEvent
{
    GameOver,
    InGame
}
