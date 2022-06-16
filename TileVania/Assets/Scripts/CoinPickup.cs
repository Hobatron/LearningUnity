using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] int coinValue;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameSession>().CoinPickUp(coinValue);
            var pos = gameObject.transform.position;
            AudioSource.PlayClipAtPoint(clip, pos);
            Destroy(gameObject);
        }
    }
}
