using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives = 3;  
    public TextMeshProUGUI livesText;
    public GameObject wrongAnswerMessage;  

    void Start()
    {
        UpdateLivesUI();  // Show initial lives
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Keep GameManager between scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicate GameManagers
        }
    }

    // This function will be called when the player chooses a wrong answer
    public void OnWrongAnswer()
    {
        Debug.Log("OnWrongAnswer called.");  // Check if the function is being called

        if (lives > 0)
        {
            Debug.Log("Wrong answer. Lives before: " + lives);  // Log current lives

            lives--;  // Decrease life by 1

            UpdateLivesUI();  // Update UI with new lives count

            Debug.Log("Lives after: " + lives);  // Log updated lives

            wrongAnswerMessage.SetActive(true);

            if (lives == 0)
            {
                GameOver();  // Call GameOver when lives run out
            }
        }
    }

    // Update the lives text in the UI
    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;  // Update the UI text
        Debug.Log("UI updated. Lives displayed: " + lives);  // Log the lives count to the console
    }

    // Handle game over state
    void GameOver()
    {
        Debug.Log("Game Over! You have no lives left.");
        // Show a game over screen or restart the game
    }

    public void AnswerSelected(bool isCorrect)
    {
        if (!isCorrect)
        {
            GameManager.Instance.OnWrongAnswer();  // Call wrong answer logic
        }
        else
        {
            // Call logic for correct answer
        }
    }
}