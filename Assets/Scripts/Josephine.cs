using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Josephine : MonoBehaviour
{
    [SerializeField] private bool trackMode = false;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trackMode)
        {
            if (Player.Instance.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
