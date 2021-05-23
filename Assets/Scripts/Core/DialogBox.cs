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
    [SerializeField] private Image continueIcon;

    [Header("Character Images")]
    [SerializeField] private Sprite anonymousImage;
    [SerializeField] private Sprite jessePortrait;
    [SerializeField] private Sprite adminPortrait;

    public Queue<string> dialogueQueue;
    private Dialogue chainDialogue;

    [SerializeField] private int typeInterval = 5;
    [SerializeField] private float typeDelay = 0.2f;
    private string currentText;
    private bool isTyping = false;
    private int typeCursor = 0;

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
            leftImage.sprite = jessePortrait;
            leftImage.enabled = true;
            rightImage.enabled = false;
        }
        else if (dialogue.name != "")
        {
            Sprite characterImage = FindCharacterImage(dialogue.name);
            rightImage.sprite = characterImage;
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
            // dialogText.text = dialogueQueue.Dequeue();
            // continueIcon.enabled = true;
            StartCoroutine(Typer(dialogueQueue.Dequeue()));
            GameManager.Instance.isDialogUp = true;
        }
    }

    private IEnumerator Typer(string newText)
    {
        currentText = newText;
        typeCursor = 0;
        isTyping = true;
        continueIcon.enabled = false;

        for (;;)
        {
            if (!isTyping) break;
            typeCursor += typeInterval;
            if (typeCursor >= currentText.Length)
            {
                FullText();
                break;
            }
            dialogText.text = currentText.Substring(0, typeCursor);
            yield return new WaitForSeconds(typeDelay);
        }
    }

    private void FullText()
    {
        dialogText.text = currentText;
        isTyping = false;
        continueIcon.enabled = true;
    }

    private Sprite FindCharacterImage(string characterName)
    {
        switch (characterName)
        {
            case "Admin":
                return adminPortrait;
            default:
                return anonymousImage;
        }
    }

    public bool IsEmpty()
    {
        return dialogueQueue.Count == 0 && chainDialogue == null && !GameManager.Instance.isDialogUp;
    }

    public void DisableText()
    {
        dialogText.text = "";
        continueIcon.enabled = false;
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
            if (!isTyping)
            {
                NextDialogue();
            }
            else
            {
                FullText();
            }
        }
    }
}
