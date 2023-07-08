using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Timer waveTimer;
    [SerializeField] int waveLenghtInSeconds;
    [SerializeField] EnemySpawner enemySpawner;
    public bool isWaveOngoing = false;

    private void Start()
    {
        //GameManager.OnNewGame += StartWave;
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
        Debug.Log("Wave has ended");

        // stop enemy spawning
        enemySpawner.StopSpawning();
        isWaveOngoing = false;

        // now when last enemy is killed fire off a Waiting next wave event

    }
}
