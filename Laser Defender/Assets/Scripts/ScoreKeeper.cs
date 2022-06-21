using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private UIDisplay uiDisplay;
    static ScoreKeeper instance;

    public int score {get; private set;} = 0;

    private void Awake() 
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        instance.uiDisplay = FindObjectOfType<UIDisplay>();
    }

    public int UpdateScore(int points)
    {
        score += points;
        updateScoreUi();
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        updateScoreUi();
    }

    private void updateScoreUi()
    {
        if (uiDisplay != null)
        {
            uiDisplay.SetScoreText(score);
        }
    }
}
