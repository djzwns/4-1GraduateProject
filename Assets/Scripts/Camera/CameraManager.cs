using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera m_mainCam;
    public Camera m_subCam;

    void Awake()
    {
        MainCameraOn();

        StageManager.Instance.stageinit_callback += SubCameraOn;
        StageManager.Instance.stagecreated_callback += MainCameraOn;
    }

    public void MainCameraOn()
    {
        m_mainCam.enabled = true;
        m_subCam.enabled = false;
    }

    public void SubCameraOn()
    {
        m_subCam.enabled = true;
        m_mainCam.enabled = false;
    }
}
