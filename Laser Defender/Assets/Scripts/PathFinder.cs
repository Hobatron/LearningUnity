using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int currentWaypoint = 0;
    private EnemySpawner enemySpawner;

    void Awake() 
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }


    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
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
                currentWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
