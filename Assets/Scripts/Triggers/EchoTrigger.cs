using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EchoTrigger : MonoBehaviour
{
    [SerializeField] private GameObject echoPopup;
    [SerializeField] private TMP_Text statementText;
    [SerializeField] private TMP_Text authorText;
    [SerializeField] private int echoID;
    [SerializeField] private EchoScriptableObject echoList;

    // Start is called before the first frame update
    void Start()
    {
        EchoSave echoSave = Array.Find(echoList.echosaves, ele => ele.index == echoID);
        if (echoSave == null)
        {
            Debug.Log("echo does not exist");
        }
        statementText.text = echoSave.statement;
        authorText.text = echoSave.author;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            echoPopup.SetActive(true);
            Prefs.SaveEcho(echoID);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            echoPopup.SetActive(false);
        }
    }
}
