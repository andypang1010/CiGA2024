using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    public AudioSource mainMenuMusic;
    public AudioSource playingMusic;
    public AudioSource pausedMusic;
    public AudioSource gameOverMusic;

    private void Awake()
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

    private void Start()
    {
        Debug.Log("AudioController Start");
        UpdateAudio(GameManager.Instance.CurrentState);
    }

    public void UpdateAudio(GameManager.GameState gameState)
    {
        // StopAllMusic();

        // switch (gameState)
        // {
        //     case GameManager.GameState.MainMenu:
        //         mainMenuMusic.Play();
        //         break;
        //     case GameManager.GameState.Playing:
        //         playingMusic.Play();
        //         break;
        //     case GameManager.GameState.Paused:
        //         pausedMusic.Play();
        //         break;
        //     case GameManager.GameState.GameOver:
        //         gameOverMusic.Play();
        //         break;
        // }
    }

    private void StopAllMusic()
    {
        mainMenuMusic.Stop();
        playingMusic.Stop();
        pausedMusic.Stop();
        gameOverMusic.Stop();
    }
}
