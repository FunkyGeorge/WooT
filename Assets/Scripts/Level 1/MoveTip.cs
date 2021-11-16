using UnityEngine;

public class MoveTip : Tip
{
    [SerializeField] private float showTipDelay = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowMoveTip", showTipDelay);
    }

    private void ShowMoveTip()
    {
        SetTipBox();
    }
}
