using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenAreaTrigger : MonoBehaviour
{
    [SerializeField] private Color originalColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private float enterAlpha = 0.25f;
    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.color = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            tilemap.color = new Color(1, 1, 1, enterAlpha);
        }
    }
}
