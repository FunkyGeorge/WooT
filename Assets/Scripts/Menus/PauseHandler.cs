using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseHandler : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainPauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenuDefaultElement;
    [SerializeField] private GameObject settingsDefaultElement;

    [SerializeField] private ConfigScriptableObject config;
    [SerializeField] private GameObject debugBar;
    [SerializeField] private AudioClip selectSFX;
    [Range(1, 100)] [SerializeField] private int selectAudioVolume = 100;
    [SerializeField] private GameObject defaultMenuItem;
    [SerializeField] private DeathManagerScriptableObject deathManager;
    [SerializeField] private TMP_Text deathCountText;

    [Header("Settings")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown resDropdown;
    [SerializeField] private List<Vector2> resolutions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = Prefs.GetMasterVolume();

        if (deathManager)
        {
            deathManager.deathEvent.AddListener(AdjustDeathCount);
            deathCountText.text = SceneManager.GetActiveScene().name == "Level 1" ? "0" : deathManager.deathCount.ToString();
        }

        if (config.isDemo || config.isReview)
        {
            debugBar.SetActive(true);
        }
    }

    void OnDisable()
    {
        if (deathManager)
        {
            deathManager.deathEvent.RemoveListener(AdjustDeathCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        // volumeSlider.value = Prefs.GetMasterVolume();
        EventSystem.current.SetSelectedGameObject(defaultMenuItem);
        mainPauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        mainPauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
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

    private void AdjustDeathCount(int deathCount)
    {
        deathCountText.text = deathCount.ToString();
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

    public void ToggleMenu()
    {
        if (pauseMenuDefaultElement.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(pauseMenuDefaultElement);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(settingsDefaultElement);
        }
    }

    // Input
    private void OnLeftStick(InputValue value)
    {
        if (GameManager.Instance.isPaused && !EventSystem.current.currentSelectedGameObject)
        {
            if (pauseMenuDefaultElement.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(pauseMenuDefaultElement);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(settingsDefaultElement);
            }
        }
    }
}
