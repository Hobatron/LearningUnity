using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lives;
    [SerializeField] TextMeshProUGUI score;
    private int playerLives = 3;
    private int playerScore = 0;
    private DeathCanvas deathCanvasScript;

    void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    internal void CoinPickUp(int coinValue)
    {
        playerScore += coinValue;
        score.text = playerScore.ToString();
    }

    private void Start() 
    {
        initValues();
    }

    private void initValues()
    {
        playerLives = 3;
        playerScore = 0;
        lives.text = playerLives.ToString();
        score.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        playerLives--;
        lives.text = playerLives.ToString();
        deathCanvasScript = FindObjectOfType<DeathCanvas>();

        if (playerLives >= 1)
        {
            deathCanvasScript.FadeIn(playerLives, RestartLevel);
        }
        else
        {
            deathCanvasScript.FadeIn(playerLives, RestartGame);
        }
    }

    public string[] GetLivesAndScore()
    {
        return new string[] {playerLives.ToString(), playerScore.ToString()};
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RestartGame()
    {
        initValues();
        FindObjectOfType<ScenePersist>().DestroyScenePersist();
        SceneManager.LoadScene(0);
    }
}
