using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int CurrentLevel { get; private set; }
    public int TotalLevels { get; private set; }
    public ArrayList allDeaths = new ArrayList();
    public Queue<Vector3> activeDeaths = new Queue<Vector3>();
    public GameObject[] deadSprites;
    Boolean hasInstantiatedBodies = false;

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

        // fill queue with dummy objects
        for (int i = 0; i < 4; i++)
        {
            activeDeaths.Enqueue(new Vector3(1000, 1000, 1000));
        }
    }

    private void Update()
    {
    }

    public void LoadNextLevel()
    {
        if (CurrentLevel <= TotalLevels - 1)
        {
            CurrentLevel++;
            SceneManager.LoadScene(CurrentLevel);
        }
        else
        {
            GameComplete();
        }
    }

    public void RestartCurrentLevel()
    {
        Debug.Log("Restarting level " + CurrentLevel);
        SceneManager.LoadScene(CurrentLevel);
        hasInstantiatedBodies = false;
    }

    public void GameComplete()
    {
        Debug.Log("All levels completed!");
        // Handle end of game logic, show final score, etc.
    }

    public void updatePlayerDeath(Vector3 newDeathPos)
    {
        if (activeDeaths.Count >= 4)
        {
            activeDeaths.Dequeue();
            activeDeaths.Enqueue(newDeathPos);
        }
        else
        {
            activeDeaths.Enqueue(newDeathPos);
        }
        allDeaths.Add(newDeathPos);
    }

    public void instantiateActiveBodies()
    {
        if (hasInstantiatedBodies)
        {
            return;
        }
        int index = 0;
        foreach (Vector3 deathPos in activeDeaths)
        {
            // Instantiate a body at the death position
            GameObject bodyPrefab = deadSprites[index];
            GameObject body = Instantiate(bodyPrefab, deathPos, Quaternion.identity);
            switch (index)
            {
                case 0:
                    body.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    break;
                case 1:
                    body.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    break;
                case 2:
                    body.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                    break;
                case 3:
                    body.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                    break;
                default:
                    body.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    break;
            }

            index++;
        }

        hasInstantiatedBodies = true;
    }
}
