using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<LevelData> levels;
    public int currentLevel = 1;

    public TMP_Text questionText, yesButtonText, noButtonText;
    public Image logoImage;
    public TMP_Text livesText;

    public Button nextButton;
    public Button yesButton;
    public Button noButton;
    public Button backButton;

    private List<IObserver> observers = new List<IObserver>();

    public float popDuration = 5f;
    public float visibleDuration = 5f;
    public float hideDuration = 5f;
    public float popScale = 5f;

    public int lives = 3;

    public void LoadLevel()
    {
        if (currentLevel <= levels.Count && currentLevel > 0)
        {
            
            nextButton.gameObject.SetActive(true);
            LevelData currentLevelData = levels[currentLevel - 1];

            questionText.text = currentLevelData.question;
            yesButtonText.text = currentLevelData.yesButtonText;
            noButtonText.text = currentLevelData.noButtonText;
            logoImage.sprite = currentLevelData.logo;

            NotifyObservers("Level Loaded: " + currentLevelData.question);
        }
        else
        {
            Debug.LogError("Invalid level index.");
        }
    }

    public void OnNextButtonClicked()
    {
        Debug.Log("Next button clicked. Current level: " + currentLevel);

        if (currentLevel < levels.Count)
        {
            currentLevel++;
            LoadLevel();
            nextButton.gameObject.SetActive(false);
            yesButton.interactable = true;
            noButton.interactable = true;
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    private void Start()
    {
        if (levels == null || levels.Count == 0)
        {
            return;
        }

        
        nextButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        LoadLevel();
        UpdateLivesUI();
    }

    public void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    public void CheckAnswer(bool isYesClicked)
    {
        LevelData currentLevelData = levels[currentLevel - 1];

        yesButton.interactable = false;
        noButton.interactable = false;
        if (currentLevelData.isYesAnswerCorrect == isYesClicked)
        {
            NotifyObservers("Correct Answer:" +currentLevelData.correctAnswerMessage);
        }
        else
        {
            if (lives > 0)
            {
                lives--;
                UpdateLivesUI();

                NotifyObservers("Wrong Answer:" + currentLevelData.wrongAnswerMessage);

                if (lives <= 0)
                {
                    ShowGameOverPopup();
                }
            }
        }

        nextButton.gameObject.SetActive(true);
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers(string message)
    {
        foreach (IObserver observer in observers)
        {
            observer.OnNotify(message);
        }
    }

    private void ShowGameOverPopup()
    {
        Debug.Log("Game Over!");

        nextButton.interactable = false;
        yesButton.interactable = false;
        noButton.interactable = false;
        backButton.interactable = true;

        logoImage.gameObject.SetActive(false);
        questionText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
    }
}
