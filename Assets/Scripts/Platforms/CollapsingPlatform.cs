using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class CollapsingPlatform : MonoBehaviour
{
    [Header("Animation Config")]
    [SerializeField] PlayableDirector director;
    [SerializeField] private DeathManagerScriptableObject deathManager;
    private float dropDelay = 0.75f;
    private float returnDelay = 2f;
    private Tilemap tilemap;
    private TilemapCollider2D tileCollider;
    private CompositeCollider2D compCollider;

    [Header("Sound Config")]
    [SerializeField] private AudioClip soundClip;
    [Range(1, 100)][SerializeField] private int soundVolume = 65;
    private Rigidbody2D rb;
    private bool isFalling = false;
    private bool isReturning = false;
    private bool isTouchingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;
        tilemap = GetComponent<Tilemap>();
        tileCollider = GetComponent<TilemapCollider2D>();
        compCollider = GetComponent<CompositeCollider2D>();

        if (deathManager)
        {
            deathManager.deathEvent.AddListener(ResetPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isReturning)
        {
            // Try returning 
            transform.position = Vector3.zero;
            isTouchingPlayer = compCollider.IsTouching(Player.Instance.gameObject.GetComponent<BoxCollider2D>());
            if (!isTouchingPlayer)
            {
                isReturning = false;
                ResetPosition(0);
            }
        }
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
        tilemap.color = new Color(0.7f, 0.7f, 0.7f, 1);
        yield return new WaitForSeconds(dropDelay);
        director.Play();
        transform.DOLocalMoveY(-1f, 0.45f);
        yield return new WaitForSeconds(0.15f);
        compCollider.isTrigger = true;
        yield return new WaitForSeconds(returnDelay);
        isReturning = true;
        isFalling = false;
    }

    private void ResetPosition(int deathCount) {
        tilemap.color = new Color(1, 1, 1, 1);
        compCollider.isTrigger = false;
        transform.position = Vector3.zero;
        isReturning = false;
        isFalling = false;
    }
}
