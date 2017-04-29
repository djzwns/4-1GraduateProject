using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_Lobby : MonoBehaviour
{
    public GUIAnimator m_Setting;
    public GUIAnimator m_Music;
    public GUIAnimator m_Skin;

    public GUIAnimator[] m_Map;

    public ScrollSnap m_ScollSnap;
    private int m_lastPage;

    private bool m_SettingOn = false;

    void Start()
    {
        m_lastPage = m_ScollSnap.m_currentPage;
        m_Map[m_lastPage].MoveIn();
    }

    void Update()
    {
        if (m_lastPage != m_ScollSnap.m_currentPage)
        {
            m_Map[m_lastPage].MoveOut();

            m_lastPage = m_ScollSnap.m_currentPage;
            m_Map[m_lastPage].MoveIn();
        }
    }

    public void OnSetting()
    {
        m_SettingOn = !m_SettingOn;
        if (m_SettingOn)
        {
            m_Music.MoveIn();
            m_Skin.MoveIn();
        }
        else
        {
            m_Music.MoveOut();
            m_Skin.MoveOut();
        }

        StartCoroutine(DisableButtonForSeconds(m_Setting.gameObject, 0.5f));
    }


    private IEnumerator DisableButtonForSeconds(GameObject _gObj, float _disableTime)
    {
        GUIAnimSystem.Instance.EnableButton(_gObj.transform, false);

        yield return new WaitForSeconds(_disableTime);

        GUIAnimSystem.Instance.EnableButton(_gObj.transform, true);
    }
}
