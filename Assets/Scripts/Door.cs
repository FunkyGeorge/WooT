using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string inventoryDictKeyToCheck;
    [SerializeField] private bool doesConsumeItem = true;
    [SerializeField] private bool hasStateRequirement = false;
    [SerializeField] private LevelState.State stateRequirement;

    [Header("Audio")]
    [SerializeField] private AudioClip onDestroyAudioClip;
    [SerializeField] private AudioClip accessDeniedAudioClip;
    [SerializeField] [Range(1, 100)] private int onDestroyAudioVolume = 100;
    [SerializeField] [Range(1, 100)] private int accessDeniedAudioVolume = 100;

    private LevelState levelState;

    // Start is called before the first frame update
    void Start()
    {
        levelState = FindObjectOfType<LevelState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((hasStateRequirement && levelState.state == stateRequirement && Player.Instance.inventory.ContainsKey(inventoryDictKeyToCheck)) || (!hasStateRequirement && Player.Instance.inventory.ContainsKey(inventoryDictKeyToCheck)))
            {
                if (doesConsumeItem)
                {
                    Player.Instance.RemoveInventoryItem(inventoryDictKeyToCheck);
                }

                if (onDestroyAudioClip)
                {
                    AudioPlayer.Instance.PlaySFX(onDestroyAudioClip, onDestroyAudioVolume);
                }
                Destroy(gameObject);
            }
            else
            {
                AudioPlayer.Instance.PlaySFX(accessDeniedAudioClip, accessDeniedAudioVolume);
            }
        }
    }
}
