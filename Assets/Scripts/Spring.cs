using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private AudioClip bounceAudioClip;
    [SerializeField] private int bounceAudioClipVolume = 100;
    [SerializeField] private float bouncePower = 20;

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
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("Bounce");
            AudioPlayer.Instance.PlaySFX(bounceAudioClip, bounceAudioClipVolume);
            Player.Instance.Bounce(bouncePower);
        }
    }
}
