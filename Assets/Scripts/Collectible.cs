using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{


    public enum ItemType
    {
        Shard,
        Inventory
    }

    [SerializeField] private AudioClip onDestroyAudioClip;
    [SerializeField] [Range(1, 100)] private int onDestroyAudioVolume = 100;
    public ItemType type;
    public Sprite inventorySprite;
    [SerializeField] private bool checkpoint = false;
    private const string SPAWN_POINT = "Spawn Point";

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
        if (collision.gameObject == Player.Instance.gameObject)
        {
            if (onDestroyAudioClip)
            {
                AudioPlayer.Instance.PlaySFX(onDestroyAudioClip, onDestroyAudioVolume);
            }

            if (checkpoint)
            {
                GameObject.Find(SPAWN_POINT).transform.position = transform.position;
            }

            Destroy(gameObject);
        }
    }
}
