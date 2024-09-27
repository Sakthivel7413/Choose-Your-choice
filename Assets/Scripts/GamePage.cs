using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePage : MonoBehaviour, IObserver
{
    [SerializeField] private Button YesButton, NoButton;
    [SerializeField] private TMP_Text questionText;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.RegisterObserver(this); 
        }
    }

    private void OnYesButtonClicked()
    {
        levelManager.CheckAnswer(true);
    }

    private void OnNoButtonClicked()
    {
        levelManager.CheckAnswer(false);
    }

    private void OnDestroy()
    {
        if (levelManager != null)
        {
            levelManager.UnregisterObserver(this); 
        }
    }

    public void OnNotify(string message)
    {
        Debug.Log(message);
        questionText.text = message;  
    }
}
