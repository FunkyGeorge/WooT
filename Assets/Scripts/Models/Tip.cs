using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Tip : MonoBehaviour
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
    protected bool used = false;
    protected bool isActive = false;
    protected bool canDismiss = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void InitComponents()
    {
        tipBox = GameObject.Find("Tip Box");
        GameObject tipText = GameObject.Find("Tip Text");
        GameObject tipImage = GameObject.Find("Tip Image");
        textComponent = tipText.GetComponent<TMP_Text>();
        imageComponent = tipImage.GetComponent<Image>();
    }

    protected void SetTipBox()
    {
        InitComponents();
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
        StartCoroutine(SetCanDismiss());
    }

    protected void DismissTipBox()
    {
        Time.timeScale = 1;
        Player.Instance.hasControl = true;
        isActive = false;
        textComponent.text = "";
        imageComponent.sprite = null;
        CanvasGroup group = tipBox.GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    private IEnumerator SetCanDismiss()
    {
        yield return new WaitForSecondsRealtime(1f);
        canDismiss = true;
    }

    private Sprite GetPlatformImage()
    {
        return keyboardSprite;
    }

    // Player Input System
    private void OnAnyInput(InputValue value)
    {
        if (isActive && canDismiss)
        {
            DismissTipBox();
        }
    }

    public void OnControlsChanged(PlayerInput input)
    {
        Debug.Log("OnControlsChanged");
    }
}
