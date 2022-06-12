using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem headTrauma;
    [SerializeField] AudioClip crashSFX;
    private bool crashPlayed = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Ground" )
        {
            headTrauma.Play();
            Invoke("ResetScene", 1f);
            if (!crashPlayed) {
                GetComponent<AudioSource>().PlayOneShot(crashSFX);
                crashPlayed = true;
            }
        }
    }
    private void ResetScene() 
    {
        FindObjectOfType<PlayerController>().SetCrashing(true);
        Debug.Log("Bonk!");
        SceneManager.LoadScene(0);
    }
}
