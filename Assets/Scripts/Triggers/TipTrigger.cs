using UnityEngine;

public class TipTrigger : Tip
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance.gameObject == collision.gameObject && !used && !isActive)
        {
            SetTipBox();
        }
    }
}
