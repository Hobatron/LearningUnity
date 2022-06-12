using UnityEngine;

public class Score : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    float score;
    
    public int GetCorrectAnswer()
    {
        return correctAnswers;
    }
    public void IncCorrectAnswers()
    {
        correctAnswers++;
    }
    public void IncQuestionsSeen()
    {
        questionsSeen++;
    }
    public int GetQuestionSeen()
    {
        return questionsSeen;
    }

    public float realCalc()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
