using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameSession gameSession;
    [SerializeField] float climbSpeed = 6f;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;
    Animator animator;
    CapsuleCollider2D playerCollider;
    Rigidbody2D rb2d;
    Vector2 moveInput;
    ParticleSystem blood;
    SceneManager sceneManager;
    public float runSpeed = 8.5f;
    public float jumpSpeed = 22f;
    bool playerIsReallyMoving;
    bool playerIsClimbing;
    int groundLayer;
    int climbingLayer;
    private int hazardsLayer;
    float initGravity;
    private bool disableControls = false;
    private bool isShooting = false;
    private bool allowClickToRetry;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initGravity = rb2d.gravityScale;
        animator = GetComponent<Animator>();
        blood = GetComponentInChildren<ParticleSystem>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        climbingLayer = LayerMask.GetMask("Climbing");
        hazardsLayer = LayerMask.GetMask("Hazards");
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
        
        if (!disableControls)
        {
            playerIsReallyMoving = MathF.Abs(moveInput.x) > Mathf.Epsilon;
            playerIsClimbing = MathF.Abs(moveInput.y) > Mathf.Epsilon;
            Climb();
            Run();
            FlipSprite();
        }
    }

    void ToggleShooting()
    {
        isShooting = !isShooting;
    }

    void ShootArrow()
    {
        Instantiate(arrow, bow.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if ((other.gameObject.tag == "Goober" || rb2d.IsTouchingLayers(hazardsLayer)) && !disableControls)
        {
            Death();
        }
    }

    private void Death()
    {
        disableControls = true;
        animator.SetTrigger("deathTrigger");
        rb2d.velocity = new Vector2(0, jumpSpeed);
        blood.Play();
        gameSession.ProcessPlayerDeath();
    }

    private void FlipSprite()
    {
        if (playerIsReallyMoving)
        {
            transform.localScale = new Vector2 (moveInput.x > Mathf.Epsilon ? 1 : -1, 1f);
        }
    }

    void OnFire()
    {
        animator.SetTrigger("shootTrigger");
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && playerCollider.IsTouchingLayers(groundLayer) && !disableControls)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }
    }
}
