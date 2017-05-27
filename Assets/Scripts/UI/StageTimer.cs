using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{
    public Slider m_TimerSlider;
    public Text m_TimerText;

    private Timer m_timer;
    public bool m_active;
    private float m_limitTime;

    public delegate void TimeOutHandler();
    public TimeOutHandler timeout_callback { get; set; }

    void Start()
    {
        m_limitTime = StageInformation.Instance.GetCurrentStageLimitedTime();
        m_timer = new Timer(m_limitTime);
        //timeout_callback = null;
        UpdateTimerText();
    }

    void Update()
    {
        if (m_active)
        {
            m_timer.Update(Time.deltaTime);
            m_TimerSlider.value = m_timer.GetCurrentTime() / m_limitTime;
            UpdateTimerText();
        }

        if (m_timer.IsTimeOut())
        {
            if(this.timeout_callback != null)
                this.timeout_callback();
            SetActive(false);
        }
    }

    private void UpdateTimerText()
    {
        float remainingTime = m_timer.RemainingTime();
        int second = (int)remainingTime;
        int millisecond = Mathf.Max((int)((remainingTime - second) * 100), 0);
        string time_text = second.ToString("00 : ") + millisecond.ToString("00");
        m_TimerText.text = time_text;
    }

    /// <summary>
    /// 타이머 실행
    /// </summary>
    /// <param name="_enable"></param>
    public void SetActive(bool _enable)
    {
        if (!_enable)
        {
            m_timer.Reset();
            m_limitTime = StageInformation.Instance.GetCurrentStageLimitedTime();
            m_timer.SetLimitTime(m_limitTime);
            m_TimerSlider.value = 0;
            UpdateTimerText();
        }
        m_active = _enable;
    }
}
