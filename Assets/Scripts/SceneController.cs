using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] string gameSceneName;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnNewGame += NewGame;
    }

    // Update is called once per frame
    void NewGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
