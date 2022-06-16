using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class DeathCanvas : MonoBehaviour
{
    private CanvasGroup deathCanvas;
    [SerializeField] private float time = .01f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private TextMeshProUGUI lifeCounter;
    private bool fading;
    private Action restartCallback;
    private bool canFadeOut;

    void Start() {
        deathCanvas = GetComponent<CanvasGroup>();
    }

    public void OnClick(InputValue input)
    {
        if (!fading && canFadeOut)
        {
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeOut()
    {
        return FadeOutCanvas();
    }

    public void FadeIn(int? lives = null, Action callBack = null)
    {
        switch (lives)
        {
            case 2: 
                lifeCounter.SetText("\"I think I can go a few more times...\"");
                break;
            case 1: 
                lifeCounter.SetText("\"This is my last chance...\"");
                break;
            default:
                lifeCounter.SetText("\"Wait... What is happening?\"");
                break;
        }
        restartCallback = callBack;
        canFadeOut = true;
        StartCoroutine(FadeInCanvas());
    }

    IEnumerator FadeInCanvas()
    {
        fading = true;
        while (maxAlpha > deathCanvas.alpha)
        {
            yield return new WaitForSecondsRealtime(time);
            deathCanvas.alpha += .01f;
        }
        fading = false;
    }

    IEnumerator FadeOutCanvas()
    {
        fading = true;
        float maxCounter = maxAlpha;
        while (maxCounter >= Mathf.Epsilon)
        {
            yield return new WaitForSecondsRealtime(time);
            deathCanvas.alpha = maxCounter;
            maxCounter += -.01f;
        }
        fading = false;
        
        if (restartCallback != null)
        {
            restartCallback();
        }
    }
}
