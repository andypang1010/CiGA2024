using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public void callPause()
    {
        GameManager.Instance.PauseGame();
    }

    public void callResume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void callHowToPlay()
    {
        GameManager.Instance.GoToTemporaryHowToPlay();
    }

    public void callCloseHowToPlay()
    {
        GameManager.Instance.CloseTemporaryHowToPlay();
    }

    public void callGoToMainMenu()
    {
        GameManager.Instance.GoToMenu();
    }
}
