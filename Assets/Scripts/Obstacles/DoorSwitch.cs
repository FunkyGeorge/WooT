using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{

    [SerializeField] private GameObject door;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private AudioClip sfxClip;
    [Range(1, 100)][SerializeField] private int sfxVolume = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactivateDoor()
    {
        door.SetActive(false);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = brokenSprite;
        collider.enabled = false;
        AudioPlayer.Instance.PlaySFX(sfxClip, sfxVolume);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            DeactivateDoor();
        }
    }
}
