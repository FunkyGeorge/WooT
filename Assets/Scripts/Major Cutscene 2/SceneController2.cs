using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SceneController2 : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [Header("Timing Config")]
    [SerializeField] private float initialDelay = 0;

    [Header("Dialog Sequence")]
    [SerializeField] private Dialogue[] dialoguePart;

    [SerializeField] private TimelineAsset cutsceneClip;

    private DialogBox dialogBox;
    private bool hasGoneThroughDialogue = false;
    private bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        SpriteRenderer spriteRenderer = Player.Instance.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;

        PrepDialogue();
        Player.Instance.canShoot = false;
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrepDialogue()
    {
        for (int i = dialoguePart.Length - 1; i > 0; i--)
        {
            dialoguePart[i - 1].chainDialogue = dialoguePart[i];
        }
    }

    private IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(initialDelay);
        // ProgressCutscene();

        // Poll to for cutscene progress
        for (;;)
        {
            if (dialogBox.IsEmpty())
            {
                ProgressCutscene();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void ProgressCutscene()
    {
        if (dialogBox.IsEmpty() && !hasGoneThroughDialogue)
        {
            hasGoneThroughDialogue = true;
            dialogBox.InitializeDialogue(dialoguePart[0]);
        }
        else if (dialogBox.IsEmpty() && hasGoneThroughDialogue && !isComplete)
        {
            if (SteamManager.Initialized)
            {
                SteamUserStats.SetAchievement("ACT1");
                SteamUserStats.StoreStats();
            }
            GameManager.Instance.FadeLoadLevel(nextScene);
            Player.Instance.canShoot = true;
            isComplete = true;
        }
    }
}
