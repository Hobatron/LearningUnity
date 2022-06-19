using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float mySpeed = 7f;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    [SerializeField] DamageAnimations damageAnimations;
    Shooter shooter;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    private bool isFiring;
    private void Awake() 
    {
        shooter = GetComponent<Shooter>();
    }

    void Start() 
    {
        InitBounds();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 delta = rawInput * Time.deltaTime * mySpeed;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
