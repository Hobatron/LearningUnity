using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    private int playerLives = 3;
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

    public void ProcessPlayerDeath()
    {
        deathCanvasScript = FindObjectOfType<DeathCanvas>();
        playerLives--;
        if (playerLives >= 1)
        {
            deathCanvasScript.FadeIn(playerLives, RestartLevel);
        }
        else
        {
            deathCanvasScript.FadeIn(playerLives, RestartGame);
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RestartGame()
    {
        playerLives = 3;
        SceneManager.LoadScene(0);
    }
}
