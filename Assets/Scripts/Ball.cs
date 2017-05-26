using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D m_Ball;

    void Awake()
    {
        m_Ball = gameObject.GetComponent<Rigidbody2D>();
        GameObject.FindObjectOfType<StartFlag>().ResetPosition(m_Ball.transform);
        Stop();
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
}
