using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem finishEffect;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            finishEffect.Play();
            Invoke("ResetScene", 1f);
        }
    }

    private void ResetScene() 
    {
        SceneManager.LoadScene(0);
    }
}
