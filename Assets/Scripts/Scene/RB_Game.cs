using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RB_Game : MonoBehaviour {

    bool m_ObjectBoxOpen;
    bool m_PauseBoxOpen;
    public GUIAnimator m_ObjectBox;
    public GUIAnimator m_OpenButton;

    public GUIAnimator m_PauseBox;
    public GUIAnimator m_PauseButton;
    public GameObject[] m_Next_ResetButton;
    public Image m_Window;
    public Sprite[] m_Windows;
    public GameObject m_SoundController;
    //public Text m_PauseBoxLabel;

    public GUIAnimator m_WasteBasket;
    public GUIAnimator[] m_Star;
    private int m_starCount = 0;

    void Awake()
    {
        m_ObjectBoxOpen = false;
        m_PauseBoxOpen = false;
        BGMManager.Instance.BGMChange(-1);
    }

    // 오브젝트 박스 토글
    public void ToggleObjectBox()
    {
        if (GameManager.Instance.m_IsPlaying) return;

        m_ObjectBoxOpen = !m_ObjectBoxOpen;
        if (m_ObjectBoxOpen == true)
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
        }
        else
        {
            m_PauseBox.MoveOut();
        }
        GameManager.Instance.PauseGame();
        if (!StageManager.Instance.m_IsClear)
            StartCoroutine(DisableButtonForSeconds(m_PauseButton.gameObject, 0.5f));
        else
            EnableButton(m_PauseButton.gameObject, false);
    }

    private void ToggleNextReset()
    {
        if (StageManager.Instance.m_IsClear)
        {
            m_Next_ResetButton[0].SetActive(false);
            m_Next_ResetButton[1].SetActive(true);
            //m_PauseBoxLabel.text = "CLEAR !!";
            m_Window.sprite = m_Windows[1];
            m_SoundController.SetActive(false);
        }
        else
        {
            m_Next_ResetButton[1].SetActive(false);
            m_Next_ResetButton[0].SetActive(true);
            m_Window.sprite = m_Windows[0];
            m_SoundController.SetActive(true);
            //m_PauseBoxLabel.text = StageInformation.GetCurrentStage();
        }
    }

    // 리셋
    public void Reset()
    {
        m_PauseBoxOpen = false;

        m_PauseBox.MoveOut();
        GameManager.Instance.ResetGame();
        GameManager.Instance.ObjectsDestroy();
        DefaultStar();
    }

    // 다음맵
    public void NextGame()
    {
        if (!StageManager.Instance.NextStage()) return;
        Reset();
        EnableButton(m_PauseButton.gameObject, true);
    }

    // UI 숨김
    public void HideAllGUIs()
    {
        m_ObjectBoxOpen = false;
        m_PauseBoxOpen = false;
        m_PauseBox.MoveOut();
        m_OpenButton.MoveOut();
        m_ObjectBox.MoveOut();
    }

    private IEnumerator DisableButtonForSeconds(GameObject _gObj, float _disableTime)
    {
        GUIAnimSystem.Instance.EnableButton(_gObj.transform, false);

        yield return new WaitForSeconds(_disableTime);

        GUIAnimSystem.Instance.EnableButton(_gObj.transform, true);
    }

    private void EnableButton(GameObject _gObj, bool _enable)
    {
        GUIAnimSystem.Instance.EnableButton(_gObj.transform, _enable);
    }

    public void BasketOn()
    {
        m_WasteBasket.MoveIn();
    }

    public void BasketOff()
    {
        m_WasteBasket.MoveOut();
    }

    /// <summary>
    /// 좌측 상단 별 추가
    /// </summary>
    public void AddStar()
    {
        if (m_starCount > 2) return;
        m_Star[m_starCount++].MoveIn();
    }

    public void DefaultStar()
    {
        for (int i = m_starCount - 1; i >= 0; --i)
        {
            m_Star[i].MoveOut();
        }
        m_starCount = 0;
    }
}
