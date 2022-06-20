using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private UIDisplay uiDisplay;

    public int score {get; private set;}

    private void Awake() 
    {
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        uiDisplay = FindObjectOfType<UIDisplay>();
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
        uiDisplay.SetScoreText(score);
    }
}
