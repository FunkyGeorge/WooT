using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [Header("Timing Config")]
    [SerializeField] private float initialDelay = 0;

    [Header("Dialog Sequence")]
    [SerializeField] private Dialogue[] dialoguePart;

    [SerializeField] private TimelineAsset cutsceneClip;
    private PlayableDirector director;

    private DialogBox dialogBox;
    private bool hasPlayedCutscene = false;
    private bool hasGoneThroughDialogue = false;
    private bool cutsceneIsPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        director = GetComponent<PlayableDirector>();
        UpdateTimeline();

        PrepDialogue();
        director.stopped += OnTimelineFinished;
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTimeline()
    {
        // This method will apply the current Player animator component to the timeline since it
        // will likely be different than the one it was created with.

        PlayableAsset originalPlayable = director.playableAsset;
        // PlayableAsset newPlayable;

        var oldBindings = originalPlayable.outputs.GetEnumerator();
 
        while (oldBindings.MoveNext())
        {
            var oldBindings_sourceObject = oldBindings.Current.sourceObject;
            if (oldBindings_sourceObject)
            {
                Object something = director.GetGenericBinding(oldBindings_sourceObject);
                director.SetGenericBinding(
                    oldBindings_sourceObject,
                    Player.Instance.gameObject.GetComponent<Animator>()
                );
            } 
        }

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
        Player.Instance.hasControl = false;
        yield return new WaitForSeconds(initialDelay);
        ProgressCutscene();
    }

    private void ProgressCutscene()
    {
        if (!hasPlayedCutscene)
        {
            hasPlayedCutscene = true;
            cutsceneIsPlaying = true;
            director.Play();
        }
        else if (dialogBox.IsEmpty() && !hasGoneThroughDialogue)
        {
            hasGoneThroughDialogue = true;
            dialogBox.InitializeDialogue(dialoguePart[0]);
        }
        else if (dialogBox.IsEmpty() && hasGoneThroughDialogue)
        {
            GameManager.Instance.FadeLoadLevel(nextScene);
        }
    }

    private void OnTimelineFinished(PlayableDirector obj)
    {
        ProgressCutscene();
        cutsceneIsPlaying = false;
    }

    // Player Input System
    private void OnContinue(InputValue value)
    {
        if (!cutsceneIsPlaying && dialogBox.IsEmpty())
        {
            ProgressCutscene();
        }
    }
}
