using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.canShoot = true;
        }
    }
}
