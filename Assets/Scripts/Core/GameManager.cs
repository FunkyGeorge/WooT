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
}
