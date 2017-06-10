using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RB_Lobby : MonoBehaviour
{
    public GUIAnimator m_Setting;
    public GUIAnimator m_Exit;
    public GUIAnimator m_PauseWindow;
    public GUIAnimator m_ExitWindow;
    private bool m_SettingOn = false;
    private bool m_ExitOn = false;
    private GameObject m_ButtonGuard;

    public GUIAnimator[] m_Map;

    public ScrollSnap m_ScollSnap;
    private int m_lastPage;


    void Start()
    {
        PlayerPrefs.SetInt("RB_LastStage", 15);
        m_ButtonGuard = GameObject.Find("ButtonGuard (Panel)");
        EnableButtonGuard(false);

        m_lastPage = m_ScollSnap.m_currentPage;
        StageInformation.Load();

        InteractableMapButton(StageInformation.m_lastStage);

        MapUIDisable();
        m_Map[m_lastPage].gameObject.SetActive(true);
        m_Map[m_lastPage].MoveIn();
    }

    void Update()
    {
        if (m_lastPage != m_ScollSnap.m_currentPage)
        {
            MapUIActive();
        }
    }

    private void MapUIDisable()
    {
        for (int i = 0; i < m_Map.Length; ++i)
        {
            m_Map[i].gameObject.SetActive(false);
        }
    }

    private void MapUIActive()
    {
        m_Map[m_lastPage].MoveOut();
        m_Map[m_lastPage].gameObject.SetActive(false);

        m_lastPage = m_ScollSnap.m_currentPage;
        m_Map[m_lastPage].gameObject.SetActive(true);
        m_Map[m_lastPage].MoveIn();
    }

    /// <summary>
    /// 설정 버튼 온/오프
    /// </summary>
    public void OnSetting()
    {
        m_SettingOn = !m_SettingOn;
        if (m_SettingOn)
        {
            m_PauseWindow.MoveIn();
            EnableButtonGuard(true);
            DisableButton(m_Exit.gameObject, false);
        }
        else
        {
            m_PauseWindow.MoveOut();
            EnableButtonGuard(false);
            DisableButton(m_Exit.gameObject, true);
        }

        StartCoroutine(DisableButtonForSeconds(m_Setting.gameObject, 0.5f));
    }

    /// <summary>
    /// 나가기 버튼 온/오프
    /// </summary>
    public void OnExitWindow()
    {
        m_ExitOn = !m_ExitOn;
        if (m_ExitOn)
        {
            m_ExitWindow.MoveIn();
            EnableButtonGuard(true);
            DisableButton(m_Setting.gameObject, false);
        }
        else
        {
            m_ExitWindow.MoveOut();
            EnableButtonGuard(false);
            DisableButton(m_Setting.gameObject, true);
        }

        StartCoroutine(DisableButtonForSeconds(m_Exit.gameObject, 0.5f));
    }

    private void EnableButtonGuard(bool _enable)
    {
        m_ButtonGuard.SetActive(_enable);
    }

    private void DisableButton(GameObject _button, bool _enable)
    {
        GUIAnimSystem.Instance.EnableButton(_button.transform, _enable);
        Image image = _button.GetComponent<Image>();
        if (_enable) image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        else image.color = new Color(image.color.r, image.color.g, image.color.b, 0.42f);
    }

    private IEnumerator DisableButtonForSeconds(GameObject _button, float _disableTime)
    {
        GUIAnimSystem.Instance.EnableButton(_button.transform, false);

        yield return new WaitForSeconds(_disableTime);

        GUIAnimSystem.Instance.EnableButton(_button.transform, true);
    }

    /// <summary>
    /// 클리어 완료 된 맵까지만 버튼 활성화
    /// </summary>
    /// <param name="_lastStage"></param>
    private void InteractableMapButton(int _lastStage)
    {
        GameObject mapPanel = GameObject.Find("Panel (Bottom Center)");
        Button[] btn = mapPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < btn.Length; ++i)
        {
            if (i <= _lastStage)
                GUIAnimSystem.Instance.InteractableButton(btn[i].transform, true);
            else
                GUIAnimSystem.Instance.InteractableButton(btn[i].transform, false);
        }
    }
}
