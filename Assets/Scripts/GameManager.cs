using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGamePaused { get; private set; }
    public int PlayerScore { get; private set; }
    public int PlayerLives { get; private set; }
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState CurrentState;

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
        Debug.Log("GameManager Start");
        IsGamePaused = false;
        PlayerScore = 0;
        PlayerLives = 3;
    }

    // This method is used to test the level loading functionality 
    // and the scene orders.
    // private IEnumerator TestLevels()
    // {
    //     yield return new WaitForSeconds(2f);
    //     LoadNextLevel();
    //     yield return new WaitForSeconds(2f);
    //     LoadNextLevel();
    // }

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
        LevelManager.Instance.GameComplete();
        // other logic for ending the game
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }

    public void RestartLevel()
    {
        LevelManager.Instance.RestartCurrentLevel();
    }
}
