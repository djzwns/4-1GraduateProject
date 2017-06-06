using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public bool m_IsPlaying { get; private set; }
    public bool m_IsPause { get; private set; }

    private Ball m_Ball;
    private CameraController m_CamController;

    private GameObject m_SceneController;
    public StageTimer m_stageTimer;

    public delegate void GameStartHandler();
    public GameStartHandler gamestart_callback { get; set; }

    public delegate void GameResetHandler();
    public GameResetHandler gamereset_callback { get; set; }

    void Start()
    {
        m_IsPlaying = false;
        m_Ball = GameObject.Find("ball").GetComponent<Ball>();
        m_CamController = GameObject.FindObjectOfType<CameraController>();
        m_SceneController = GameObject.Find("-- SceneController --");
        m_stageTimer.timeout_callback += ResetGame;
        gamereset_callback += ResetGame;
    }

    // 다시시작, 클리어시 스테이지 이동 구현

    #region Button Active

    public void StopGame()
    {
        m_Ball.Stop();
    }

    public void PauseGame()
    {
        m_IsPause = !m_IsPause;
    }

    public void PlayGame()
    {
        if (m_IsPause) return;
        if (m_IsPlaying)
        {
            //ResetGame();
            if (gamereset_callback != null)
                gamereset_callback();
            return;
        }
        m_stageTimer.SetActive(true);
        m_SceneController.SendMessage("HideAllGUIs");
        m_IsPlaying = true;
        m_IsPause = false;
        m_Ball.Active();

        if (gamestart_callback != null)
        {
            gamestart_callback();
        }
    }

    /// <summary>
    /// 게임 리셋
    /// </summary>
    public void ResetGame()
    {
        m_IsPlaying = false;
        m_IsPause = false;
        StageManager.Instance.m_IsClear = false;
        StageManager.Instance.ResetStageObject();
        m_Ball.Reset();
        m_CamController.ResetPosition(m_Ball.transform);
        m_SceneController.SendMessage("DefaultStar");
        m_stageTimer.SetActive(false);
    }

    /// <summary>
    /// 유저가 생성한 오브젝트를 지워버림.
    /// </summary>
    public void ObjectsDestroy()
    {
        Objects[] userObjects = GameObject.Find("UserObjects").GetComponentsInChildren<Objects>();

        foreach (Objects uo in userObjects)
        {
            Destroy(uo.gameObject);
        }
    }

    #endregion // Button Active
}
