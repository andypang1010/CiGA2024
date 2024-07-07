using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

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
    bool hasInstantiatedBodies = false;

    public bool hasCoin1 = false;
    public bool hasCoin2 = false;
    public bool moveBed = false;
    public bool finalDoorOpen = false;

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
        // wrap up actions before we restart the level
        if (moveBed) finalDoorOpen = true;
        // maybe more... 

        Debug.Log("Restarting level " + CurrentLevel);
        SceneManager.LoadScene(CurrentLevel);
        hasInstantiatedBodies = false;
        Invoke("instantiateActiveBodies", 0.05f);
        Invoke("checkMoveBed", 0.05f);
    }

    public void GameComplete()
    {
        Debug.Log("All levels completed!");
        // Handle end of game logic, show final score, etc.

        // darken the current scene gradually in 3 seconds

        // add a fade-in
        // switch to scene "Victory"
        SceneManager.LoadScene("Victory");
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

                foreach (MeshRenderer mr in room.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = true;
                }

                foreach (VisualEffect vfx in room.GetComponentsInChildren<VisualEffect>())
                {
                    vfx.enabled = true;
                }

                foreach (SpriteRenderer sr in room.GetComponentsInChildren<SpriteRenderer>()) {
                    sr.enabled = true;
                }
            }

            else
            {
                foreach (MeshRenderer mr in room.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = false;
                }

                foreach (VisualEffect vfx in room.GetComponentsInChildren<VisualEffect>())
                {
                    vfx.enabled = false;
                }

                foreach (SpriteRenderer sr in room.GetComponentsInChildren<SpriteRenderer>()) {
                    sr.enabled = false;
                }
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

    public void checkMoveBed()
    {
        if (moveBed)
        {
            Debug.Log("Moving bed up");
            GameObject bed = GameObject.Find("Bed");
            bed.GetComponent<ObjectAction>().StartMoveUp();
        }
    }
}
