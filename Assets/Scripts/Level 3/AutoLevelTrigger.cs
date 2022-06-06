using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLevelTrigger : DialogueTrigger
{
    [SerializeField] private string nextScene;
    private DialogBox dialogBoxCheck;
    private bool hasNavigated = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogBoxCheck = FindObjectOfType<DialogBox>();

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
        if (used && dialogBoxCheck.IsEmpty() && !hasNavigated)
        {
            hasNavigated = true;
            GameManager.Instance.FadeLoadLevel(nextScene);
        }
    }
}
