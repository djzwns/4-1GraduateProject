using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Borodar.FarlandSkies.LowPoly;

/// <summary>
/// 맵 이동시 마다 이 씬으로 넘어와서 배의 이동상태를 보여줌.
/// </summary>
public class RB_Travel : MonoBehaviour
{
    public float m_moveTime = 5f;

    public GameObject m_ship;
    public GameObject m_destination;
    public GameObject[] m_islands;

    private Transform m_camera;

    private SceneManager m_sceneManager;
    private float[] m_time = { 60, 75, 100 };

    private int m_current;
    private int m_next;

    void Awake()
    {
        BGMManager.Instance.BGMChange(-1);
    }

    void Start()
    {
        m_camera = GameObject.Find("Main Camera").transform;
        m_sceneManager = SceneManager.Instance;

        int mapNum = StageInformation.m_stageNum;
        m_current = Mathf.Clamp((mapNum - 1) % 3, 0, 2);
        m_next = Mathf.Clamp(mapNum % 3, 0, 2);

        MapActive(mapNum / 3);
        Acting();
    }

    void FixedUpdate()
    {
        float dist = (m_ship.transform.position.x - m_destination.transform.position.x) * 0.5f;
        m_camera.position = new Vector3(m_ship.transform.position.x - dist, m_camera.position.y, m_camera.position.z);
    }
    
    /// <summary>
    /// 씬이 시작하면 연출시작
    /// </summary>
    private void Acting()
    {
        StartCoroutine("MoveShip");
        StartCoroutine("TimeFlow");
        StartCoroutine("CloudFlow");
    }

    /// <summary>
    /// 정해진 시간 안에 배를 이동시킴
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveShip()
    {
        float time = 0f;
        float original_xPos = m_ship.transform.position.x;
        while (time <= 1)
        {
            if (m_next == 0) time += Time.deltaTime / m_moveTime;
            else time += Time.deltaTime / 0;
            //time = time * time;
            // 배 이동
            float xPos = Mathf.Lerp(original_xPos, m_destination.transform.position.x, time);
            m_ship.transform.position = new Vector3(xPos, m_ship.transform.position.y);

            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// 정해진 시간 안에 시간이 흐름
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeFlow()
    {
        float time = 0f;

        while (time <= 1)
        {
            time += Time.deltaTime / m_moveTime;
            
            // 시간 흐름
            m_sceneManager.TimeOfDay = Mathf.Lerp(m_time[m_current], m_time[m_next], time);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    private IEnumerator CloudFlow()
    {
        SkyboxController sbCtrl = m_sceneManager.GetComponentInChildren<SkyboxController>();

        while (true)
        {
            sbCtrl.CloudsRotation = -Time.time * 2f;
            yield return new WaitForFixedUpdate();
        }
    }

    private void MapActive(int _mapNum)
    {
        for (int i = 0; i < m_islands.Length; ++i)
        {
            if (i == _mapNum) m_islands[i].SetActive(true);
            else m_islands[i].SetActive(false);
        }
    }
}
