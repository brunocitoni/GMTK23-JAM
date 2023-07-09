using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip battleMusic;
    public AudioClip restMusic;
    public AudioClip mainMenuMusic;

    public static AudioManager instance;

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

    private void Start()
    {
        WaveManager.OnWaveStartCountdownBegin += StartPlayingBattleMusic;
        WaveManager.OnWaveComplete += StopPlayingBattleMusic;

        audioSource.clip = mainMenuMusic;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        WaveManager.OnWaveStartCountdownBegin -= StartPlayingBattleMusic;
        WaveManager.OnWaveComplete -= StopPlayingBattleMusic;
    }

    private void StartPlayingBattleMusic()
    {
        audioSource.Stop();
        audioSource.clip = battleMusic;
        audioSource.Play();
    }

    private void StopPlayingBattleMusic()
    {
        audioSource.Stop();
        audioSource.clip = restMusic;
        audioSource.Play();
    }
}






