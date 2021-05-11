using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CollapsingPlatform : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] float dropDelay = 0.75f;
    [SerializeField] float returnDelay = 3f;
    private Rigidbody2D rb;
    private bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(dropDelay);
        director.Play();
        yield return new WaitForSeconds(returnDelay);
        rb.position = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        isFalling = false;
        yield return null;
    }
}
