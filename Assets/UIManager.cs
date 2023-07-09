using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject waveText;
    [SerializeField] GameObject endWavePanel;
    [SerializeField] GameObject atkBuffIcon;
    [SerializeField] GameObject defBuffIcon;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TMP_Text waveCounterText;

    // Start is called before the first frame update
    void Awake()
    {
        gameOverScreen.SetActive(false);

        // this need to initialise then will automatically reset to false
        endWavePanel.SetActive(true);
        waveText.SetActive(false);

        GameManager.OnGameOver += ToggleGameOverScreen;
        WaveManager.OnWaveStart += ActivateWaveTimer;
        WaveManager.OnWaveComplete += DeactivateWaveTimer;
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= ToggleGameOverScreen;
        WaveManager.OnWaveStart -= ActivateWaveTimer;
        WaveManager.OnWaveComplete -= DeactivateWaveTimer;
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

    public void ActivateWaveTimer()
    {
        waveText.SetActive(true);
    }

    public void DeactivateWaveTimer()
    {
        waveText.SetActive(false);
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

        waveCounterText.text = (WaveManager.waveCounter + 1).ToString();
    }
}
