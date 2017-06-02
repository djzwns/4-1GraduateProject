using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 로비 화면의 먹은 별에 대한 정보를 표시하기 위한 클래스.
/// </summary>
public class LobbyStar : MonoBehaviour
{
    // 스테이지 이름
    public string m_stageName;

    // 별 레이아웃
    public GameObject[] m_StarLayout;

    // 해당 스테이지 정보
    private StageInformation.Stage m_stage;

    // 해당 스테이지 별 갯수
    private int m_starCount;

    void Start()
    {
        m_stage = StageInformation.LoadStageInfo(m_stageName);

        if (m_stage == null) return;

        m_starCount = m_stage.star.Length;

        StarActive();
    }

    /// <summary>
    /// 별을 표시함.
    /// </summary>
    private void StarActive()
    {
        UnityEngine.UI.Image[] m_emptyStar = m_StarLayout[0].GetComponentsInChildren<UnityEngine.UI.Image>(includeInactive: true);
        UnityEngine.UI.Image[] m_filledStar = m_StarLayout[1].GetComponentsInChildren<UnityEngine.UI.Image>(includeInactive: true);
        
        for (int i = 0; i < m_starCount; ++i)
        {
            m_emptyStar[i].gameObject.SetActive(true);
            if (m_stage.star[i].ateThis)
                m_filledStar[i*2].gameObject.SetActive(true);
        }
    }
}
