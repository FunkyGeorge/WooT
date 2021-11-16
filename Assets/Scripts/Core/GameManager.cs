﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("State Flags")]
    public bool isDialogUp = false;

    [Header("Music Config")]
    [SerializeField] private AudioClip musicClip1;
    [Range(1, 100)][SerializeField] private int musicClip1Volume = 100;
    [SerializeField] private string[] music1Levels;
    [SerializeField] private AudioClip musicClip2;
    [Range(1, 100)][SerializeField] private int musicClip2Volume = 100;
    [SerializeField] private string[] music2Levels;

    private const string coinTextName = "UI_CoinText";
    private const string inventorySpriteName = "UI_InventoryImage";

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

    private void PlayMusic()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        for (int i = 0; i < music1Levels.Length; i++)
        {
            if (music1Levels[i] == currentLevel)
            {
                AudioPlayer.Instance.PlayMusic(musicClip1, musicClip1Volume);
                break;
            }
        }

        for (int i = 0; i < music2Levels.Length; i++)
        {
            if (music2Levels[i] == currentLevel)
            {
                AudioPlayer.Instance.PlayMusic(musicClip2, musicClip2Volume);
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
