using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem trail;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        trail.Play();
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        trail.Stop();
    }

}
