using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] float mySpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D wallDetector;
    private int groundLayer;
    private int playerLayer;
    private int direction = 1;
    private bool stop = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        wallDetector = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");
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
        if (wallDetector.IsTouchingLayers(groundLayer))
        {
            transform.localScale = new Vector2 (transform.localScale.x * -1, 1f);
            direction *= -1;
        }
    }
}
