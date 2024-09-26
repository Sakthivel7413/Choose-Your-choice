using DG.Tweening;
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
    public TMP_Text wrongAnswerText;
    public TMP_Text correctAnswerText;
    public TMP_Text finalText;
    public TMP_Text hintText;
    public int correctAnswerCount = 0;
    public int lives = 3;
    public TMP_Text livesText;

    public Button nextButton;
    public Button yesButton;
    public Button noButton;
    public Button backButton;

    public float popDuration = 5f;
    public float visibleDuration = 5f;
    public float hideDuration = 5f;
    public float popScale = 5f;

    private bool hasAnsweredWrong = false;


    public void LoadLevel()
    {
        
        if (currentLevel <= levels.Count && currentLevel > 0)
        {
            nextButton.gameObject.SetActive(true);
            questionText.text = "Test Question for Level " + currentLevel;

            questionText.text = levels[currentLevel - 1].question;
            yesButtonText.text = levels[currentLevel - 1].yesButtonText;
            noButtonText.text = levels[currentLevel - 1].noButtonText;
            logoImage.sprite = levels[currentLevel - 1].logo;
            ShowHintText(levels[currentLevel - 1].hintText);
            ShowFinalText(levels[currentLevel - 1].finalText);


            wrongAnswerText.gameObject.SetActive(false);
            correctAnswerText.gameObject.SetActive(false);
            hasAnsweredWrong = false;
        }
        else
        {
            Debug.LogError("Invalid level index. currentLevel: " + currentLevel);
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

        //ShuffleLevels();
        wrongAnswerText.gameObject.SetActive(false);
        correctAnswerText.gameObject.SetActive(false);
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
        if (isYesClicked)
        {
            noButton.interactable = false;
        }
        else
        {
            yesButton.interactable = false;
        }
        if (levels[currentLevel - 1].isYesAnswerCorrect == isYesClicked)
        {
            Debug.Log("Correct Answer");
            correctAnswerText.text = levels[currentLevel - 1].correctAnswerMessage;
            ShowCorrectAnswerText();
            correctAnswerCount++;
        }
        else
        {
            Debug.Log("Wrong Answer");
            if (!hasAnsweredWrong)
            {
                lives--;
                hasAnsweredWrong = true;
                UpdateLivesUI();

                if (lives <= 0)
                {
                    ShowGameOverPopup();
                }
            }
            wrongAnswerText.text = levels[currentLevel - 1].wrongAnswerMessage;
            ShowWrongAnswerText();
        }
        nextButton.gameObject.SetActive(true);
    }
    //This line of will code make a game over popup message and disable all the images and buttons....
    private void ShowGameOverPopup()
    {

        correctAnswerText.text = "Game Over!";
        correctAnswerText.gameObject.SetActive(true);
        correctAnswerText.transform.localScale = Vector3.zero;
        correctAnswerText.transform.DOScale(popScale, popDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => StartCoroutine(HideAfterDelay(visibleDuration)));


        nextButton.interactable = false;
        yesButton.interactable = false;
        noButton.interactable = false;
        backButton.interactable = true;


        logoImage.gameObject.SetActive(false);
        questionText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        hintText.gameObject.SetActive(false);
        wrongAnswerText.gameObject.SetActive(false);
    }

   
    public void ShowHintText(string message)
    {
        hintText.text = message;

        hintText.transform.localScale = Vector3.one;
        hintText.gameObject.SetActive(true);

        hintText.transform.DOScale(popScale, popDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => StartCoroutine(HideHintTextAfterDelay(visibleDuration)));
    }

    private IEnumerator HideHintTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        hintText.transform.DOScale(0f, hideDuration).OnComplete(() =>
        {
            hintText.gameObject.SetActive(false);
        });
    }
    public void ShowFinalText(string message)
    {
        finalText.text = message;

        finalText.transform.localScale = Vector3.one;
        finalText.gameObject.SetActive(true);

        finalText.transform.DOScale(popScale, popDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => StartCoroutine(HidefinalTextAfterDelay(visibleDuration)));
    }

    private IEnumerator HidefinalTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        finalText.transform.DOScale(0f, hideDuration).OnComplete(() =>
        {
            finalText.gameObject.SetActive(false);
        });
    }
    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        correctAnswerText.gameObject.SetActive(false); 
    }


    private void ShowWrongAnswerText()
    {
        wrongAnswerText.gameObject.SetActive(true);       
        StartCoroutine(HideWrongAnswerTextAfterDelay(10f)); 
    } 
    private IEnumerator HideWrongAnswerTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        wrongAnswerText.gameObject.SetActive(false);
    }
    private void ShowCorrectAnswerText()
    {
        correctAnswerText.gameObject.SetActive(true); 
        StartCoroutine(HideCorrectAnswerTextAfterDelay(10f));
    }

    private IEnumerator HideCorrectAnswerTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        correctAnswerText.gameObject.SetActive(false); 
    }  
}




//It will helps to shuffle the levels Ease, Medium and Hard levels...
//private void ShuffleLevels()
//{

//    LevelData hardLevel = levels[levels.Count - 1]; 
//    List<LevelData> easyMediumLevels = levels.GetRange(0, levels.Count - 1);   
//    for (int i = easyMediumLevels.Count - 1; i > 0; i--)
//    {
//        int j = Random.Range(0, i + 1); 

//        LevelData temp = easyMediumLevels[i];
//        easyMediumLevels[i] = easyMediumLevels[j];
//        easyMediumLevels[j] = temp;
//    }


//    levels.Clear();
//    levels.AddRange(easyMediumLevels);
//    levels.Add(hardLevel); 
//}
