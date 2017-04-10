using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objects : MonoBehaviour {
    
    bool m_CanMove      = false;

    [Range(0f, 1f)]
    float m_Power = -1;
    float m_Rotation;

    Slider m_PowerBar;
    Slider m_RotateBar;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FollowToFinger();

        RotateObject();
	}

    void FollowToFinger()
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

    private void RotateObject()
    {
        if (m_RotateBar == null) return;

        transform.rotation = Quaternion.AngleAxis((m_RotateBar.value - 0.5f) * -180f, Vector3.forward);
    }

    #region UI_Control

    public void SetSlider(Slider _power = null, Slider _rotate = null)
    {
        SetPowerBar(_power);
        SetRotateBar(_rotate);
    }

    private void SetPowerBar(Slider _power = null)
    {
        if (_power == null) return;
        
        m_PowerBar = _power;
        m_PowerBar.value = m_Power;
    }

    private void SetRotateBar(Slider _rotate = null)
    {
        if (_rotate == null) return;

        m_RotateBar = _rotate;
        m_RotateBar.value = transform.rotation.z;
    }

    #endregion // UI_Control
}
