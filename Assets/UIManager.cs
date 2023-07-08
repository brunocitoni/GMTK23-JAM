using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject waveText;
    [SerializeField] GameObject endWavePanel;
    [SerializeField] GameObject atkBuffIcon;
    [SerializeField] GameObject defBuffIcon;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject mainMenuScreen;

    // Start is called before the first frame update
    void Awake()
    {
        gameOverScreen.SetActive(false);
        // these need to initialise then will automatically reset to false
        endWavePanel.SetActive(true);
        waveText.SetActive(true);
        GameManager.OnGameOver += ToggleGameOverScreen;
        GameManager.OnNewGame += ToggleMainMenuScreen;
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= ToggleGameOverScreen;
        GameManager.OnNewGame -= ToggleMainMenuScreen;
    }

    public void ToggleGameOverScreen()
    {
        if (gameOverScreen.activeSelf)
        {
            gameOverScreen.SetActive(false);
        }
        else
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void ToggleMainMenuScreen()
    {
        if (mainMenuScreen.activeSelf)
        {
            mainMenuScreen.SetActive(false);
        }
        else {
            mainMenuScreen.SetActive(true);
        }
    }

    private void Update()
    {
        if (HeroManager.attackBuffActive) {
            atkBuffIcon.SetActive(true);
        }
        else {
            atkBuffIcon.SetActive(false);
        }

        if (HeroManager.defenceBuffActive)
        {
            defBuffIcon.SetActive(true);
        }
        else
        {
            defBuffIcon.SetActive(false);
        }
    }
}
