using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeedbackHandler : MonoBehaviour
{
    [SerializeField] private ConfigScriptableObject config;
    [SerializeField] private AudioClip selectSFX;
    [Range(1, 100)] [SerializeField] private int selectAudioVolume = 100;
    [SerializeField] private GameObject defaultMenuItem;

    public void OpenFeedbackForm()
    {
        OpenURLForm(config.feedbackURL);
    }

    public void OpenBugForm()
    {
        OpenURLForm(config.bugReportURL);
    }

    public void Init()
    {
        EventSystem.current.SetSelectedGameObject(defaultMenuItem);
    }

    public void BackButtonHandler()
    {
        GameManager.Instance.CloseFeedback();
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

    private void OpenURLForm(string url)
    {
        Application.OpenURL(url);
    }
}
