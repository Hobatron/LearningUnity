using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] bool isSmartBug;
    [SerializeField] float delayForSmartBug;
    [SerializeField]  float rotationSpeedSmartBug;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int currentWaypoint = 0;
    private EnemySpawner enemySpawner;
    private bool notInCo = true;

    void Awake() 
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }


    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = isSmartBug ? waveConfig.GetSmartBugWaypoints() : waveConfig.GetWaypoints();
        transform.position = waypoints[currentWaypoint].position;
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (currentWaypoint < waypoints.Count)
        {
            Vector2 targetPostition = waypoints[currentWaypoint].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPostition, delta);
            if ((Vector2)transform.position == targetPostition)
            {
                if (isSmartBug)
                {
                    if (notInCo)
                    {
                        notInCo = false;
                        StartCoroutine(WaitToShootThenMove());
                    }
                }
                else
                {
                    currentWaypoint++;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitToShootThenMove()
    {
        //shoot
        StartCoroutine(TurnTowardsNextPosition());
        yield return new WaitForSeconds(delayForSmartBug);
    }

    IEnumerator TurnTowardsNextPosition()
    {
        bool done = currentWaypoint == waypoints.Count - 1;
        while (!done)
        {
            Vector3 direction = transform.position - waypoints[currentWaypoint + 1].position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle*-1));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeedSmartBug * Time.deltaTime);
            done = targetRotation == transform.rotation;
            yield return new WaitForEndOfFrame();
        };
        currentWaypoint++;
        notInCo = true;
    }
}
