using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int timeBetweenSpawns = 10;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Timer spawnerTimer;
    [SerializeField] List<GameObject> spawnLocations = new List<GameObject>();
    private GameManager gameManager;
    public List<EnemySO> spawnableEnemies = new List<EnemySO>();
    public int currentNumberOfEnemiesSpawned;
    public static int enemyKilled = 0;

    private Vector3 initialPosition;

    private void Start()
    {
        WaveManager.OnWaveStart += WaveStarted;
        GameManager.OnGameOver += StopSpawning;
        spawnerTimer.TimerElapsed += StartSpawning;

        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void WaveStarted()
    {
        spawnerTimer.SetDuration(timeBetweenSpawns);
        StartSpawning();
    }

    private void OnDestroy()
    {
        WaveManager.OnWaveStart -= WaveStarted;
        GameManager.OnGameOver -= StopSpawning;
        spawnerTimer.TimerElapsed -= StartSpawning;
    }

    public void StartSpawning()
    {
        // Use the stored initial position instead of this.transform
        Debug.Log("Spawning an enemy at " + initialPosition);

        var newEnemy = Instantiate(enemyPrefab, this.gameObject.transform);
        newEnemy.transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position;
        newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)];
        currentNumberOfEnemiesSpawned++;

        spawnerTimer.RestartTimer();
    }

    public void StopSpawning()
    {
        Debug.Log("Stopping the spawning");
        spawnerTimer.StopTimer();

        /*if (GameObject.FindGameObjectWithTag("Hero").GetComponent<Health>().currentHealth <= 0)
        {
            gameManager.TriggerGameOver();
        }*/
    }
}
