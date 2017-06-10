using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 목적지. 공이 범위 내로 들어왔을 때 실행됨
/// </summary>
public class DestinationFlag : MonoBehaviour
{
    public GUIStyle m_style;
    public float m_limitTime = 3f;
    public GameObject m_effect;

    private Timer m_timer;
    private bool m_timerOn;

    private Ball m_ball;

    void Awake()
    {
        m_timer = new Timer(m_limitTime);
        m_timerOn = false;
        m_effect.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.m_IsPause) return;
        if (m_timerOn)
        {
            m_timer.Update(Time.deltaTime);
            m_ball.CountDown(m_timer.GetCurrentTime());
        }

        if (!m_timer.IsTimeOut()) return;

        if (!StageManager.Instance.m_IsClear)
        {
            StageManager.Instance.m_IsClear = true;
            GameManager.Instance.StopGame();
            StageManager.Instance.Save();
            // 다음 스테이지 갈지말지..?
            GameObject.FindObjectOfType<RB_Game>().TogglePauseBox();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;
        m_timerOn = true;
        m_effect.SetActive(true);
        m_ball = coll.GetComponent<Ball>();
        m_ball.TimerOn();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;
        m_timerOn = false;
        m_timer.Reset();
        m_effect.SetActive(false);
        m_ball.TimerOff();
        m_ball = null;
    }
}
