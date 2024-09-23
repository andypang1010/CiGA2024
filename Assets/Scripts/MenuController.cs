using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CG");
        GameManager.Instance.CurrentState = GameManager.GameState.Playing;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        GameManager.Instance.CurrentState = GameManager.GameState.MainMenu;
    }

    public void GoToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        GameManager.Instance.CurrentState = GameManager.GameState.HowToPlay;
    }
}
