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
    }

    private void Start() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
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

    public void FireAimedShot(float angle)
    {
        InitProj(angle);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            var angle = useAI ? 180f : 0f;
            InitProj(angle);

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

    private void InitProj(float angle)
    {
        GameObject projGameObj = Instantiate(projectilePrefab, 
                        gameObject.transform.position, 
                        Quaternion.Euler(0,0,angle));
        Rigidbody2D rb = projGameObj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = projGameObj.transform.up * projectileSpeed;
        }
        Destroy(projGameObj, projectileLifeTime);
    }
}
