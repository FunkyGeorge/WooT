using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneController5 : MonoBehaviour
{
    [SerializeField] private GameObject bossSprite;
    [SerializeField] private SpriteRenderer hectorSpriteRenderer;
    [SerializeField] private string nextScene;
    [SerializeField] private DialogBox dialogBox;
    [SerializeField] private Dialogue[] dialoguePart1;
    [SerializeField] private Dialogue[] dialoguePart2;
    private Vector2 dropLocation = new Vector2(-2, 3);
    private bool playedFirstDialogue = false;
    private bool playedSecondDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        PrepDialogue();
        Player.Instance.canShoot = false;
        Player.Instance.hasControl = false;
        StartCoroutine(SceneProcedure());
    }

    private void PrepDialogue()
    {
        for (int i = dialoguePart1.Length - 1; i > 0; i--)
        {
            dialoguePart1[i - 1].chainDialogue = dialoguePart1[i];
        }

        for (int i = dialoguePart2.Length - 1; i > 0; i--)
        {
            dialoguePart2[i - 1].chainDialogue = dialoguePart2[i];
        }
    }

    private IEnumerator SceneProcedure()
    {
        // Start Drone shaking
        bossSprite.transform.DORotate(new Vector3(0, 0, -6), 0.1f);
        bossSprite.transform.DORotate(new Vector3(0, 0, 6), 0.4f).SetEase(Ease.InOutBounce).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(3f);

        for (;;)
        {
            if (!playedFirstDialogue)
            {
                playedFirstDialogue = true;
                dialogBox.InitializeDialogue(dialoguePart1[0]);
            } else if (dialogBox.IsEmpty() && playedFirstDialogue && !playedSecondDialogue)
            {
                hectorSpriteRenderer.flipX = false;
                yield return new WaitForSeconds(2f);
                playedSecondDialogue = true;
                dialogBox.InitializeDialogue(dialoguePart2[0]);
            } else if (dialogBox.IsEmpty() && playedSecondDialogue)
            {
                yield return new WaitForSeconds(1f);
                bossSprite.transform.DOMove(dropLocation, 0.2f);
                GameManager.Instance.FadeLoadLevel(nextScene);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
