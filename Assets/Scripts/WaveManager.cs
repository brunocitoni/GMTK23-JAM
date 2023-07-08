using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Timer waveTimer;
    [SerializeField] int waveLenghtInSeconds;


    private void Start()
    {
        waveTimer.TimerElapsed += EndWave;
        waveTimer.SetDuration(waveLenghtInSeconds);
        // need to subscribe to a New Game event to start the timer

    }

    private void OnDestroy()
    {
        spawnerTimer.TimerElapsed -= SpawnEnemy;
    }

    private void StartWave()
    {
        Debug.Log("Spawning an enemy at " + this.transform.position);

        var newEnemy = Instantiate(enemyPrefab, this.transform);
        newEnemy.transform.position = this.transform.position;
        newEnemy.GetComponent<Enemy>().thisEnemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)]; // assign a random enemy to this specific enemy

        spawnerTimer.RestartTimer(); // at the moment only fixed time spawns, can make it more random if needs be todo
    }

    private void EndWave() {

    }
}
