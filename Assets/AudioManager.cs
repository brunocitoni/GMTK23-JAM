using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip battleMusic;
    public AudioClip restMusic;
    public AudioClip mainMenuMusic;
    public static bool isMuted;
    public Button muteButton;

    public GameObject defaultAudioPlayer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        float time = 0;
        if (audioSource.clip != mainMenuMusic)
        {
            time = audioSource.time;
        }

        audioSource.Stop();
        audioSource.clip = battleMusic;
        audioSource.time = time;
        audioSource.Play();
    }

    private void StopPlayingBattleMusic()
    {
        float time = audioSource.time;
        audioSource.Stop();
        audioSource.clip = restMusic;
        audioSource.time = time;
        audioSource.Play();
    }

    public static void PlaySound(AudioClip sounds, Vector2 pitchRange = default(Vector2))
    {
        GameObject audioGameObject = Instantiate(instance.defaultAudioPlayer, instance.transform);
        AudioSource source = audioGameObject.GetComponent<AudioSource>();
        source.clip = sounds;
        source.pitch *= 1 + Random.Range(pitchRange.x, pitchRange.y);
        source.Play();
    }

    public static void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
    }
}






