using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;
    private bool used = false;

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
        if (!used && collision.gameObject == Player.Instance.gameObject)
        {
            used = true;
            cutscene.Play();
        }
    }

    private void onCutsceneStopped()
    {
        Debug.Log("read me");
    }
}
