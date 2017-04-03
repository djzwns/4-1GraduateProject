using UnityEngine.SceneManagement;
using UnityEngine;

public class OpenScene : MonoBehaviour {

    public void ButtonOpenMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void ButtonOpenLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ButtonOpenGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ButtonGameExit()
    {
        Application.Quit();
    }
}
