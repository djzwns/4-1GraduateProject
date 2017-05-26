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

        gameObject.SendMessage("HideAllGUIs");
    }

    public void ButtonOpenGame(int _stageNum)
    {// 이거 수정해야함 땜빵코드 ㅂ ㄷㅂㄷ ㅂㄷ _stageNum != 0 마음에 안들음
        if (_stageNum != 0 && StageInformation.m_lastStage <= _stageNum) return;

        StageInformation.m_stageNum = _stageNum;

        // 스테이지 따라 브금 변경..
        BGMManager.Instance.BGMChange(-1);

        SceneManager.LoadScene("Game");
    }

    public void ButtonGameExit()
    {
        Application.Quit();
    }
}
