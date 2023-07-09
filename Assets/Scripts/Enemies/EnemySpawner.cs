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
    public List<EnemySO> spawnableEnemiesLv0 = new List<EnemySO>();
    public List<EnemySO> spawnableEnemiesLv1 = new List<EnemySO>();
    public List<EnemySO> spawnableEnemiesLv2 = new List<EnemySO>();
    public List<EnemySO> spawnableEnemiesLv3 = new List<EnemySO>();
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

        if (WaveManager.waveCounter < 2)
        {
            newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemiesLv0[Random.Range(0, spawnableEnemiesLv0.Count)];
        } else if (WaveManager.waveCounter > 2 && WaveManager.waveCounter < 6)
        {
            newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemiesLv1[Random.Range(0, spawnableEnemiesLv1.Count)];
        }
        else if (WaveManager.waveCounter > 6 && WaveManager.waveCounter < 10)
        {
            newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemiesLv2[Random.Range(0, spawnableEnemiesLv2.Count)];
        }
        else if (WaveManager.waveCounter > 10)
        {
            newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemiesLv2[Random.Range(0, spawnableEnemiesLv3.Count)];
        }
        else // should never happen
        {
            newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemiesLv1[Random.Range(0, spawnableEnemiesLv1.Count)];
        }

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
