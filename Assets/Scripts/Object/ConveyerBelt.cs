using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Objects
{
    private float m_max_power = 3f;
    [Tooltip("음수는 왼쪽 양수는 오른쪽")] [Range(-3f, 3f)]
    public float m_const_power = 0;

    void Start () {
        m_PowerEnable = true;
	}
	
	void Update ()
    {
        FollowToFinger();
        RotateObject(this.transform);
        PowerRenewal();
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "ball") return;

        float power;
        if (m_const_power == 0)
            power = m_max_power * m_power;
        else
            power = m_const_power;

        coll.rigidbody.AddForce(transform.right * power, ForceMode2D.Force);
    }
}
