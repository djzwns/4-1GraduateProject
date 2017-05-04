using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RB_Game : MonoBehaviour {

    bool m_BoxOpen;
    bool m_PauseBoxOpen;
    public GUIAnimator m_ObjectBox;
    public GUIAnimator m_OpenButton;

    public GUIAnimator m_PauseBox;
    public GUIAnimator m_PauseButton;
    public GameObject[] m_Next_ResetButton;
    public Text m_PauseBoxLabel;

    void Awake()
    {
        m_BoxOpen = false;
        m_PauseBoxOpen = false;

        BGMManager.Instance.BGMChange(-1);
    }

    // 오브젝트 박스 토글
    public void ToggleObjectBox()
    {
        m_BoxOpen = !m_BoxOpen;
        if (m_BoxOpen == true)
        {
            m_OpenButton.MoveIn();
            m_ObjectBox.MoveIn();
        }

        else
        {
            m_OpenButton.MoveOut();
            m_ObjectBox.MoveOut();
        }

        StartCoroutine(DisableButtonForSeconds(m_OpenButton.gameObject, 0.5f));
    }

    // 일시정지 박스 토글
    public void TogglePauseBox()
    {
        m_PauseBoxOpen = !m_PauseBoxOpen;

        ToggleNextReset();
        if (m_PauseBoxOpen == true)
        {
            m_PauseBox.MoveIn();
            if (GameManager.Instance.m_IsPlaying) GameManager.Instance.StopGame();
        }
        else
        {
            if (GameManager.Instance.m_IsPlaying) GameManager.Instance.PlayGame();
            m_PauseBox.MoveOut();
        }

        StartCoroutine(DisableButtonForSeconds(m_PauseButton.gameObject, 0.5f));
    }

    private void ToggleNextReset()
    {
        if (StageManager.Instance.m_IsClear)
        {
            m_Next_ResetButton[0].SetActive(false);
            m_Next_ResetButton[1].SetActive(true);
            m_PauseBoxLabel.text = "CLEAR !!";
        }
        else
        {
            m_Next_ResetButton[1].SetActive(false);
            m_Next_ResetButton[0].SetActive(true);
            m_PauseBoxLabel.text = StageInformation.GetCurrentStage();
        }
    }

    // 리셋
    public void Reset()
    {
        m_PauseBoxOpen = false;

        m_PauseBox.MoveOut();
        GameManager.Instance.ResetGame();
    }

    // 다음맵
    public void NextGame()
    {
        StageManager.Instance.NextStage();
        Reset();
    }

    // UI 숨김
    public void HideAllGUIs()
    {
        m_PauseBox.MoveOut();
        m_ObjectBox.MoveOut();
    }

    private IEnumerator DisableButtonForSeconds(GameObject _gObj, float _disableTime)
    {
        GUIAnimSystem.Instance.EnableButton(_gObj.transform, false);

        yield return new WaitForSeconds(_disableTime);

        GUIAnimSystem.Instance.EnableButton(_gObj.transform, true);
    }
}
