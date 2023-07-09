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

    static TMP_Text countdownLabel;

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
        waveCountdown = GameObject.Find("UI Canvas Variant").GetComponent<Timer>();

        //countdownLabel = GameObject.Find("UI Canvas Variant").transform.Find("countdownToWaveLabel").GetComponent<TMP_Text>();

        waveTimer = this.GetComponent<Timer>();
        waveTimer.TimerElapsed += EndWave;
        waveTimer.SetDuration(Data.waveLenghtInSeconds);
        waveTimer.display = true;
        InvokeWaveStart();
    }

    private void OnDestroy()
    {
        waveTimer.TimerElapsed -= EndWave;
    }

    public static void StartWave()
    {
        // set coundown to wave UI not active
        //countdownLabel.enabled = false;

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

        // add to wave timer, and make enemies spawn quicker
        Data.waveLenghtInSeconds = Data.waveLenghtInSeconds + waveCounter;
        Data.spawnDelay = Data.spawnDelay - waveCounter / 5;

        // destroy all objects left lying around
        UtilityFunctions.DestroyAllChildren(GameObject.Find("ItemSpawner"));

        OnWaveComplete?.Invoke();
    }

    public static void InvokeWaveStart() {
        Debug.Log("A new wave start has been invoked");
        waveCountdown.SetDuration(4);
        waveCountdown.RestartTimer();
        // set coundown to wave UI active
        //countdownLabel.enabled = true;
        waveCountdown.TimerElapsed += ActuallyInvoke;
    }

    private static void ActuallyInvoke()
    {
        waveCountdown.TimerElapsed -= ActuallyInvoke; // immediately unsub because we are goign to subscribe again
        waveCountdown.timerText.text = "";
        OnWaveStart?.Invoke();
        StartWave();
    }
}
