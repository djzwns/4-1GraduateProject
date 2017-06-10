using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D m_Ball;

    bool m_timerActive = false;
    public UnityEngine.UI.Slider m_Timer;

    void Start()
    {
        m_Ball = gameObject.GetComponent<Rigidbody2D>();
        GameObject.FindObjectOfType<StartFlag>().ResetPosition(m_Ball.transform);
        Stop();
        TimerOff();
    }

    void FixedUpdate()
    {
        if (!m_timerActive) return;
        Vector3 pos = CameraManager.Instance.WorldToScreenPosition(transform.position);
        m_Timer.transform.position = pos;
    }

    public void Stop()
    {
        m_Ball.gravityScale = 0f;
        m_Ball.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Active()
    {
        m_Ball.gravityScale = 1.2f;
        m_Ball.constraints = RigidbodyConstraints2D.None;
    }

    public void Reset()
    {
        GameObject.FindObjectOfType<StartFlag>().ResetPosition(m_Ball.transform);
        m_Ball.velocity = Vector2.zero;
        Stop();
    }

    public void CountDown(float _time)
    {
        m_Timer.value = _time;
    }

    public void TimerOn()
    {
        m_timerActive = true;
        m_Timer.gameObject.SetActive(true);
    }

    public void TimerOff()
    {
        m_timerActive = false;
        m_Timer.value = 0;
        m_Timer.gameObject.SetActive(false);
    }
}
