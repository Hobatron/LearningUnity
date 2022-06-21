using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private AudioPlayer audioPlayerSource;
    private ScoreKeeper scoreKeeper;

    private void Awake() {
        audioPlayerSource = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        audioPlayerSource.GetComponent<AudioSource>().pitch = 1;
        scoreKeeper.ResetScore();
        audioPlayerSource.StopMyCoroutine();
        SceneManager.LoadScene("GameScene");
    }
    public void LoadMainMenu()
    {
        audioPlayerSource.GetComponent<AudioSource>().pitch = 1;
        audioPlayerSource.StopMyCoroutine();
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitLoadingScene("GameOver", 5f));
    }

    IEnumerator WaitLoadingScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }    
}
