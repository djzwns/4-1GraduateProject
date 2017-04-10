using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    private bool m_IsPlaying;
    private Rigidbody2D m_Ball;

    void Awake()
    {
        m_IsPlaying = false;
        m_Ball = GameObject.Find("ball").GetComponent<Rigidbody2D>();

        StopBall();
    }


    #region Button Active

    public void TogglePlayStop()
    {
        m_IsPlaying = !m_IsPlaying;

        if (m_IsPlaying) ActiveBall();
        else StopBall(); 
    }    
    

    public void ResetGame()
    {
    }

    private void StopBall()
    {
        m_Ball.constraints = RigidbodyConstraints2D.FreezeAll;
        m_Ball.gravityScale = 0f;
    }

    private void ActiveBall()
    {
        m_Ball.constraints = RigidbodyConstraints2D.None;
        m_Ball.gravityScale = 1f;
    }

    #endregion // Button Active
}
