using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D m_Ball;
    private Vector2 m_lastVelocity;

    void Awake()
    {
        m_Ball = gameObject.GetComponent<Rigidbody2D>();
        GameObject.FindObjectOfType<StartFlag>().ResetPosition(m_Ball.transform);
        Stop();
    }

    public void Stop()
    {
        m_lastVelocity = m_Ball.velocity;
        m_Ball.gravityScale = 0f;
        m_Ball.constraints = RigidbodyConstraints2D.FreezeAll;
        //Debug.Log(string.Format("last : {0}", m_lastVelocity));
    }

    public void Active()
    {
        m_Ball.velocity = m_lastVelocity;
        m_Ball.gravityScale = 1f;
        m_Ball.constraints = RigidbodyConstraints2D.None;
        //Debug.Log(string.Format("ball vel : {0}", m_Ball.velocity));
    }

    public void Reset()
    {
        GameObject.FindObjectOfType<StartFlag>().ResetPosition(m_Ball.transform);
        m_Ball.velocity = Vector2.zero;
        Stop();
    }
}
