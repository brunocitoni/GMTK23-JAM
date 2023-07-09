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
    [SerializeField] TMP_Text score;

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
        itemsCrafted.text = "Items crafted: " + PlayerInventory.itemsCrafted; 
        potionsChugged.text = "Potions chugged: " + PlayerInventory.potionsDrank;
        score.text = "Total score: " + (EnemySpawner.enemyKilled + WaveManager.waveCounter * 2 + PlayerInventory.itemsCrafted - PlayerInventory.potionsDrank)*10;
    }
}
