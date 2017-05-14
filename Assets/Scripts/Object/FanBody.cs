using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBody : Objects
{
    public Transform m_joint;
    public GameObject m_effect;

    private float m_max_power = 30f;
    [Range(0f, 2f)]
    public float m_const_power = 0;

    void Start()
    {
        m_PowerEnable = true;
    }

    void Update()
    {
        FollowToFinger(this.transform);
        RotateObject(m_joint);
        PowerRenewal();

        if (m_const_power == 0) return;
        if (m_power > -1) m_effect.SetActive(true);
        else m_effect.SetActive(false);
    }

    public void Blow(GameObject _go)
    {
        Rigidbody2D rigid = _go.GetComponent<Rigidbody2D>();

        if (rigid == null) return;

        float power = 0;
        if (m_const_power == 0)
            power = m_max_power * (m_power + 1);
        else
            power = m_const_power * m_max_power;

        rigid.AddForce(m_joint.up * power, ForceMode2D.Force);
    }
}
