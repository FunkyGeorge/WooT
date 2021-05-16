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
        UpdateCutscene();
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

    private void UpdateCutscene()
    {
        // This method will apply the current Player animator component to the timeline since it
        // will likely be different than the one it was created with.

        PlayableAsset originalPlayable = cutscene.playableAsset;
        // PlayableAsset newPlayable;

        var oldBindings = originalPlayable.outputs.GetEnumerator();
 
        while (oldBindings.MoveNext())
        {
            var oldBindings_sourceObject = oldBindings.Current.sourceObject;
            if (oldBindings_sourceObject)
            {
                Object something = cutscene.GetGenericBinding(oldBindings_sourceObject);
                cutscene.SetGenericBinding(
                    oldBindings_sourceObject,
                    Player.Instance.gameObject.GetComponent<Animator>()
                );
            } 
        }

    }
}
