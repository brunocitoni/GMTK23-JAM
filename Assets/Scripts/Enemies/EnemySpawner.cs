using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this should be instantiated or started when a new game is started
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int timeBetweenSpawns = 10;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Timer spawnerTimer;
    public List<EnemySO> spawnableEnemies = new();

    private void Start()
    {
        spawnerTimer.TimerElapsed += SpawnEnemy;
        spawnerTimer.SetDuration(timeBetweenSpawns);
        spawnerTimer.RestartTimer();
    }

    private void OnDestroy()
    {
        spawnerTimer.TimerElapsed -= SpawnEnemy;
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawning an enemy at " + this.transform.position);

        var newEnemy = Instantiate(enemyPrefab, this.transform);
        newEnemy.transform.position = this.transform.position;
        newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)]; // assign a random enemy to this specific enemy

        spawnerTimer.RestartTimer(); // at the moment only fixed time spawns, can make it more random if needs be todo
    }
}
