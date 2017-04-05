using System.Collections;
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

    Camera m_Camera;

	// Use this for initialization
	void Start () {
        m_Camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
