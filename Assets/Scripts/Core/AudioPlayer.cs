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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip sfxClip, int sfxVolume = 100)
    {
        float playVolume = (_masterVolume / 100f) * (_sfxVolume / 100f) * (sfxVolume / 100f);
        sfxAudioSource.PlayOneShot(sfxClip, playVolume);
    }
}
