using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private AudioClip bounceAudioClip;
    [SerializeField] private int bounceAudioClipVolume = 100;
    [SerializeField] private float bouncePower = 20;
    private bool isLoaded = true;
    private float carryDistanceCheck = 0.1f;
    private float springCooldown = 0.3f;
    private Rigidbody2D rb;
    RaycastHit2D[] hits;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoaded)
        {
            hits = new RaycastHit2D[16];
            int hitCount = rb.Cast(Vector2.up, hits, carryDistanceCheck);
            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i].collider.gameObject == Player.Instance.gameObject)
                {
                    Animator animator = gameObject.GetComponent<Animator>();
                    animator.SetTrigger("Bounce");
                    AudioPlayer.Instance.PlaySFX(bounceAudioClip, bounceAudioClipVolume);
                    Player.Instance.Bounce(bouncePower);
                    isLoaded = false;
                    Invoke("LoadSpring", springCooldown);
                }
            }
        }
    }

    void LoadSpring()
    {
        isLoaded = true;
    }
}
