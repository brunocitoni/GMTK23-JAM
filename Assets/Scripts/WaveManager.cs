using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Timer waveTimer;
    [SerializeField] int waveLenghtInSeconds;
    public bool isWaveOngoing = false;

    private void Start()
    {
        GameManager.OnNewGame += StartWave;
        waveTimer.TimerElapsed += EndWave;
        waveTimer.SetDuration(waveLenghtInSeconds);
    }

    private void OnDestroy()
    {
        waveTimer.TimerElapsed -= EndWave;
    }

    private void StartWave()
    {
        // start the wave timer
        waveTimer.SetDuration(waveLenghtInSeconds);
        waveTimer.RestartTimer();
        Debug.Log("Wave has started");
        isWaveOngoing = true;
    }

    private void EndWave()
    {
        Debug.Log("Wave has ended");
        isWaveOngoing = false;
    }
}
