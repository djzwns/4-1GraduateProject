﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RB_Lobby : MonoBehaviour
{
    static int m_mute = 0;
    public Sprite[] m_mute_image;
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
        StageInformation.m_lastStage = PlayerPrefs.GetInt("RB_LastStage", 0);
        BGMManager.Instance.BGMChange(-1);

        GUIAnimSystem.Instance.ButtonAddEvents(m_Music.transform, BGMManager.Instance.ToggleMute);
        GUIAnimSystem.Instance.ButtonAddEvents(m_Music.transform, ToggleMute);
        MuteImageChange(m_Music.GetComponent<Image>());
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

    public void ToggleMute()
    {
        Image btn = m_Music.GetComponent<Image>();
        m_mute = ++m_mute % 2;
        MuteImageChange(btn);
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
}
