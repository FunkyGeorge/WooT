using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue jesseDialogue;
    [SerializeField] private Dialogue adminDialogue;
    [SerializeField] private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.gameObject == Player.Instance.gameObject)
        {
            // Should only be able to get here after checkpoint 2
            FindObjectOfType<LevelState>().state = LevelState.State.Checkpoint3;
            hasTriggered = true;

            DialogBox dialogBox = FindObjectOfType<DialogBox>();
            jesseDialogue.chainDialogue = adminDialogue;
            dialogBox.InitializeDialogue(jesseDialogue);
        }
    }
}
