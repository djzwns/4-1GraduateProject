using UnityEngine;

/// <summary>
/// 스테이지의 정보를 전달 하기 위한 클래스
/// </summary>

public class StageInformation : Singleton<StageInformation>
{
    public Stage[] m_stage;
    public void SetStage(Stage _stage)
    {
        m_stage[m_stageNum] = _stage;
    }

    public static int m_stageNum;
    public static int m_lastStage;

    /// <summary>
    /// 현재 스테이지.
    /// </summary>
    /// <returns></returns>
    public Stage GetCurrentStage()
    {
        return m_stage[m_stageNum];
    }

    public float GetCurrentStageLimitedTime()
    {
        return m_stage[m_stageNum].limitedTime;
    }

    /// <summary>
    /// 현재 스테이지의 별 갯수
    /// </summary>
    /// <returns></returns>
    public int GetCurrentStageStarCount()
    {
        return m_stage[m_stageNum].star.Length;
    }
    
    /// <summary>
    /// 현재 진행중인 스테이지에 있는 별을 다 먹은지 확인
    /// 이미 다먹은 스테이지라면 true
    /// 다 못먹은 스테이지라면 전부다 먹어야 true를 반환
    /// </summary>
    /// <returns></returns>
    public bool AllStarAte()
    {
        int starCount = m_stage[m_stageNum].star.Length;
        Star[] star = m_stage[m_stageNum].star;

        for (int i = 0; i < starCount; ++i)
        {
            if (!star[i].ateThis) return false;
        }

        return true;
    }

    /// <summary>
    /// 다음 스테이지 이름
    /// </summary>
    /// <returns></returns>
    public string GetNextStage()
    {
        if (m_stageNum + 1 > m_stage.Length) return "end";

        return m_stage[++m_stageNum].name;
    }

    /// <summary>
    /// 최종 스테이지 갱신
    /// </summary>
    /// <returns></returns>
    public void RenewalStage()
    {
        if (m_lastStage > m_stageNum) return;

        m_lastStage = m_stageNum + 1;
        PlayerPrefs.SetInt("RB_LastStage", m_lastStage);
    }

    public static bool CanPlay(int _stageNum)
    {
        if (m_lastStage < _stageNum) return false;

        return true;
    }

    [System.Serializable]
    public class Stage
    {
        public string name;
        public float limitedTime;
        public Star[] star;
    }

    [System.Serializable]
    public class Star
    {
        public Vector3 position;
        [HideInInspector]public bool ateThis;
    }

    /// <summary>
    /// 스테이지 정보 불러오기
    /// </summary>
    /// <param name="_fileName"></param>
    public static Stage LoadStageInfo(string _fileName)
    {
        string json = FileReadWrite.Read(_fileName);

        if (json == null) { return null; }

        Stage stage_data;
        stage_data = JsonUtility.FromJson<Stage>(json);

        return stage_data;
    }

    public static void Load()
    {
        m_lastStage = PlayerPrefs.GetInt("RB_LastStage", 0);
    }

    public void Save()
    {
        SaveStageInfo();
    }

    /// <summary>
    /// 스테이지 정보 저장
    /// </summary>
    /// <param name="_fileName"></param>
    private void SaveStageInfo()
    {
        Stage stage = m_stage[m_stageNum];

        string json_to_string = JsonUtility.ToJson(stage, true);

        FileReadWrite.Write(stage.name, json_to_string);
    }

    public void SetStarAte(int _starNum, bool _ate)
    {
        m_stage[m_stageNum].star[_starNum].ateThis = _ate;
    }

    public void StarReset()
    {
        int starCount = m_stage[m_stageNum].star.Length;
        for (int i = 0; i < starCount; ++i)
        {
            m_stage[m_stageNum].star[i].ateThis = false;
        }
    }
}
