using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWavePanel : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("EndWave Panel awake");
        WaveManager.OnWaveComplete += DisplayPanel;
        this.gameObject.SetActive(false);
    }

    private void DisplayPanel() {

        this.gameObject.SetActive(true);
    }

    public void OnClickRest() {
        // todo add functionality
        Debug.Log("Resting...");
        // invoke new wave start
        InvokeNewWaveStart();

    }

    public void OnClickCraft() {

        //todo
        Debug.Log("Crafting...");
        // invoke new wave start
        InvokeNewWaveStart();
    }

    public void OnClickUpgrade() {
        //todo
        Debug.Log("Upgrading...");
        // invoke new wave start
        InvokeNewWaveStart();
    }

    private void InvokeNewWaveStart() {
        this.gameObject.SetActive(false);
        WaveManager.InvokeWaveStart();
    }
}
