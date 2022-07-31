using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

public class CollapsingPlatform : MonoBehaviour
{
    [Header("Animation Config")]
    [SerializeField] PlayableDirector director;
    [SerializeField] private float dropDelay = 0.75f;
    [SerializeField] private float returnDelay = 3f;
    [SerializeField] private DeathManagerScriptableObject deathManager;
    private Tilemap tilemap;
    private TilemapCollider2D tileCollider;

    [Header("Sound Config")]
    [SerializeField] private AudioClip soundClip;
    [Range(1, 100)][SerializeField] private int soundVolume = 65;
    private Rigidbody2D rb;
    private bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;

        if (deathManager)
        {
            deathManager.deathEvent.AddListener(ResetPosition);
            tilemap = GetComponent<Tilemap>();
            tileCollider = GetComponent<TilemapCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        if (deathManager)
        {
            deathManager.deathEvent.RemoveListener(ResetPosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            if (!isFalling)
            {
                isFalling = true;
                StartCoroutine(DropSequence());
            }
        }
    }

    IEnumerator DropSequence()
    {
        AudioPlayer.Instance.PlaySFX(soundClip, soundVolume);
        yield return new WaitForSeconds(dropDelay);
        director.Play();
        yield return new WaitForSeconds(returnDelay);
        rb.position = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        isFalling = false;
        yield return null;
    }

    private void ResetPosition(int deathCount) {
        director.Stop();
        tileCollider.enabled = true;
        tilemap.color = new Color(1, 1, 1, 1);
        rb.position = Vector2.zero;
        isFalling = false;
    }
}
