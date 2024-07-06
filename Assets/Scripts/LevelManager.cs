using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Cinemachine;
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
    public List<GameObject> rooms = new List<GameObject>();
    public CinemachineVirtualCamera virtualCamera;
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
            SceneManager.sceneLoaded += OnSceneLoad;
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
        Invoke("instantiateActiveBodies", 0.05f);
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
                    body.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Death4"); ;
                    break;
                case 1:
                    body.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Death3");
                    break;
                case 2:
                    body.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Death2");
                    break;
                case 3:
                    body.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Death1");
                    break;
                default:
                    body.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    break;
            }

            index++;
        }

        hasInstantiatedBodies = true;
    }

    public void ShowActiveRoom(GameObject currentRoom)
    {
        foreach (GameObject room in rooms)
        {
            GameObject roomFloor = room.transform.GetChild(0).gameObject;

            if (currentRoom == room)
            {
                virtualCamera.Follow = roomFloor.transform;

                for (int i = 1; i < room.transform.childCount; i++)
                {
                    room.transform.GetChild(i).gameObject.SetActive(true);
                    // print(room.transform.GetChild(i).gameObject);
                }
                roomFloor.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
            else
            {
                for (int i = 1; i < room.transform.childCount; i++)
                {
                    room.transform.GetChild(i).gameObject.SetActive(false);
                    // print(room.transform.GetChild(i).gameObject);
                }
                roomFloor.GetComponentInChildren<MeshRenderer>().enabled = false;
            }

            roomFloor.SetActive(true);
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameObject environment = GameObject.Find("ENVIRONMENT");

        rooms.Clear();

        for (int i = 0; i < environment.transform.childCount; i++)
        {
            rooms.Add(environment.transform.GetChild(i).gameObject);
        }

        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
    }
}
