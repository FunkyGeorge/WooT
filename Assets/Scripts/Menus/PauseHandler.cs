using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
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

    public void ResumeButtonHandler()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void ToMainMenuHandler()
    {
        GameManager.Instance.UnpauseGame();
        SceneManager.MoveGameObjectToScene(GameManager.Instance.gameObject, SceneManager.GetActiveScene());
        Player.Instance.PrepareForDestroy();
        SceneManager.MoveGameObjectToScene(Player.Instance.gameObject, SceneManager.GetActiveScene());
        AudioPlayer.Instance.StopMusic();
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    // ?
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
