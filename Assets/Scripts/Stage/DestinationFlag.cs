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
    private string m_str;
    private bool m_timerOn;
    
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
            m_str = m_timer.RemainingTime().ToString("0.00");
        }

        if (!m_timer.IsTimeOut()) return;

        if (!StageManager.Instance.m_IsClear)
        {
            // 클리어 !
            m_str = "CLEAR!!";
            // 클리어 부분 추가...
            StageManager.Instance.m_IsClear = true;
            StageManager.Instance.Save();
            GameManager.Instance.StopGame();
            // 다음 스테이지 갈지말지..?
            GameObject.FindObjectOfType<RB_Game>().TogglePauseBox();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;
        m_timerOn = true;
        m_effect.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;
        m_timerOn = false;
        m_timer.Reset();
        m_str = "";
        m_effect.SetActive(false);
    }

    void OnGUI()
    {
        // 임시... 카운트 표시
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.BeginGroup(new Rect(pos, new Vector2(200, 50)), m_style);

        GUI.Label(new Rect(0, 0, 200, 40), m_str, m_style);

        GUI.EndGroup();
    }
}
