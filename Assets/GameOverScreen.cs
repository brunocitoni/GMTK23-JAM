using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesKilled;
    [SerializeField] TMP_Text wavesCompleted;
    [SerializeField] TMP_Text itemsCrafted;
    [SerializeField] TMP_Text potionsChugged;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text trophyGiven;

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("Death screen on enable");
        Populate();
    }

    // Update is called once per frame
    void Populate()
    {
        enemiesKilled.text = "Enemies killed: " + EnemySpawner.enemyKilled;
        wavesCompleted.text = "Waves completed: " + WaveManager.waveCounter;
        itemsCrafted.text = "Items crafted: " + PlayerInventory.itemsCrafted;
        trophyGiven.text = "Trophy given: " + PlayerInventory.trophiesGiven;
        potionsChugged.text = "Potions chugged: " + PlayerInventory.potionsDrank;

        int score = (EnemySpawner.enemyKilled + (WaveManager.waveCounter * 2) + PlayerInventory.itemsCrafted - PlayerInventory.potionsDrank + (PlayerInventory.trophiesGiven*10)) * 10;
        Debug.Log("Score is " + score);
        scoreText.text = "Total score: " + score;

        if (!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetInt("highScore", 0);
            PlayerPrefs.Save();
        }

        int loadedScore = PlayerPrefs.GetInt("highScore");
        if (score > loadedScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            highScoreText.text = "New high score: " + score.ToString();
        }
        else
        {
            highScoreText.text = "High score is " + loadedScore.ToString();
        }

        // reset here in case player is restarting
        EnemySpawner.enemyKilled = 0;
        WaveManager.waveCounter = 0;
        PlayerInventory.itemsCrafted = 0;
        PlayerInventory.potionsDrank = 0;
        PlayerInventory.trophiesGiven = 0;
    }
}
