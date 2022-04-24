using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class SceneController3 : MonoBehaviour
{
    [SerializeField] private ConfigScriptableObject configScriptable;
    [SerializeField] private string nextScene;
    [SerializeField] private string demoNextScene;
    [SerializeField] private Animator hectorAnimator;

    [Header("Timing Config")]
    [SerializeField] private float initialDelay = 0;

    [Header("Playables")]
    [SerializeField] private PlayableAsset playerIntro;
    [SerializeField] private PlayableAsset hectorTurn;

    [Header("Dialog Sequence")]
    [SerializeField] private Dialogue[] dialoguePart;

    private PlayableDirector director;

    private DialogBox dialogBox;
    private bool hasPlayedWalkinCutscene = false;
    private bool hasPlayedHectorTurnCutscene = false;
    private bool hasGoneThroughDialogue = false;
    private bool cutsceneIsPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        director = GetComponent<PlayableDirector>();

        PrepDialogue();
        director.stopped += OnTimelineFinished;
        Player.Instance.canShoot = false;
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTimeline(Animator animator)
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
                    animator
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
        yield return new WaitForSeconds(initialDelay);

        // Poll to for cutscene progress
        for (;;)
        {
            if (!cutsceneIsPlaying && dialogBox.IsEmpty())
            {
                ProgressCutscene();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void ProgressCutscene()
    {
        if (dialogBox.IsEmpty() && !hasPlayedWalkinCutscene)
        {
            hasPlayedWalkinCutscene = true;
            cutsceneIsPlaying = true;
            SetPlayableAsset(playerIntro);
            director.Play();
        }
        else if (dialogBox.IsEmpty() && !hasPlayedHectorTurnCutscene)
        {
            hasPlayedHectorTurnCutscene = true;
            cutsceneIsPlaying = true;
            director.playableAsset = hectorTurn;
            UpdateTimeline(hectorAnimator);
            director.Play();
        }
        else if (dialogBox.IsEmpty() && !hasGoneThroughDialogue)
        {
            hasGoneThroughDialogue = true;
            dialogBox.InitializeDialogue(dialoguePart[0]);
        }
        else if (dialogBox.IsEmpty() && hasGoneThroughDialogue)
        {
            if (configScriptable.isDemo)
            {
                GameManager.Instance.FadeLoadLevel(demoNextScene);
            }
            else {
                GameManager.Instance.FadeLoadLevel(nextScene);
            }
            Player.Instance.canShoot = true;
        }
    }

    private void SetPlayableAsset(PlayableAsset playable)
    {
        director.playableAsset = playable;
        UpdateTimeline(Player.Instance.gameObject.GetComponent<Animator>());
    }

    private void OnTimelineFinished(PlayableDirector obj)
    {
        cutsceneIsPlaying = false;
    }
}
