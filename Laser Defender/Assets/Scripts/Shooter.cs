using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float fireRate = .2f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float projectileSpeed = 10f;
    [Header("AI")]
    [SerializeField] float minAIFireRate = .5f;
    private float aiFireRateVariance = .2f;
    [SerializeField] bool useAI;
    [HideInInspector]
    public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start() 
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            var rotateProj = useAI ? 180f : 0f;
            GameObject projGameObj = Instantiate(projectilePrefab, 
                        gameObject.transform.position, 
                        Quaternion.Euler(0,0,rotateProj));
            Rigidbody2D rb = projGameObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(projGameObj, projectileLifeTime);

            fireRate = useAI ?
                        Mathf.Clamp(
                            UnityEngine.Random.Range(fireRate - aiFireRateVariance, fireRate + aiFireRateVariance),
                            minAIFireRate,
                            float.MaxValue)
                        : fireRate;

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(fireRate);
        };
    }
}
