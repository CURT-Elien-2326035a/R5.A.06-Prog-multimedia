using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    public int CurrentScore => score;

    public GameObject winUI; // Assign your win screen UI panel in inspector
    private bool hasWon = false;

    void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreUI();
    }

    public void WinGame()
    {
        if (hasWon) return; // Prevent multiple win triggers

        hasWon = true;

        // Show win UI
        if (winUI != null)
        {
            winUI.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0f;

        Debug.Log("You Win! Final Score: " + score);
    }

    // Optional: Add a restart method that can be called from win UI button
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
