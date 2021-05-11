using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [Header("Timing Config")]
    [SerializeField] private float initialDelay = 0;

    [Header("Dialog Sequence")]
    [SerializeField] private Dialogue[] dialoguePart;
    private PlayableDirector director;

    private DialogBox dialogBox;
    private bool cutsceneIsPlaying = false;
    private int dialogueCursor = 0;
    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox = FindObjectOfType<DialogBox>();
        director = GetComponent<PlayableDirector>();
        director.stopped += OnTimelineFinished;
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Cutscene()
    {
        Player.Instance.hasControl = false;
        yield return new WaitForSeconds(initialDelay);
        step++;
        dialogBox.InitializeDialogue(dialoguePart[dialogueCursor++]);
    }

    private void ProgressCutscene()
    {
        switch (step)
        {
            case 1:
                step++;
                cutsceneIsPlaying = true;
                director.Play();
                break;
            case 2:
                step++;
                cutsceneIsPlaying = false;
                break;
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
                step++;
                dialogBox.InitializeDialogue(dialoguePart[dialogueCursor++]);
                break;
            case 17: step++; break;
            case 18:
                SceneManager.LoadScene(nextScene);
                break;
            default: Debug.Log("Missing case"); break;
        }
    }

    private void OnTimelineFinished(PlayableDirector obj)
    {
        ProgressCutscene();
    }

    // Player Input System
    private void OnContinue(InputValue value)
    {
        if (step > 0 && dialogBox.dialogueQueue.Count == 0 && !cutsceneIsPlaying)
        {
            // dialog should be finished
            Debug.Log("Start next action: " + step.ToString());
            ProgressCutscene();
        }
    }
}
