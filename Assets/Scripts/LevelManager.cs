using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int CurrentLevel { get; private set; }
    public int TotalLevels { get; private set; }

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
        CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        TotalLevels = SceneManager.sceneCountInBuildSettings;
    }

    private void Update()
    {
    }

    public void LoadNextLevel()
    {
        if (CurrentLevel < TotalLevels - 1)
        {
            CurrentLevel++;
            SceneManager.LoadScene(CurrentLevel);
        }
        else
        {
            EndCurrentLevel();
        }
    }

    public void RestartCurrentLevel()
    {
        Debug.Log("Restarting level " + CurrentLevel);
        SceneManager.LoadScene(CurrentLevel);
    }

    public void EndCurrentLevel()
    {
        Debug.Log("All levels completed!");
        // Handle end of game logic, show final score, etc.
    }
}
