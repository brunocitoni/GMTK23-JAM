using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public delegate void NewGameDelegate();
    public static NewGameDelegate OnNewGame; // Event to be invoked on death


    public delegate void GameOverDelegate();
    public static GameOverDelegate OnGameOver; // Event to be invoked on death

    public static int score;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnPlayerDeath += TriggerGameOver;
    }

    // Start is called before the first frame update
    void OnDestroy()
    {
        PlayerHealth.OnPlayerDeath -= TriggerGameOver;
        PlayerPrefs.SetInt("restart", 0);
        PlayerPrefs.Save();
    }

    public void TriggerGameOver() {
        GameObject.FindGameObjectWithTag("Hero").SetActive(false);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy")) {
            obj.SetActive(false);
        }
        OnGameOver?.Invoke();
    }

    public void OnClickNewGame() {
        OnNewGame?.Invoke();
    }

    public void OnClickRestart() {
        PlayerPrefs.SetInt("restart", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
