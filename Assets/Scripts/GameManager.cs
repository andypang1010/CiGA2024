using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGamePaused { get; private set; }
    public int PlayerScore { get; private set; }
    public int PlayerLives { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        IsGamePaused = false;
        PlayerScore = 0;
        PlayerLives = 3;
    }

    public void UpdateScore(int points)
    {
        PlayerScore += points;
        // Update UI or other game elements as needed
    }

    public void UpdateLives(int change)
    {
        PlayerLives += change;
        if (PlayerLives <= 0)
        {
            GameOver();
        }
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Handle game over logic, show game over screen, etc.
        // Optionally, restart the level or show main menu
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }

    public void RestartLevel()
    {
        LevelManager.Instance.RestartLevel();
    }
}
