using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGamePaused { get; private set; }
    public int PlayerScore { get; private set; }
    public int PlayerLives { get; private set; }
    public enum GameState { MainMenu, Playing, Paused, GameOver, HowToPlay }
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

    private void Update()
    {
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
        Debug.Log("Game Paused!");
        GameObject PauseScene = GameObject.Find("PauseScene");
        for (int i = 0; i < PauseScene.transform.childCount; i++)
        {
            PauseScene.transform.GetChild(i).gameObject.SetActive(true);
        }
        IsGamePaused = true;
        Time.timeScale = 0f;
        CurrentState = GameState.Paused;
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game!");
        GameObject PauseScene = GameObject.Find("PauseScene");
        for (int i = 0; i < PauseScene.transform.childCount; i++)
        {
            PauseScene.transform.GetChild(i).gameObject.SetActive(false);
        }
        IsGamePaused = false;
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
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

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void GoToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void GoToTemporaryHowToPlay()
    {
        GameObject temp = GameObject.Find("HowToPlayScene");
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            temp.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void CloseTemporaryHowToPlay()
    {
        GameObject temp = GameObject.Find("HowToPlayScene");
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            temp.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
