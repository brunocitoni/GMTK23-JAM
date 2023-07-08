using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWavePanel : MonoBehaviour
{

    [SerializeField] GameObject upgradePanel;

    private void Awake()
    {
        Debug.Log("EndWave Panel awake");
        WaveManager.OnWaveComplete += DisplayPanel;
        this.gameObject.SetActive(false);
    }

    private void DisplayPanel() {

        this.gameObject.SetActive(true);
        // take away player control

    }

    public void OnClickRest() {
        // todo add functionality
        Debug.Log("Resting...");

        //give back some health to player and hero
        GameObject.FindGameObjectWithTag("Hero").GetComponent<Health>().ModifyHealth(Data.healOnRestHero);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().ModifyHealth(Data.healOnRestPlayer);

        // invoke new wave start
        InvokeNewWaveStart();

    }

    public void OnClickUpgrade() {
        //todo
        Debug.Log("Upgrading...");

        // set this off
        this.gameObject.SetActive(false);
        // toggle upgrade panel
        upgradePanel.SetActive(true);

        // invoke new wave start
        //InvokeNewWaveStart();
    }

    private void InvokeNewWaveStart() {
        this.gameObject.SetActive(false);
        WaveManager.InvokeWaveStart();
    }
}
