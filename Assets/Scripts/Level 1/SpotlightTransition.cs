using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightTransition : MonoBehaviour
{
    [SerializeField] private GameObject transitionsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteTransition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ExecuteTransition()
    {
        GameObject transitions = Instantiate(transitionsPrefab);
        // GameObject slider = GameObject.Find("Spotlight");
        Animator animator = transitions.GetComponent<Animator>();

        animator.SetTrigger("start");
        yield return new WaitForSeconds(2);
        Destroy(transitions);
    }
}
