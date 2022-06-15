using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float bulletSpeed = 15f;
    PlayerMovement player;
    private float xSpeed;

    void Start()
    {
      myRigidbody = GetComponent<Rigidbody2D>();  
      player = FindObjectOfType<PlayerMovement>();
      xSpeed = player.transform.localScale.x * bulletSpeed;
      transform.localScale = new Vector3(player.transform.localScale.x,1f,0);
    }
    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Destroy(gameObject);
    }
}
