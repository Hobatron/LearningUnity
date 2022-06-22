using UnityEngine;

public class ChargerScript : MonoBehaviour
{
    [SerializeField] float mySpeed = 10f;
    [SerializeField] float rotationSpeed = 50f;
    private Player player;
    private Vector3 lastKnownLocation;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    
    private void Update() {
        HuntPlayer();
    }

    private void HuntPlayer()
    {
        RotateCharger();
        transform.position += -transform.up * mySpeed * Time.deltaTime;
    }

    private void RotateCharger()
    {
        lastKnownLocation = player != null ? player.transform.position : lastKnownLocation;
        Vector3 direction = transform.position - lastKnownLocation;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle*-1));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
