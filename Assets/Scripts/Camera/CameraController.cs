﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    #region Variables
    // 카메라 줌 관련
    public float m_ZoomSpeed;
    public float m_MinCamSize;
    public float m_MaxCamSize;

    // 카메라 이동 관련
    public float m_MoveSpeed;


    #endregion //Variables

    private Camera m_Camera;
    private GameObject m_Ball;
    
	void Start ()
    {
        m_Camera = Camera.main;
        m_Ball = GameObject.Find("ball");
        ResetPosition(m_Ball.transform);
    }
	
	void LateUpdate ()
    {
        if(CanMove())
            SmoothFollow();
	}

    public void Zoom(float _deltaPinch)
    {
        float size = m_Camera.orthographicSize + _deltaPinch * m_ZoomSpeed;
        m_Camera.orthographicSize = Mathf.Clamp(size, m_MinCamSize, m_MaxCamSize);
    }

    public void Move(Vector2 _vec)
    {
        this.transform.Translate(_vec * m_MoveSpeed);
    }

    public void ResetPosition(Transform _lookAt)
    {
        Vector3 resetPosition = new Vector3(_lookAt.position.x, _lookAt.position.y, transform.position.z);
        transform.position = resetPosition;
    }

    private void SmoothFollow()
    {
        Vector3 target = new Vector3(m_Ball.transform.position.x, m_Ball.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * m_MoveSpeed * 10f);
    }

    private bool CanMove()
    {
        if (m_Ball == null) return false;
        if (!GameManager.Instance.m_IsPlaying) return false;
        if (GameManager.Instance.m_IsPause) return false;

        return true;
    }
}
