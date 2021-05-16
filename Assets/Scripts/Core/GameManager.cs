using System.Collections;
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
    }

    public void UpdateUI()
    {
        if (Player.Instance != null)
        {
            if (coinsText) coinsText.text = Player.Instance.shardsCollected.ToString();
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
    }
}
