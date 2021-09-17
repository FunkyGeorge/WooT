using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDialogueTrigger : DialogueTrigger
{
    [Header("Final Dialogue Settings")]
    [SerializeField] private FinalChoiceHandler finalChoiceHandler;
    private DialogBox dialogBox;


    private IEnumerator OnDialogueFinished()
    {
        for (;;)
        {
            if (dialogBox.IsEmpty())
            {
                finalChoiceHandler.InitiateChoicePromptPanel();
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Override
        if (!used && !isChainDialogueTrigger && collision.gameObject == Player.Instance.gameObject)
        {
            dialogBox = FindObjectOfType<DialogBox>();
            dialogBox.InitializeDialogue(mainDialogue);
            used = true;
            StartCoroutine(OnDialogueFinished());
        }
    }
}
