using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;

    public static GameManager instance;

    [SerializeField] Text pressToRestartText;
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;
    [SerializeField] Button playButton;

    private int bestScore
    {
        get
        {
            return PlayerPrefs.GetInt("BestScore");
        }
        set
        {
            if (value > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", value);
            }
        }
    }

    int _score;
    public int score
    {
        get { return _score; }
        set { _score = value; }
    }

    public int Money
    {
        get
        {
            return PlayerPrefs.GetInt("Money");
        }
        internal set
        {
            PlayerPrefs.SetInt("Money", value);
        }
    }

    void Start()
    {
        instance = this;

        score = 0;

        switch (OnLoadSettings.gameEvent)
        {
            case GameEvent.GameMenu:
                GameMenu();
                break;
            case GameEvent.GameOver:
                GameOver();
                break;
            case GameEvent.InGame:
                StartGame();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (OnLoadSettings.gameEvent == GameEvent.GameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayButton();
            }
        }
    }

    public void ScoreUpdate()
    {
        scoreText.text = score.ToString();
    }

    public void GameMenu()
    {
        OnLoadSettings.gameEvent = GameEvent.GameMenu;
        playButton.gameObject.SetActive(true);

        pressToRestartText.enabled = false;
        bestScoreText.enabled = false;
        scoreText.enabled = false;
    }

    public void StartGame()
    {
        OnLoadSettings.gameEvent = GameEvent.InGame;

        playButton.gameObject.SetActive(false);

        pressToRestartText.enabled = false;
        bestScoreText.enabled = false;
        scoreText.enabled = true;

        scoreText.text = "0";
    }

    public void GameOver()
    {
        OnLoadSettings.gameEvent = GameEvent.GameOver;

        pressToRestartText.enabled = true;
        bestScoreText.enabled = true;
        scoreText.enabled = true;

        bestScore = score;

        bestScoreText.text = "Best Score: " + bestScore.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    public void PlayButton()
    {
        OnLoadSettings.gameEvent = GameEvent.InGame;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

static class OnLoadSettings
{
    public static GameEvent gameEvent;

    static OnLoadSettings()
    {
        gameEvent = GameEvent.GameMenu;
    }
}

enum GameEvent
{
    GameMenu,
    GameOver,
    InGame
}
