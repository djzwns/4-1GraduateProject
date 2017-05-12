using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Objects
{
    public Transform m_fanBody;
    public Transform m_joint;
    
    private float m_max_power = 30f;
    [Range(0f, 2f)]
    public float m_const_power = 0;

    void Start()
    {
        m_PowerEnable = true;
    }

    void Update()
    {
        FollowToFinger(m_fanBody);
        RotateObject(m_joint);
        PowerRenewal();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;

        //Debug.Log("test");
        Blow(coll.gameObject);
    }

    private void Blow(GameObject _go)
    {
        Rigidbody2D rigid = _go.GetComponent<Rigidbody2D>();

        if (rigid == null) return;

        float power = 0;
        if (m_const_power == 0)
            power = m_max_power * (m_power + 1);
        else
            power = m_const_power * m_max_power;

        rigid.AddForce(this.transform.up * power, ForceMode2D.Force);
    }
}
