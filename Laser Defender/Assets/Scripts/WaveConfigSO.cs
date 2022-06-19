using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    EnemySpawner enemySpawner;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathePrefab;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetween = 1f;
    [SerializeReference] float spawnTimeVariance = .3f;
    [SerializeReference] float minimumSpawnTime = .5f;

    public Transform GetStartingWaypoint()
    {
        return pathePrefab.GetChild(0);
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathePrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetween - spawnTimeVariance, timeBetween + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
