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
    // 다시시작, 클리어시 스테이지 이동 구현

    #region Button Active

    public void TogglePlayStop()
    {
        m_IsPlaying = !m_IsPlaying;

        if (m_IsPlaying) ActiveBall();
        else StopBall(); 
    }    
    
    /// <summary>
    /// 게임 리셋
    /// </summary>
    public void ResetGame()
    {
        GameObject.FindObjectOfType<StartFlag>().ResetPosition(m_Ball.transform);
    }

    /// <summary>
    /// 일시 정지
    /// </summary>
    private void StopBall()
    {
        m_Ball.constraints = RigidbodyConstraints2D.FreezeAll;
        m_Ball.gravityScale = 0f;
    }

    /// <summary>
    /// 실행
    /// </summary>
    private void ActiveBall()
    {
        m_Ball.constraints = RigidbodyConstraints2D.None;
        m_Ball.gravityScale = 1f;
    }

    #endregion // Button Active
}
