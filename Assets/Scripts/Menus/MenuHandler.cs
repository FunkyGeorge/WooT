using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private string entryLevel;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenuDefaultElement;
    [SerializeField] private GameObject settingsDefaultElement;

    [Header("Audio")]
    [SerializeField] private AudioClip musicClip;
    [Range(1, 100)][SerializeField] private int musicClipVolume = 100;
    [SerializeField] private AudioClip selectSFX;
    [Range(1, 100)] [SerializeField] private int selectAudioVolume = 100;

    [SerializeField] private GameObject continueButton;

    [Header("Settings")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown resDropdown;
    [SerializeField] private List<Vector2> resolutions = new List<Vector2>();

    [Header("Debugging")]
    [SerializeField] private ConfigScriptableObject config;
    [SerializeField] private string debugEntryLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (!Prefs.HasCurrentLevel() && !config.isDebug)
        {
            Image continueButtonImage = continueButton.GetComponent<Image>();
            Color continueButtonColor = continueButtonImage.color;
            Button continueButtonInput = continueButton.GetComponent<Button>();
            continueButtonInput.interactable = false;
            continueButtonColor.a = 0.5f;
            continueButtonImage.color = continueButtonColor;
        }

        AudioPlayer.Instance.PlayMusic(musicClip, musicClipVolume);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
    }

    public void PlayButtonHandler()
    {
        PlaySound();
        StartCoroutine(FadeToGame(entryLevel));
    }

    public void ContinueButtonHandler()
    {
        PlaySound();

        string nextLevel = config.isDebug || config.isTrailer ? debugEntryLevel : Prefs.GetCurrentLevel();
        StartCoroutine(FadeToGame(nextLevel));
    }

    public void OnVolumeSliderChanged(Slider slider)
    {
        AudioPlayer.Instance.SetMasterVolume((int)slider.value);
    }

    public void OnResolutionChange()
    {
        int index = resDropdown.value;
        Screen.SetResolution(((int)resolutions[index].x), ((int)resolutions[index].y), FullScreenMode.Windowed);
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

    public void ToggleMenu()
    {
        if (mainMenuDefaultElement.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuDefaultElement);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(settingsDefaultElement);
        }
    }

    private IEnumerator FadeToGame(string level)
    {
        AudioPlayer.Instance.FadeMusicVolume(1);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(level);
        yield return null;
    }

    // Input
    private void OnLeftStick(InputValue value)
    {
        if (!EventSystem.current.currentSelectedGameObject)
        {
            if (mainMenuDefaultElement.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(mainMenuDefaultElement);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(settingsDefaultElement);
            }
        }
    }
}
