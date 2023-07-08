using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject waveText;
    [SerializeField] GameObject endWavePanel;

    // Start is called before the first frame update
    void Awake()
    {
        endWavePanel.SetActive(true);
        waveText.SetActive(true);
    }

    public void EnableWaveText()
    {

    }
}
