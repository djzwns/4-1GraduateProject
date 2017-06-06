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
        GUIAnimSystem.Instance.LoadLevel("Lobby", 0.3f);
        BGMManager.Instance.BGMChange(0);

        gameObject.SendMessage("HideAllGUIs");
    }

    public void ButtonOpenGame(int _stageNum)
    {
        if (!StageInformation.CanPlay(_stageNum)) return;

        StageInformation.m_stageNum = _stageNum;

        // 스테이지 따라 브금 변경..
        BGMManager.Instance.BGMChange((_stageNum / 3) + 1);

        SceneManager.LoadScene("Game");
    }

    public void ButtonGameExit()
    {
        Application.Quit();
    }
}
