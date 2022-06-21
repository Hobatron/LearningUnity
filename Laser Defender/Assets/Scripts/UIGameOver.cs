using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    void Start()
    {
        scoreText.text = FindObjectOfType<ScoreKeeper>().score.ToString();
    }
}
