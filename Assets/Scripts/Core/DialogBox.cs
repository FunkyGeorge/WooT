using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


public class DialogBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Image leftImage;
    [SerializeField] private Image rightImage;
    [SerializeField] private TMP_Text continueIcon;
    [SerializeField] private Sprite anonymousImage;

    public Queue<string> dialogueQueue;
    private Dialogue chainDialogue;

    // Start is called before the first frame update
    void Start()
    {
        DisableText();
        dialogueQueue = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeDialogue(Dialogue dialogue)
    {
        dialogueQueue.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            dialogueQueue.Enqueue(sentence);
        }

        if (dialogue.name == "Jesse")
        {
            leftImage.sprite = anonymousImage;
            leftImage.enabled = true;
            rightImage.enabled = false;
        }
        else if (dialogue.name != "")
        {
            rightImage.sprite = anonymousImage;
            rightImage.enabled = true;
            leftImage.enabled = false;
        }
        else
        {
            leftImage.enabled = false;
            rightImage.enabled = false;
        }
        chainDialogue = dialogue.chainDialogue;
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            if (chainDialogue != null)
            {
                InitializeDialogue(chainDialogue);
            }
            else
            {
                DisableText();
            }
        }
        else
        {
            Player.Instance.hasControl = false;
            gameObject.GetComponent<Image>().enabled = true;
            dialogText.text = dialogueQueue.Dequeue();
            continueIcon.text = ".";
            GameManager.Instance.isDialogUp = true;
        }
    }

    public bool IsEmpty()
    {
        return dialogueQueue.Count == 0 && chainDialogue == null;
    }

    public void DisableText()
    {
        dialogText.text = "";
        continueIcon.text = "";
        leftImage.enabled = false;
        rightImage.enabled = false;
        leftImage.sprite = null;
        rightImage.sprite = null;
        Player.Instance.hasControl = true;
        gameObject.GetComponent<Image>().enabled = false;
        GameManager.Instance.isDialogUp = false;
        chainDialogue = null;
    }

    // Player Input System
    private void OnContinue(InputValue value)
    {
        if (GameManager.Instance.isDialogUp)
        {
            NextDialogue();
        }
    }
}
