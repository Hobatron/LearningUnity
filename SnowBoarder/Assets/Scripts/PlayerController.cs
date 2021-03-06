using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = .7f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 15f;
    public Rigidbody2D rigidBody { get; private set; }
    private bool crashing { get; set; } = false;
    SurfaceEffector2D surfaceEffector2D;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }
    void Update()
    {
        if (!crashing) {
            RespondToBoost();
            rigidBody.AddTorque(-Input.GetAxis("Horizontal") * torqueAmount);
        }
    }

    private void RespondToBoost()
    {
        surfaceEffector2D.speed = Input.GetAxis("Jump") > 0 ? boostSpeed : baseSpeed;
    }

    public void SetCrashing(bool isCrashing)
    {
        crashing = isCrashing;
    }
}
