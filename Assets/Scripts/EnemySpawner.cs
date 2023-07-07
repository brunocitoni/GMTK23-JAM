using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int timeBetweenSpawns = 10;
    [SerializeField] Timer spawnerTimer;

    private void Start()
    {
        spawnerTimer.TimerElapsed += SpawnEnemy;
        
    }

    private void OnDestroy()
    {
        spawnerTimer.TimerElapsed -= SpawnEnemy;
    }

    private void SpawnEnemy() {
        // todo function to instantiate a game object and assign it a random(?) EnemySO
    }
}
