using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    private enum TransitionType
    {
        Slide,
        Fade
    }

    [SerializeField] private string sceneName;
    [SerializeField] private TransitionType transition = TransitionType.Slide;

    [Header("Sound")]
    [SerializeField] private AudioClip levelAudioClip;
    [Range(1, 100)] [SerializeField] private int levelAudioVolume = 100;

    [Header("Debug")]
    [SerializeField] private ConfigScriptableObject config;
    [SerializeField] private string debugEntryLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string nextScene = (bool)config && config.isDemo && debugEntryLevel != "" ? debugEntryLevel : sceneName;

        if (collision.gameObject.tag == "Player")
        {
            if (transition == TransitionType.Slide)
            {
                AudioPlayer.Instance.PlaySFX(levelAudioClip, levelAudioVolume);
                GameManager.Instance.SlideLoadLevel(nextScene);
            }
            else if (transition == TransitionType.Fade)
            {
                GameManager.Instance.FadeLoadLevel(nextScene);
            }
        }
    }
}
