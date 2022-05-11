using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    WaveConfigSO currentWave;
    void Start()
    {
        StartCoroutine(SpawnWavesAndEnemies());
    }

    public WaveConfigSO GetCurrentWave() {
        return currentWave;
    }

    // IEnumerator StartWave() {
    //     foreach(WaveConfigSO wave in waveConfigs) {
    //         currentWave = wave;
    //         StartCoroutine(SpawnEnemies());

    //         yield return new WaitForSecondsRealtime(timeBetweenWaves);
    //     }
    // }

    IEnumerator SpawnWavesAndEnemies() {
        do {
            foreach(WaveConfigSO wave in waveConfigs) {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++) {
                    if (currentWave.GetEnemyPrefab(i) != null) {
                        Instantiate(
                            currentWave.GetEnemyPrefab(i),
                            currentWave.GetStartingWaypoint().position,
                            Quaternion.Euler(0,0,180),
                            transform);
                    }

                    yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
        } while (isLooping);
        
    }
}
