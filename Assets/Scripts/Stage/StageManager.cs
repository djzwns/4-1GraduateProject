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

    void Start()
    {
        m_stageCreator = new StageCreator();
        // 스테이지 생성
        m_Map = m_stageCreator.Create(StageInformation.m_stageName);
    }
}
