using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<QuestionSO> questions;
    QuestionSO currentQuestion;
    [SerializeField] TextMeshProUGUI questionText;
    [Header("Answers")]
    [SerializeField] List<GameObject> answerButtons;
    [Header("Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField] Timer timer;
    [Header("Score")]
    [SerializeField] Score score;
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Slider")]
    [SerializeField] Slider slider;

    int correctAnswerIndex;
    int wrongAnserGuessedIndex;

    public bool isComplete { get; private set; } = false;

    void Start()
    {
        slider.value = 0;
        slider.maxValue = questions.Count;
        SetQuestion();
        SetButtonState(true);
    }

    private void SetQuestion() 
    {
        if (questions.Count > 0)
        {
            slider.value++;
            score.IncQuestionsSeen();
            currentQuestion = questions[Random.Range(0, questions.Count)];
            questionText.text = currentQuestion.GetQuestionText();
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            var i = 0;
            foreach (GameObject button in answerButtons)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
                i++;
            }
        }
        else
        {
            isComplete = true;
        }
    }

    public void GetNextQuestion()
    {
        questions.Remove(currentQuestion);
        SetButtonState(true);
        SetDefaultButtonSprites();
        SetQuestion();
    }

    public void OnAnswerSelected(int index)
    {
        if (index != correctAnswerIndex && index >= 0)
        {
            answerButtons[index].GetComponent<Image>().color = new Color32(229, 96, 96, 255);
        }
        else if (index == correctAnswerIndex)
        {
            score.IncCorrectAnswers();
        }
        SetButtonState(false);
        scoreText.text = $"Score: {score.realCalc()}%";
        timer.CancelTimer();
        answerButtons[correctAnswerIndex].GetComponent<Image>().sprite = correctAnswerSprite;
    }
    
    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
            answerButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }
}
