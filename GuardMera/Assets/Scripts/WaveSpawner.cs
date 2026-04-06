using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    
    public float timeBetweenWaves = 5f;
    public float countDown = 1f;
    public float spawnTiming = 1f;
    
    private int waveIndex = 0;

    void Update()
    {
        if(enemiesAlive > 0)
        {
            return;
        }

        if(countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
    }

     IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        if(!wave.diffEnemies)
        {
            enemiesAlive = wave.eCount;
            for(int i = 0; i < wave.eCount; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(spawnTiming/wave.rate);
            }
        }
        else
        {
            enemiesAlive = wave.eCount + wave.e2Count + wave.e3Count;
            for(int i = 0; i < wave.eCount; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(spawnTiming/wave.rate);
            }
            if(wave.enemy2 != null)
            {
                for(int i = 0; i < wave.e2Count; i++)
                {
                    SpawnEnemy(wave.enemy2);
                    yield return new WaitForSeconds(spawnTiming/wave.rate2);
                }
                if(wave.enemy3 != null)
                {

                    for(int i = 0; i < wave.e3Count; i++)
                    {
                        SpawnEnemy(wave.enemy3);
                        yield return new WaitForSeconds(spawnTiming/wave.rate3);
                    }
                }
            }
        }
        while(enemiesAlive != 0)
        {
            yield return null;
        }
        waveIndex++;
        if((waveIndex == waves.Length))
        {
            while(enemiesAlive != 0)
            {
                yield return null;
            }
            this.enabled = false;
        }

    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

}
