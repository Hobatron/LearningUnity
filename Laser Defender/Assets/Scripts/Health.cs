using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] bool isPlayer;
    [SerializeField] EnemySO enemySO;
    private ScoreKeeper scoreKeeper;
    private DamageAnimations damageAnimations;
    private AudioPlayer audioPlayer;
    private CameraShake cameraShake;

    public int GetHealth()
    {
        return health;
    }

    private void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start() 
    {
        damageAnimations = GetComponent<DamageAnimations>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        health = isPlayer ? health : enemySO.health;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        
        if (damageDealer != null)
        {
            int damageTaken = damageDealer.GetDamage();
            TakeDamge(damageTaken);
            ShakeCamera(CalculateTrauma((float)damageTaken));
            damageDealer.Hit();
        }
    }

    private float CalculateTrauma(float damageTaken)
    {
        return Mathf.Clamp(damageTaken / health, .2f, .6f) * .8f;
    }

    private void ShakeCamera(float trauma)
    {
        if (cameraShake != null && isPlayer)
        {
            cameraShake.Play(trauma);
        }
    }

    private void TakeDamge(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            scoreKeeper.UpdateScore(isPlayer ? 0 : enemySO.points);
            audioPlayer.PlayOnDestroy(isPlayer);
            damageAnimations.PlayOnDeath();
            Destroy(gameObject);
        }
        else
        {
            audioPlayer.PlayOnDamageTaken();
            damageAnimations.PlayOnDamageTaken();
        }
    }
}
