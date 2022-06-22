using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool spawning = true;
    WaveConfigSO currentWave;
    private List<string> availableSpawns;

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
           foreach (WaveConfigSO wave in waveConfigs)
            {
                availableSpawns = wave.GetListOfSpawnables();
                currentWave = wave;
                int patherSpawned = 0;
                int chargerSpawned = 0;
                int smartBugSpawned = 0;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    switch (ShouldSpawnX())
                    {
                        case "pather":
                            SpawnPather(patherSpawned);
                            availableSpawns.Remove("pather");
                            patherSpawned++;
                            break;
                        case "charger":
                            SpawnCharger(chargerSpawned);
                            availableSpawns.Remove("charger");
                            chargerSpawned++;
                            break;
                        case "smartBug":
                            SpawnSmartBug(smartBugSpawned);
                            availableSpawns.Remove("smartBug");
                            smartBugSpawned++;
                            break;
                        default:
                            break;
                    };
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (spawning);
    }

    private string ShouldSpawnX()
    {
        return availableSpawns[Random.Range(0, availableSpawns.Count)];
    }

    private void SpawnPather(int i)
    {
        Instantiate(currentWave.GetEnemyPrefab(i), 
                    currentWave.GetStartingWaypoint().position, 
                    Quaternion.Euler(0f, 0f, 180f)); 
    }
    private void SpawnCharger(int i)
    {
        Instantiate(currentWave.GetChargerPrefab(i), 
                    currentWave.GetChargerSpawnPoint().position, 
                    Quaternion.identity); 
    }
    
    private void SpawnSmartBug(int i)
    {
        waveConfigs = null;
        spawning = false;
        Debug.Log("Trying to spawn a smart boy");
        Instantiate(currentWave.GetSmartBugPrefab(i), 
                    currentWave.GetSmartBugStartingWaypoint().position, 
                    Quaternion.identity); 
    }

}
