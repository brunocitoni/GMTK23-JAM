using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class WaveManager : SerializedMonoBehaviour
{
    public static Timer waveTimer;
    public static Timer waveCountdown;
    EnemySpawner enemySpawner;
    public static bool isWaveOngoing = false;
    public static bool waitingForNewWave = true;
    public static int waveCounter = 0;

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject UICanvas;

    // events
    public delegate void WaveComplete();
    public static event WaveComplete OnWaveComplete;

    public delegate void WaveStarting();
    public static event WaveStarting OnWaveStart;

    private void Start()
    {

        // get a reference to the enemySpawner to stop and start the spawns
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        //get a reference to conwdownTimer in UI Canvas
        waveCountdown = UICanvas.GetComponent<Timer>();

        waveTimer = this.GetComponent<Timer>();
        waveTimer.TimerElapsed += EndWave;
        waveTimer.SetDuration(Data.waveLenghtInSeconds);
        waveTimer.display = true;
        InvokeWaveStart();
    }

    private void OnDestroy()
    {
        waveTimer.TimerElapsed -= EndWave;
        //GameManager.OnNewGame -= OnNewGame;
    }

    public static void StartWave()
    {
        // start the wave timer
        waveTimer.SetDuration(Data.waveLenghtInSeconds);
        waveTimer.RestartTimer();
        Debug.Log("Wave has started");
        isWaveOngoing = true;
        waitingForNewWave = false;
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
    }

    // this needs to be called by the enemy that dies AFTER the spawner has ceased spawning
    public static void InvokeWaveComplete() {
        Debug.Log("Wave complete! Invoking the OnWaveComplete event");
        waitingForNewWave = true;
        waveCounter++;

        // destroy all objects left lying around
        UtilityFunctions.DestroyAllChildren(GameObject.Find("ItemSpawner"));

        OnWaveComplete?.Invoke();
    }

    public static void InvokeWaveStart() {
        Debug.Log("A new wave start has been invoked");
        waveCountdown.SetDuration(4);
        waveCountdown.RestartTimer();
        waveCountdown.TimerElapsed += () =>
        {
            waveCountdown.timerText.text = "";
            OnWaveStart?.Invoke();
            StartWave();
        };

        //OnWaveStart?.Invoke();
        //StartWave();
    }
}
