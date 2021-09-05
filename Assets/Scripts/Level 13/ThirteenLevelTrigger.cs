using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirteenLevelTrigger : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private GameObject levelState;

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
        if (collision.gameObject == Player.Instance.gameObject && levelState.GetComponent<LevelState>().state == LevelState.State.Checkpoint1)
        {
            Player.Instance.hasControl = false;
            GameManager.Instance.FadeLoadLevel(nextLevel);
        }
    }
}
