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
    public string GetCurrentStage()
    {
        return m_stage[m_stageNum].name;
    }

    public float GetCurrentStageLimitedTime()
    {
        return m_stage[m_stageNum].limitedTime;
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
    public bool RenewalStage()
    {
        if (m_lastStage != m_stageNum) return false;
        if (m_lastStage > m_stageNum) return false;

        m_lastStage = m_stageNum + 1;
        return true;
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

    public static void Save()
    {
        PlayerPrefs.SetInt("RB_LastStage", m_lastStage);
    }

    public static void Load()
    {
        m_lastStage = PlayerPrefs.GetInt("RB_LastStage", 0);
    }
}
