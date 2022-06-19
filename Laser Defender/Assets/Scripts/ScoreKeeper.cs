using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int score {get; private set;}

    public int UpdateScore(int points)
    {
        score += points;
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
