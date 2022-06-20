using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider healthSlider;

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString("000000000");
    }

    public void SetHealthText(int health)
    {
        healthSlider.value = health;
    }

}
