using UnityEngine;

public class DamageAnimations : MonoBehaviour
{
    [SerializeField] ParticleSystem onDamageTaken;
    [SerializeField] ParticleSystem onDeath;

    public void PlayOnDamageTaken()
    {
        InitParticalSystem(onDamageTaken);
    }

    public void PlayOnDeath()
    {
        InitParticalSystem(onDeath);
    }

    private void InitParticalSystem(ParticleSystem ps)
    {
        Instantiate(ps, transform.position, Quaternion.identity);
    }
}
