using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 시작점. 오브젝트의 위치를 플래그 위치로
/// </summary>
public class StartFlag : MonoBehaviour
{
    private Vector3 m_startPosition;

    void Start()
    {
        m_startPosition = transform.position;
    }

    public void ResetPosition(Transform _ball)
    {
        _ball.position = m_startPosition;
    }
}
