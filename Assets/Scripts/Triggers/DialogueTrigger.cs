using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] public Dialogue mainDialogue;
    [SerializeField] private Dialogue secondDialogue;
    [SerializeField] private Dialogue thirdDialogue;
    [SerializeField] private Dialogue fourthDialogue;

    [SerializeField] protected bool isChainDialogueTrigger = false;
    [SerializeField] private bool hasChainDialogueTrigger = false;
    [SerializeField] private DialogueTrigger chainDialogueTrigger;

    protected bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        if (hasChainDialogueTrigger)
        {
            fourthDialogue.chainDialogue = chainDialogueTrigger.mainDialogue;
        }

        if (fourthDialogue != null)
        {
            thirdDialogue.chainDialogue = fourthDialogue;
        }
        if (thirdDialogue != null)
        {
            secondDialogue.chainDialogue = thirdDialogue;
        }
        if (secondDialogue != null)
        {
            mainDialogue.chainDialogue = secondDialogue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used && !isChainDialogueTrigger && collision.gameObject == Player.Instance.gameObject)
        {
            DialogBox dialogBox = FindObjectOfType<DialogBox>();
            dialogBox.InitializeDialogue(mainDialogue);
            used = true;
        }
    }
}
