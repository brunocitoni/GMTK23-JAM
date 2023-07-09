using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this should be instantiated or started when a new game is started
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int timeBetweenSpawns = 10;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Timer spawnerTimer;
    [SerializeField] List<GameObject> spawnLocations = new(); 
    public List<EnemySO> spawnableEnemies = new();
    public int currentNumberOfEnemiesSpawned;
    public static int enemyKilled = 0;

    private void Start()
    {
        WaveManager.OnWaveStart += WaveStarted;
        GameManager.OnGameOver += StopSpawning;
        //WaveManager.OnWaveStart += StartSpawning;
        spawnerTimer.TimerElapsed += StartSpawning;
    }

    private void WaveStarted()
    {
        spawnerTimer.SetDuration(timeBetweenSpawns);
        StartSpawning();
    }

    private void OnDestroy()
    {
        spawnerTimer.TimerElapsed -= StartSpawning;
        WaveManager.OnWaveStart -= StartSpawning;
        GameManager.OnGameOver -= StopSpawning;
    }

    public void StartSpawning()
    {
        Debug.Log("Spawning an enemy at " + this.transform.position);

        var newEnemy = Instantiate(enemyPrefab, this.transform);
        newEnemy.transform.position = spawnLocations[Random.Range(0,spawnLocations.Count)].transform.position;
        newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)]; // assign a random enemy to this specific enemy
        currentNumberOfEnemiesSpawned++;

        spawnerTimer.RestartTimer(); // at the moment only fixed time spawns, can make it more random if needs be todo
    }

    public void StopSpawning() {
        Debug.Log("Stopping the spawning");
        spawnerTimer.StopTimer();
    }
}
