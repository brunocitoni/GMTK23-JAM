using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Timer waveTimer;
    [SerializeField] int waveLenghtInSeconds;
    EnemySpawner enemySpawner;
    public static bool isWaveOngoing = false;

    // events
    public delegate void WaveComplete();
    public static event WaveComplete OnWaveComplete;

    private void Start()
    {
        // get a reference to the enemySpawner to stop and start the spawns
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        waveTimer.TimerElapsed += EndWave;
        waveTimer.SetDuration(waveLenghtInSeconds);
        waveTimer.display = true;
        StartWave();
    }

    private void OnDestroy()
    {
        waveTimer.TimerElapsed -= EndWave;
    }

    private void StartWave()
    {
        // start the wave ti
        waveTimer.SetDuration(waveLenghtInSeconds);
        waveTimer.RestartTimer();
        Debug.Log("Wave has started");
        isWaveOngoing = true;
    }

    private void EndWave()
    {
        Debug.Log("Wave time has ended");

        // stop enemy spawning
        enemySpawner.StopSpawning();
        isWaveOngoing = false;

        // check if there are currently no enemies alive
        if(enemySpawner.currentNumberOfEnemiesSpawned <= 0) {
            InvokeWaveComplete();
        }

        // else we need to wait for the call to en the wave coming from the last enemy getting killed by the hero
    }

    // this needs to be called by the enemy that dies AFTER the spawner has ceased spawning
    public static void InvokeWaveComplete() {
        OnWaveComplete?.Invoke();
    }
}
