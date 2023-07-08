using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesKilled;
    [SerializeField] TMP_Text wavesCompleted;
    [SerializeField] TMP_Text itemsCrafted;
    [SerializeField] TMP_Text potionsChugged;

    // Start is called before the first frame update
    void Start()
    {
        Populate();
    }

    // Update is called once per frame
    void Populate()
    {
        enemiesKilled.text = "Enemies killed: " + EnemySpawner.enemyKilled;
        wavesCompleted.text = "Waves completed: " + WaveManager.waveCounter;
        //itemsCrafted.text
        potionsChugged.text = "Potions chugged: " + PlayerInventory.potionsDrank;
    }
}
