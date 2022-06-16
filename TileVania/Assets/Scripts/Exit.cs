using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float time = 2f;

    private void OnTriggerEnter2D(Collider2D other) {
        var scene = SceneManager.GetActiveScene();
        if (other.tag == "Player") {
            StartCoroutine(ExitLevel(scene.buildIndex));
        }
    }

    IEnumerator ExitLevel(int currentSceneIndex)
    {
        yield return new WaitForSecondsRealtime(time);
        Debug.Log($"Attempting to load scene {currentSceneIndex + 1}");
        SceneManager.LoadScene(currentSceneIndex + 1);
        FindObjectOfType<ScenePersist>().DestroyScenePersist();
    }
}