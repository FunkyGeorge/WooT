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
        Animator animator = transitions.GetComponent<Animator>();
        yield return new WaitForSeconds(2);
        Destroy(transitions);
    }
}
