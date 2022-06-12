using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Invoke("ResetScene", 1f);
        }
    }
    private void ResetScene() 
    {
        Debug.Log("Bonk!");
        SceneManager.LoadScene(0);
    }
}
