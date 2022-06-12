using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = .7f;
    public Rigidbody2D rigidBody { get; private set; }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigidBody.AddTorque(-Input.GetAxis("Horizontal") * torqueAmount);
    }
}
