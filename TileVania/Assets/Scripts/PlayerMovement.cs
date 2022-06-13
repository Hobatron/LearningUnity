using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float climbSpeed = 6f;
    public float runSpeed = 9f;
    public float jumpSpeed = 22f;
    bool playerIsReallyMoving;
    bool playerIsClimbing;
    Animator animator;
    CapsuleCollider2D playerCollider;
    Rigidbody2D rb2d;
    Vector2 moveInput;
    int groundLayer;
    int climbingLayer;
    float initGravity;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initGravity = rb2d.gravityScale;
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        climbingLayer = LayerMask.GetMask("Climbing");
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, 0);
        rb2d.AddForce(playerVelocity);
        animator.SetBool("isRunning", playerIsReallyMoving);
    }
    private void Climb()
    {
        if (playerCollider.IsTouchingLayers(climbingLayer)) {
            animator.SetBool("isClimbing", playerIsClimbing);
            rb2d.gravityScale = .5f;
            Vector2 playerVelocity = new Vector2 (rb2d.velocity.x, moveInput.y * climbSpeed);
            rb2d.velocity = playerVelocity;
        }
        else
        {
            animator.SetBool("isClimbing", playerIsClimbing && playerCollider.IsTouchingLayers(climbingLayer));
            rb2d.gravityScale = initGravity;
        }
    }

    void Update()
    {
        playerIsReallyMoving = MathF.Abs(moveInput.x) > Mathf.Epsilon;
        playerIsClimbing = MathF.Abs(moveInput.y) > Mathf.Epsilon;
        Climb();
        Run();
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (playerIsReallyMoving)
        {
            transform.localScale = new Vector2 (moveInput.x > Mathf.Epsilon ? 1 : -1, 1f);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && playerCollider.IsTouchingLayers(groundLayer))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }
    }
}
