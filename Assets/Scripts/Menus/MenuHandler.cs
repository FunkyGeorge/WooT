using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private string entryLevel;
    [SerializeField] private AudioClip selectSFX;
    [Range(1, 100)] [SerializeField] private int selectAudioVolume = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonHandler()
    {
        PlaySound();
        SceneManager.LoadScene(entryLevel);
    }

    public void ContinueButtonHandler()
    {
        PlaySound();
        string currentLevel = Prefs.GetCurrentLevel();
    }

    public void QuitToDesktop()
    {
        PlaySound();
        Application.Quit();
    }

    public void Deselect()
    {
        PlaySound();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PlaySound()
    {
        AudioPlayer.Instance.PlaySFX(selectSFX, selectAudioVolume);
    }
}
