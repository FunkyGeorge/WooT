using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Text coinsText;
    public Image inventorySprite;
    public Sprite inventoryEmptySprite;
    [SerializeField] private GameObject transitionsPrefab;
    [SerializeField] private GameObject fadeTransitionPrefab;
    [SerializeField] private ConfigScriptableObject config;

    [Header("State Flags")]
    public bool isDialogUp = false;
    public bool isPaused = false;
    public bool shouldForceCursorVisibility = false;

    [Header("Music Config")]
    public int currentMusicVolume = 100;
    [SerializeField] private AudioClip musicClip1;
    [Range(1, 100)][SerializeField] private int musicClip1Volume = 100;
    [SerializeField] private string[] music1Levels;

    [SerializeField] private AudioClip musicClip2;
    [Range(1, 100)][SerializeField] private int musicClip2Volume = 100;
    [SerializeField] private string[] music2Levels;

    [SerializeField] private AudioClip finalMusicClip;
    [Range(1, 100)][SerializeField] private int finalMusicClipVolume = 100;
    [SerializeField] private string[] finalMusicLevels;

    private const string coinTextName = "UI_CoinText";
    private const string inventorySpriteName = "UI_InventoryImage";
    private const string pauseMenuName = "Pause Menu";
    private const string feedbackMenuName = "Feedback Menu";

    private static GameManager _instance;
    public static GameManager Instance
    {
        get {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SetUIReferences();    

        SceneManager.sceneLoaded += OnLoadCallback;
        UpdateUI();
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused || shouldForceCursorVisibility || config.isDebug || SceneManager.GetActiveScene().name == "End Scene")
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    private void SetUIReferences()
    {
        GameObject coinTextObject = GameObject.Find(coinTextName);
        GameObject inventorySpriteObject = GameObject.Find(inventorySpriteName);

        if (coinTextObject) coinsText = coinTextObject.GetComponent<Text>();
        if (inventorySpriteObject) inventorySprite = inventorySpriteObject.GetComponent<Image>();
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        SetUIReferences();
        UpdateUI();
        PlayMusic();
    }

    public void UpdateUI()
    {
        if (Player.Instance != null)
        {
            if (coinsText) coinsText.text = Player.Instance.shardsCollected.ToString();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        GameObject pauseMenu = GameObject.Find(pauseMenuName);
        CanvasGroup canvas = pauseMenu.GetComponent<CanvasGroup>();
        PauseHandler pauseHandler = pauseMenu.GetComponent<PauseHandler>();
        pauseHandler.Init();
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        GameObject pauseMenu = GameObject.Find(pauseMenuName);
        CanvasGroup canvas = pauseMenu.GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenFeedback()
    {
        if (config.isDemo)
        {
            isPaused = true;
            Time.timeScale = 0f;
            GameObject feedbackMenu = GameObject.Find(feedbackMenuName);
            CanvasGroup canvas = feedbackMenu.GetComponent<CanvasGroup>();
            // PauseHandler pauseHandler = feedbackMenu.GetComponent<PauseHandler>();
            // pauseHandler.Init();
            canvas.alpha = 1f;
            canvas.blocksRaycasts = true;
        }
    }

    public void CloseFeedback()
    {
        if (config.isDemo)
        {
            isPaused = false;
            Time.timeScale = 1f;
            GameObject feedbackMenu = GameObject.Find(feedbackMenuName);
            CanvasGroup canvas = feedbackMenu.GetComponent<CanvasGroup>();
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void PlayMusic()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        for (int i = 0; i < music1Levels.Length; i++)
        {
            if (music1Levels[i] == currentLevel)
            {
                currentMusicVolume = musicClip1Volume;
                AudioPlayer.Instance.PlayMusic(musicClip1, musicClip1Volume);
                break;
            }
        }

        for (int i = 0; i < music2Levels.Length; i++)
        {
            if (music2Levels[i] == currentLevel)
            {
                currentMusicVolume = musicClip2Volume;
                AudioPlayer.Instance.PlayMusic(musicClip2, musicClip2Volume);
                break;
            }
        }

        for (int i = 0; i < finalMusicLevels.Length; i++)
        {
            if (finalMusicLevels[i] == currentLevel)
            {
                currentMusicVolume = finalMusicClipVolume;
                AudioPlayer.Instance.PlayMusic(finalMusicClip, finalMusicClipVolume);
                break;
            }
        }
    }


    // Level Transitions
    public void SlideLoadLevel(string nextScene)
    {
        StartCoroutine(SlideToNextLevel(nextScene));
    }

    public void FadeLoadLevel(string nextScene)
    {
        StartCoroutine(FadeToNextLevel(nextScene));
    }

    private IEnumerator SlideToNextLevel(string nextScene)
    {
        GameObject transitions = Instantiate(transitionsPrefab);
        GameObject slider = GameObject.Find("Slide");
        Animator animator = slider.GetComponent<Animator>();

        animator.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
        animator.SetTrigger("end");
        yield return new WaitForSeconds(1);
        Destroy(transitions);
        Prefs.SetCurrentLevel(nextScene);
    }

    private IEnumerator FadeToNextLevel(string nextScene)
    {
        GameObject transitions = Instantiate(fadeTransitionPrefab);
        GameObject slider = GameObject.Find("Fade");
        Animator animator = slider.GetComponent<Animator>();

        Player.Instance.hasControl = false;
        animator.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextScene);
        animator.SetTrigger("end");
        yield return new WaitForSeconds(2);
        Player.Instance.hasControl = true;
        Destroy(transitions);
        Prefs.SetCurrentLevel(nextScene);
    }
}
