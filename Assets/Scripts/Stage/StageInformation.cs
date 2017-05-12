/// <summary>
/// 스테이지의 정보를 전달 하기 위한 클래스
/// </summary>

public static class StageInformation
{
    private static string[] m_stageName =
        {
        "Tutorial-1", "Tutorial-2", "Tutorial-3",
        "stage1-1", "stage1-2", "stage1-3",
        "stage2-1", "stage2-2", "stage2-3",
        "stage3-1", "stage3-2", "stage3-3",
        "stage4-1", "stage4-2", "stage4-3",
    };

    public static int m_stageNum;
    public static int m_lastStage;

    /// <summary>
    /// 현재 스테이지.
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentStage()
    {
        return m_stageName[m_stageNum];
    }

    /// <summary>
    /// 다음 스테이지 이름
    /// </summary>
    /// <returns></returns>
    public static string GetNextStage()
    {
        if (m_stageNum + 1 > m_stageName.Length) return "end";

        return m_stageName[m_stageNum + 1];
    }

    /// <summary>
    /// 최종 스테이지 갱신
    /// </summary>
    /// <returns></returns>
    public static bool RenewalStage()
    {
        if (m_lastStage != m_stageNum) return false;
        if (m_lastStage > m_stageNum) return false;

        m_lastStage = ++m_stageNum;
        return true;
    }
}
