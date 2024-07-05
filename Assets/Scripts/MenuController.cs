using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GAME"); //
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
