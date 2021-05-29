using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class TipTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInput inputScheme;
    [TextArea(3, 5)][SerializeField] private string tipMessage;
    [SerializeField] private bool hasImage;
    [SerializeField] private Sprite keyboardSprite;
    [SerializeField] private Sprite playstationSprite;
    [SerializeField] private Sprite xboxSprite;
    private GameObject tipBox;
    private TMP_Text textComponent;
    private Image imageComponent;
    private bool used = false;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        tipBox = GameObject.Find("Tip Box");
        GameObject tipText = GameObject.Find("Tip Text");
        GameObject tipImage = GameObject.Find("Tip Image");
        textComponent = tipText.GetComponent<TMP_Text>();
        imageComponent = tipImage.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance.gameObject == collision.gameObject && !used && !isActive)
        {
            SetTipBox();
        }
    }

    private void SetTipBox()
    {
        Time.timeScale = 0;
        Player.Instance.hasControl = false;
        used = true;
        isActive = true;
        textComponent.text = tipMessage;
        if (hasImage)
        {
            imageComponent.sprite = GetPlatformImage();
            imageComponent.preserveAspect = true;
        }
        else
        {
            imageComponent.enabled = false;
        }
        CanvasGroup group = tipBox.GetComponent<CanvasGroup>();
        group.alpha = 1;
    }

    private void DismissTipBox()
    {
        Time.timeScale = 1;
        Player.Instance.hasControl = true;
        isActive = false;
        textComponent.text = "";
        imageComponent.sprite = null;
        CanvasGroup group = tipBox.GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    private Sprite GetPlatformImage()
    {
        return keyboardSprite;
    }

    // Player Input System
    private void OnContinue(InputValue value)
    {
        if (isActive)
        {
            DismissTipBox();
        }
    }

    public void OnControlsChanged(PlayerInput input)
    {
        Debug.Log("OnControlsChanged");
    }
}
