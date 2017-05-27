using UnityEngine;

public class Timer
{
    private float m_elapsedTime;
    private float m_limitTime;

    public Timer(float _limitTime)
    {
        m_elapsedTime = 0f;
        m_limitTime = _limitTime;
    }

    /// <summary>
    /// 시간 누적
    /// </summary>
    /// <param name="_deltaTime"></param>
    public void Update(float _deltaTime)
    {
        m_elapsedTime = Mathf.Min(m_elapsedTime + _deltaTime, m_limitTime);
    }

    /// <summary>
    /// 타임아웃
    /// </summary>
    /// <returns></returns>
    public bool IsTimeOut()
    {
        return m_elapsedTime >= m_limitTime;
    }

    /// <summary>
    /// 시간 초기화
    /// </summary>
    public void Reset()
    {
        m_elapsedTime = 0f;
    }

    /// <summary>
    /// 현재 시간
    /// </summary>
    /// <returns></returns>
    public float GetCurrentTime()
    {
        return m_elapsedTime;
    }

    /// <summary>
    /// 남은 시간
    /// </summary>
    /// <returns></returns>
    public float RemainingTime()
    {
        return m_limitTime - m_elapsedTime;
    }

    /// <summary>
    /// 제한 시간 재설정
    /// </summary>
    /// <param name="_time"></param>
    public void SetLimitTime(float _time)
    {
        m_limitTime = _time;
    }
}
