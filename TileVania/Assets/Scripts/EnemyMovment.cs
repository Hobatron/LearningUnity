using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] float mySpeed = 1f;
    [SerializeField] float deathTimer = 1f;
    Rigidbody2D myRigidbody;
    CircleCollider2D myCircleCollider;
    BoxCollider2D myBoxCollider;
    ParticleSystem particals;
    Animator animator;
    SpriteRenderer mySprite;
    private int groundLayer;
    private int playerLayer;
    private int nonInteractiveLayer;
    private int direction = 1;
    private bool stop = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        particals = GetComponentInChildren<ParticleSystem>();
        mySprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");
        nonInteractiveLayer = LayerMask.NameToLayer("NonInteractive");
    }
    
    void Update()
    {
        if (!stop) 
        {
            if (myRigidbody.IsTouchingLayers(groundLayer))
            {
                myRigidbody.velocity = new Vector2 (mySpeed*direction, 0);
            }
            if (myRigidbody.IsTouchingLayers(playerLayer))
            {
                stop = true;
            }
        }
        else
        {
            //victory?
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (myBoxCollider.IsTouchingLayers(groundLayer))
        {
            transform.localScale = new Vector2 (transform.localScale.x * -1, 1f);
            direction *= -1;
        }
        if (other.tag == "Arrow")
        {
            stop = true;
            gameObject.layer = nonInteractiveLayer;
            animator.SetTrigger("diesTrigger");
            particals.Play();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        while (mySprite.color.a > Mathf.Epsilon)
        {
            yield return new WaitForSecondsRealtime(deathTimer / 100);
            mySprite.color = ReduceAlpha(mySprite.color, .01f);
        }
        Destroy(gameObject);
    }

    private Color ReduceAlpha(Color color, float dec)
    {
        return new Color(color.r, color.g, color.b, color.a - dec);
    }
}
