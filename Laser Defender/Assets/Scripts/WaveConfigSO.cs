using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    EnemySpawner enemySpawner;
    [SerializeField] List<GameObject> pathingEnemyPrefabs;
    [SerializeField] List<GameObject> chargerPrefabs;
    [SerializeField] List<GameObject> smartBugPrefabs;
    [SerializeField] Transform pathePrefab;
    [SerializeField] Transform smartBugPathPrefab;
    [SerializeField] Transform chargerSpawnsPrefab;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetween = 1f;
    [SerializeReference] float spawnTimeVariance = .3f;
    [SerializeReference] float minimumSpawnTime = .5f;

    internal List<string> GetListOfSpawnables()
    {
        List<string> list = new List<string>();
        pathingEnemyPrefabs.ForEach(l => list.Add("pather"));
        chargerPrefabs.ForEach(l => list.Add("charger"));
        smartBugPrefabs.ForEach(l => list.Add("smartBug"));
        return list;
    }

    public Transform GetStartingWaypoint()
    {
        return pathePrefab.GetChild(0);
    }
    public Transform GetSmartBugStartingWaypoint()
    {
        return smartBugPathPrefab.GetChild(0);
    }

    public bool HasChargers()
    {
        return chargerSpawnsPrefab != null;
    }

    public Transform GetChargerSpawnPoint()
    {
        Transform[] spawnPoints = chargerSpawnsPrefab.GetComponentsInChildren<Transform>();
        return spawnPoints[Random.Range(1, spawnPoints.Length)];
    }

    public int GetEnemyCount()
    {
        return pathingEnemyPrefabs.Count + chargerPrefabs.Count + smartBugPrefabs.Count;
    }

    public GameObject GetChargerPrefab(int index)
    {
        return chargerPrefabs[index];
    }

    public GameObject GetSmartBugPrefab(int index)
    {
        Debug.Log(smartBugPrefabs);
        return smartBugPrefabs[index];
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return pathingEnemyPrefabs[index];
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public List<Transform> GetSmartBugWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in smartBugPathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
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
