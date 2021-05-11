using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DialogueScene1 : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private string nextScene;
    
    // Start is called before the first frame update
    void Start()
    {
        playableDirector.Play();
        Invoke("GoToNextScene", 7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
