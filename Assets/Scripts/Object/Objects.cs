﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Objects : MonoBehaviour {

    public bool m_UIEnable;
    [HideInInspector]
    public bool m_PowerEnable;
    bool m_CanMove      = false;

    protected Slider m_PowerBar;
    Slider m_RotateBar;

    void Awake()
    {
        m_PowerEnable = false;
    }

	void Update () {
        FollowToFinger();

        RotateObject();
	}

    /// <summary>
    /// 터치 좌표로 오브젝트 이동
    /// </summary>
    protected void FollowToFinger()
    {
        if (!m_CanMove) return;

        Vector2 touchPos = Input.GetTouch(0).position;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

        transform.position = worldPos;
    }

    public void CanMove(bool _move)
    {
        m_CanMove = _move;
    }

    /// <summary>
    /// 슬라이더의 값에 따라 회전
    /// </summary>
    protected void RotateObject()
    {
        if (m_RotateBar == null) return;

        transform.rotation = Quaternion.AngleAxis(-m_RotateBar.value, Vector3.forward);
    }

    #region UI_Control

    /// <summary>
    /// 슬라이더바 세팅
    /// </summary>
    public void SetSlider(Slider _power = null, Slider _rotate = null)
    {
        SetPowerBar(_power);
        SetRotateBar(_rotate);
    }

    private void SetPowerBar(Slider _power = null)
    {
        m_PowerBar = _power;
    }

    private void SetRotateBar(Slider _rotate = null)
    {
        m_RotateBar = _rotate;
        if (_rotate == null) return;
        float angle_z = transform.localEulerAngles.z;

        if (angle_z >= 270f) angle_z -= 360f;

        m_RotateBar.value = -angle_z;
    }

    #endregion // UI_Control
}
