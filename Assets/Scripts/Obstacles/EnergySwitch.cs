using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySwitch : MonoBehaviour
{

    [SerializeField] private GameObject energy;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private AudioClip sfxClip;
    [Range(1, 100)][SerializeField] private int sfxVolume = 100;
    private Sprite defaultSprite;
    private bool isGoingToReset = false;
    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.isDying && !isGoingToReset)
        {
            isGoingToReset = true;
            // Time is based on time it takes for transition to cover the screen
            Invoke("ResetState", 0.6f);
        }
    }

    void DeactivateDoor()
    {
        energy.SetActive(false);
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

    void ResetState()
    {
        isGoingToReset = false;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = true;
        spriteRenderer.sprite = defaultSprite;
        energy.SetActive(true);
    }
}
