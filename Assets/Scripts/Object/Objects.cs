using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Objects : MonoBehaviour
{
    public AudioClip m_effectSound;

    public bool m_UIEnable;
    [HideInInspector]
    public bool m_PowerEnable;
    private bool m_CanMove      = false;
    private Vector3 m_size;
    protected Transform m_rotateObject;

    protected float m_power;
    protected Slider m_PowerBar;
    protected Slider m_RotateBar;

    void Awake()
    {
        m_PowerEnable = false;
        m_size = this.transform.localScale;
        m_rotateObject = this.transform;
    }

	void Update () {
        FollowToFinger(this.transform);

        RotateObject(m_rotateObject);
	}

    /// <summary>
    /// 터치 좌표로 오브젝트 이동
    /// </summary>
    protected void FollowToFinger(Transform _trans)
    {
        if (!m_CanMove) return;

        Vector2 touchPos = Input.GetTouch(0).position;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

        _trans.position = worldPos + Vector2.up * 3f;
    }

    public void CanMove(bool _move)
    {
        m_CanMove = _move;
        PickUp(m_CanMove);
    }

    private void PickUp(bool _pick)
    {
        if (_pick)
        {
            //float t = 0;
            //while (t <= 1)
            //{
            //    t += Time.deltaTime / 0.5f;
            //    this.transform.localScale = Vector3.Lerp(m_size, m_size * 1.2f, t);
            //    yield return new WaitForFixedUpdate();
            //}
            this.transform.localScale = m_size * 1.3f;
        }
        else
        {
            this.transform.localScale = m_size;
        }
    }

    /// <summary>
    /// 슬라이더의 값에 따라 회전
    /// </summary>
    protected void RotateObject(Transform _trans)
    {
        if (m_RotateBar == null) return;

        _trans.rotation = Quaternion.AngleAxis(-m_RotateBar.value, Vector3.forward);
    }

    protected void PowerRenewal()
    {
        if (m_PowerBar == null) return;

        m_power = m_PowerBar.value;
    }

    #region UI_Control

    /// <summary>
    /// 슬라이더바 세팅
    /// </summary>
    public void SetSlider(Slider _power = null, Slider _rotate = null)
    {
        if (GameManager.Instance.m_IsPlaying) return;
        SetPowerBar(_power);
        SetRotateBar(_rotate);
    }

    private void SetPowerBar(Slider _power = null)
    {
        m_PowerBar = _power;
        if (_power == null) return;

        m_PowerBar.value = m_power;
    }

    private void SetRotateBar(Slider _rotate = null)
    {
        m_RotateBar = _rotate;
        if (_rotate == null) return;
        float angle_z = m_rotateObject.localEulerAngles.z;

        if (angle_z >= 270f) angle_z -= 360f;

        m_RotateBar.value = -angle_z;
    }

    protected bool CanActive(Collision2D coll)
    {
        if (!GameManager.Instance.m_IsPlaying) return false;
        if (coll.gameObject.tag != "ball") return false;
        return true;
    }

    public bool CanActive(Collider2D coll)
    {
        if (coll.tag != "ball") return false;
        if (!GameManager.Instance.m_IsPlaying) return false;
        return true;
    }

    #endregion // UI_Control
}
