using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource ambientAudioSource;

    [Header("Audio Levels")]
    [Range(1, 100)]
    public int _masterVolume = 100;

    [Range(1, 100)]
    public int _sfxVolume = 100;

    [Range(1, 100)]
    public int _musicVolume = 100;

    [Range(1, 100)]
    public int _ambientVolume = 100;

    private AudioClip queuedMusicClip;
    private float queuedMusicClipVolume = 0;
    [SerializeField] private float musicFadeSpeed = 0.1f;
    [SerializeField] private ConfigScriptableObject config;

    private static AudioPlayer _instance;
    public static AudioPlayer Instance
    {
        get {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject parentObject = GameObject.Find("Audio");
        _masterVolume = Prefs.GetMasterVolume();
        DontDestroyOnLoad(parentObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (queuedMusicClip != null || queuedMusicClipVolume != 0)
        {
            musicAudioSource.volume = Mathf.Max(musicAudioSource.volume - musicFadeSpeed, 0);
        }

        if (musicAudioSource.volume == 0)
        {
            musicAudioSource.clip = queuedMusicClip;
            float playVolume = (_masterVolume / 100f) * (_sfxVolume / 100f) * (queuedMusicClipVolume / 100f);
            musicAudioSource.clip = queuedMusicClip;
            musicAudioSource.volume = playVolume;
            musicAudioSource.Play();
            queuedMusicClip = null;
            queuedMusicClipVolume = 0;
        }
    }

    public void PlaySFX(AudioClip sfxClip, int sfxVolume = 100)
    {
        float playVolume = (_masterVolume / 100f) * (_sfxVolume / 100f) * (sfxVolume / 100f);
        sfxAudioSource.PlayOneShot(sfxClip, playVolume);
    }

    public void PlayMusic(AudioClip musicClip, int musicVolume = 100)
    {
        _masterVolume = Prefs.GetMasterVolume();

        if (musicAudioSource.clip == null)
        {
            float playVolume = calculateNewVolume();
            musicAudioSource.volume = playVolume;
            musicAudioSource.clip = musicClip;
            musicAudioSource.Play();
        }
        else if (musicAudioSource.clip != musicClip)
        {
            queuedMusicClip = musicClip;
            queuedMusicClipVolume = musicVolume;
        }

        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    public void FadeMusicVolume(int musicVolume)
    {
        queuedMusicClipVolume = musicVolume;
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void SetMasterVolume(int newVolume)
    {
        if (newVolume > 0 && newVolume <= 100)
        {
            Prefs.SetMasterVolume(newVolume);
            _masterVolume = newVolume;

            if (musicAudioSource.isPlaying)
            {
                float adjustedVolume = calculateNewVolume();
                musicAudioSource.volume = adjustedVolume;
            }
        }
    }

    private float calculateNewVolume()
    {
        if (config.isTrailer)
        {
            _musicVolume = 0;
        }
        return (_masterVolume / 100f) * (_musicVolume / 100f) * (GameManager.Instance.currentMusicVolume / 100f);
    }
}
