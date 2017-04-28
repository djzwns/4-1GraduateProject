using UnityEngine.SceneManagement;
using UnityEngine;

public class OpenScene : MonoBehaviour {

    public void ButtonOpenMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void ButtonOpenLobby()
    {
        GUIAnimSystem.Instance.EnableAllButton(false);
        GUIAnimSystem.Instance.LoadLevel("Lobby", 1.5f);

        gameObject.SendMessage("HideAllGUIs");
    }

    public void ButtonOpenGame(string _stageName)
    {
        StageInformation.m_stageName = _stageName;

        SceneManager.LoadScene("Game");
    }

    public void ButtonGameExit()
    {
        Application.Quit();
    }
}
