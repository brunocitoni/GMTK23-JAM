using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Timer waveTimer;
    [SerializeField] int waveLenghtInSeconds;


    private void Start()
    {
        GameManager.OnNewGame += StartWave;
        waveTimer.TimerElapsed += EndWave;
        waveTimer.SetDuration(waveLenghtInSeconds);
        // need to subscribe to a New Game event to start the timer

    }

    private void OnDestroy()
    {
        waveTimer.TimerElapsed -= EndWave;
    }

    private void StartWave()
    {
       
    }

    private void EndWave() {

    }
}
