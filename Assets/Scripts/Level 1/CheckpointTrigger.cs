using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue firstDialogue;
    [SerializeField] private Dialogue secondDialogue;

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
        if (collision.gameObject == Player.Instance.gameObject && FindObjectOfType<LevelState>().state == LevelState.State.Checkpoint0)
        {
            DialogBox dialogBox = FindObjectOfType<DialogBox>();
            dialogBox.InitializeDialogue(firstDialogue);
            LevelState levelState = FindObjectOfType<LevelState>();
            levelState.state = LevelState.State.Checkpoint1;
        }

        if (collision.gameObject == Player.Instance.gameObject && FindObjectOfType<LevelState>().state == LevelState.State.Checkpoint1 && Player.Instance.inventory.ContainsKey("Keycard"))
        {
            DialogBox dialogBox = FindObjectOfType<DialogBox>();
            dialogBox.InitializeDialogue(secondDialogue);
            LevelState levelState = FindObjectOfType<LevelState>();
            levelState.state = LevelState.State.Checkpoint2;
        }
    }
}
