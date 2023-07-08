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

    // Start is called before the first frame update
    void Awake()
    {
        endWavePanel.SetActive(true);
        waveText.SetActive(true);
    }

    public void EnableWaveText()
    {

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
