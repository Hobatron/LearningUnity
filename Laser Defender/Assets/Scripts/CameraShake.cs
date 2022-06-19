using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] int maxShakeAngle;


    Vector3 initPosition;
    private Quaternion initRotation;
    private float trauma;

    void Start()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
    }

    public void Play(float trauma)
    {
        this.trauma += trauma;
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        while (trauma > 0)
        {
            float shake = Mathf.Pow(trauma, 2);
            transform.position = initPosition + 
                                (Vector3)Random.insideUnitCircle * 
                                shake;
            transform.rotation = Quaternion.Euler(0, 0, shake * maxShakeAngle * Random.Range(-1, 1));
            trauma -= Time.deltaTime * .1f;
            yield return new WaitForEndOfFrame();
        }
        trauma = 0;
        transform.rotation = initRotation;
        transform.position = initPosition;
    }
}
