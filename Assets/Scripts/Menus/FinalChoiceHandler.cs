using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FinalChoiceHandler : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private string nextScene;
    [SerializeField] private bool didStopProjectGentleGoodnight = false;
    [SerializeField] private AudioClip selectSFX;
    [Range(1, 100)] [SerializeField] private int selectAudioVolume = 100;
    [SerializeField] private ConfigScriptableObject config;

    [Header("Choice Prompt Form")]
    [SerializeField] private GameObject choicePromptPanel;
    [SerializeField] private TMP_Text descriptionText;
    [TextArea(3, 5)] [SerializeField] private string yesDecisionText = "";
    [TextArea(3, 5)] [SerializeField] private string noDecisionText = "";

    [Header("Confirmation Step")]
    [SerializeField] public Dialogue confirmationDialogue;

    [Header("Explanation Form")]
    [SerializeField] private GameObject explanationPromptPanel;
    [SerializeField] private TMP_Text explanationCounter;
    [SerializeField] private TMP_InputField explanationInput;
    [SerializeField] private TMP_Text explanationInputText;
    private string counterTemplate = "/85";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //----------------------------------------------
    // Choice Prompt Form
    //----------------------------------------------
    public void InitiateChoicePromptPanel()
    {
        Player.Instance.hasControl = false;
        GameManager.Instance.shouldForceCursorVisibility = true;
        choicePromptPanel.SetActive(true);
    }


    public void YesButtonHandler()
    {
        didStopProjectGentleGoodnight = true;
        PlaySound();
        GameManager.Instance.shouldForceCursorVisibility = false;
        choicePromptPanel.SetActive(false);
        DialogBox dialogBox = FindObjectOfType<DialogBox>();
        dialogBox.InitializeDialogue(confirmationDialogue);
        StartCoroutine(OnDialogueFinished());
    }

    public void YesButtonHoverHandler()
    {
        PlaySound();
        EventSystem.current.SetSelectedGameObject(null);
        descriptionText.text = yesDecisionText;
    }

    public void NoButtonHandler()
    {
        didStopProjectGentleGoodnight = false;
        PlaySound();
        GameManager.Instance.shouldForceCursorVisibility = false;
        choicePromptPanel.SetActive(false);
        DialogBox dialogBox = FindObjectOfType<DialogBox>();
        dialogBox.InitializeDialogue(confirmationDialogue);
        StartCoroutine(OnDialogueFinished());
    }

    public void NoButtonHoverHandler()
    {
        PlaySound();
        EventSystem.current.SetSelectedGameObject(null);
        descriptionText.text = noDecisionText;
    }


    //----------------------------------------------
    // Explanation Form
    //----------------------------------------------
    public void InitiateExplanationPanel()
    {
        Player.Instance.hasControl = false;
        // Default to 89 characters for debugging
        counterTemplate = config.isDebug ? "/89" : "/" + Player.Instance.shardsCollected.ToString();
        explanationInput.characterLimit = config.isDebug ? 89 : Player.Instance.shardsCollected;
        explanationCounter.text = "0" + counterTemplate;
        GameManager.Instance.shouldForceCursorVisibility = true;
        explanationPromptPanel.SetActive(true);
    }

    public void OnExplanationTextChange()
    {
        explanationCounter.text = explanationInputText.text.Length.ToString() + counterTemplate;
    }

    private IEnumerator OnDialogueFinished()
    {
        DialogBox dialogBox = FindObjectOfType<DialogBox>();
        for (;;)
        {
            if (dialogBox.IsEmpty())
            {
                InitiateExplanationPanel();
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        
    }

    public void SubmitButtonHandler()
    {
        Debug.Log("Stopped Gentle Goodnight:" + didStopProjectGentleGoodnight.ToString());
        if (SteamManager.Initialized)
        {
            string achievementName = didStopProjectGentleGoodnight ? "CHOICE_JESSE" : "CHOICE_HECTOR";
            SteamUserStats.SetAchievement(achievementName);
            SteamUserStats.StoreStats();
        }

        GameManager.Instance.FadeLoadLevel(nextScene);
    }

    public void BackButtonHandler()
    {
        explanationPromptPanel.SetActive(false);
        InitiateChoicePromptPanel();
    }

    public void PlaySound()
    {
        AudioPlayer.Instance.PlaySFX(selectSFX, selectAudioVolume);
    }
}
