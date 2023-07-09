using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButtonScript : MonoBehaviour
{
    public Sprite mutedAudioSprite;
    public Sprite activeAudipSprite;
    public Image buttonImage;

    private void Start()
    {
        this.GetComponent<Button>().onClick.RemoveAllListeners();
        this.GetComponent<Button>().onClick.AddListener(OnClickToggleMute);

        UpdateUI();
    }

    public void OnClickToggleMute()
    {
        AudioManager.ToggleMute();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (AudioManager.isMuted)
        {
            buttonImage.sprite = mutedAudioSprite;
        }
        else
        {
            buttonImage.sprite = activeAudipSprite;
        }
    }

}
