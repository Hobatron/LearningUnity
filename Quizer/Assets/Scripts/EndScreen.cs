using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Score score;
    public void ShowFinalScore()
    {
        var finalScore = score.realCalc();
        scoreText.text = $"Congratulations! You scored {finalScore}%";
    }
}
