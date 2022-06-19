using System;
using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = .2f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] AudioClip playerDestroyed;
    [SerializeField] AudioClip enemyDestroyed;
    [SerializeField] [Range(0f, 1f)] float damageVolume = .2f;

    [Header("Pitch")]
    [SerializeField] [Range(0f, 1f)] float minPitch;
    [SerializeField] [Range(0f, 10f)] private float pitchStepper;
    [SerializeField] [Range(0f, 1f)] private float pitchSpeed;
    private AudioSource audioSource;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();  
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayOnDamageTaken()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayOnDestroy(bool isPlayer)
    {
        if (isPlayer)
        {
            PlayClip(playerDestroyed, damageVolume);
            StartCoroutine(ReducePitch());
        } 
        else
        {
            PlayClip(enemyDestroyed, damageVolume);
        }
    }

    IEnumerator ReducePitch()
    {
        
        while (audioSource.pitch > minPitch)
        {
            float stepDelta = pitchStepper * Time.deltaTime;
            audioSource.pitch -= stepDelta; 
            yield return new WaitForSeconds(pitchSpeed);
        };
    }

    void PlayClip(AudioClip clip, float vol)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, vol);
        }
    }
}
