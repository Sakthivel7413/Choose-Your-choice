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
        UpdateLivesUI(); 
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    public void OnWrongAnswer()
    {
        Debug.Log("OnWrongAnswer called.");  

        if (lives > 0)
        {
            Debug.Log("Wrong answer. Lives before: " + lives); 

            lives--; 

            UpdateLivesUI();  
            Debug.Log("Lives after: " + lives);  

            wrongAnswerMessage.SetActive(true);

            if (lives == 0)
            {
                GameOver();  
            }
        }
    }
    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
        Debug.Log("UI updated. Lives displayed: " + lives);  
    }
    void GameOver()
    {
        Debug.Log("Game Over! You have no lives left.");

    }

    public void AnswerSelected(bool isCorrect)
    {
        if (!isCorrect)
        {
            GameManager.Instance.OnWrongAnswer();  
        }
       
    }
}
