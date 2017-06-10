using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private Camera m_camera;

    void Awake()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public Vector3 WorldToScreenPosition(Vector3 _worldPosition)
    {
        return m_camera.WorldToScreenPoint(_worldPosition);
    }
}
