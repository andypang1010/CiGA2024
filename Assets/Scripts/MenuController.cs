using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CG"); //
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
