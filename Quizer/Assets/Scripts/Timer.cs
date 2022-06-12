using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToComplete = 15f;
    [SerializeField] float timeToShowAnswer = 5f;
    [SerializeField] Quiz QuizCanvas;
    public bool isAnsweringQuestion = true;
    float timerValue;

    void Start() {
        timerValue = timeToComplete;
    }
    void Update()
    {
        updateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void updateTimer()
    {

        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            GetComponentInChildren<Image>().fillAmount = timerValue / timeToComplete;
        }
        else
        {
            GetComponentInChildren<Image>().fillAmount = timerValue / timeToShowAnswer;
        }
        if (timerValue <= 0) 
        {
            if (isAnsweringQuestion)
            {
                isAnsweringQuestion = false;
                QuizCanvas.OnAnswerSelected(-1);
                timerValue = timeToShowAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToComplete;
                QuizCanvas.GetNextQuestion();
            }
        }
    }
}
