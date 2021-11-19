using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private string entryLevel;
    [SerializeField] private AudioClip selectSFX;
    [Range(1, 100)] [SerializeField] private int selectAudioVolume = 100;

    [SerializeField] private GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if (!Prefs.HasCurrentLevel())
        {
            Image continueButtonImage = continueButton.GetComponent<Image>();
            Color continueButtonColor = continueButtonImage.color;
            Button continueButtonInput = continueButton.GetComponent<Button>();
            continueButtonInput.interactable = false;
            Debug.Log(continueButtonImage.color);
            continueButtonColor.a = 0.5f;
            continueButtonImage.color = continueButtonColor;
            Debug.Log(continueButtonImage.color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
    }

    public void PlayButtonHandler()
    {
        PlaySound();
        SceneManager.LoadScene(entryLevel);
    }

    public void ContinueButtonHandler()
    {
        if (Prefs.HasCurrentLevel())
        {
            PlaySound();
            string currentLevel = Prefs.GetCurrentLevel();
            SceneManager.LoadScene(currentLevel);
        }
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
