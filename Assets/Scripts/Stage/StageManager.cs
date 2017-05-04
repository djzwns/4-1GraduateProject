using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 관리. 
/// 스테이지를 생성하고 시작할 수 있도록 해줌.
/// </summary>
public class StageManager : Singleton<StageManager>
{
    private GameObject m_Map;
    private StageCreator m_stageCreator;

    public bool m_IsClear { get; set; }

    void Awake()
    {
        m_stageCreator = gameObject.AddComponent<StageCreator>();
        // 스테이지 생성
        m_Map = m_stageCreator.Create(StageInformation.GetCurrentStage());
    }

    public void NextStage()
    {
        string nextStage = StageInformation.GetNextStage();

        // 마지막 맵이면 아무것도 ..
        if (nextStage.Equals("end")) return;
        m_IsClear = false;

        if (StageInformation.RenewalStage()) Save();

        GameObject destroyObject = m_Map;
        destroyObject.SetActive(false);
        Destroy(destroyObject);

        m_Map = m_stageCreator.Create(nextStage);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("RB_LastStage", StageInformation.m_lastStage);
    }
}
