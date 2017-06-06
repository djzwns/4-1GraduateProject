using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RB_Lobby : MonoBehaviour
{
    static int m_mute = 0;
    public Sprite[] m_mute_image;
    public GUIAnimator m_Setting;
    public GUIAnimator m_PauseWindow;
    //public GUIAnimator m_Music;
    //public GUIAnimator m_Skin;

    public GUIAnimator[] m_Map;

    public ScrollSnap m_ScollSnap;
    private int m_lastPage;

    private bool m_SettingOn = false;

    void Start()
    {
        m_lastPage = m_ScollSnap.m_currentPage;
        StageInformation.Load();

        InteractableMapButton(StageInformation.m_lastStage);

        MapUIDisable();
        m_Map[m_lastPage].gameObject.SetActive(true);
        m_Map[m_lastPage].MoveIn();
        BGMManager.Instance.BGMChange(-1);

        //GUIAnimSystem.Instance.ButtonAddEvents(m_Music.transform, BGMManager.Instance.ToggleMute);
        //GUIAnimSystem.Instance.ButtonAddEvents(m_Music.transform, ToggleMute);
        //MuteImageChange(m_Music.GetComponent<Image>());

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
            //m_Music.MoveIn();
            //m_Skin.MoveIn();
        }
        else
        {
            m_PauseWindow.MoveOut();
            //m_Music.MoveOut();
            //m_Skin.MoveOut();
        }

        StartCoroutine(DisableButtonForSeconds(m_Setting.gameObject, 0.5f));
    }

    /// <summary>
    /// 음악 온/오프 이미지 토글
    /// </summary>
    public void ToggleMute()
    {
        //Image btn = m_Music.GetComponent<Image>();
        //m_mute = ++m_mute % 2;
        //MuteImageChange(btn);
    }

    private void MuteImageChange(Image _btn)
    {
        _btn.sprite = m_mute_image[m_mute];
    }


    private IEnumerator DisableButtonForSeconds(GameObject _gObj, float _disableTime)
    {
        GUIAnimSystem.Instance.EnableButton(_gObj.transform, false);

        yield return new WaitForSeconds(_disableTime);

        GUIAnimSystem.Instance.EnableButton(_gObj.transform, true);
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
