using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuralTrigger : MonoBehaviour
{
    private const string SPAWN_POINT = "Spawn Point";

    [SerializeField] private Animator animator;
    private bool cleared = false;

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
        if (!cleared && collision.gameObject == Player.Instance.gameObject)
        {
            cleared = true;
            animator.SetBool("cleared", true);
            GameObject.Find(SPAWN_POINT).transform.position = transform.position;
        }
    }
}
