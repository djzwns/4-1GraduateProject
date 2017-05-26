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
        m_rotateObject = this.transform;
    }
	
	void Update ()
    {
        FollowToFinger(this.transform);
        RotateObject(m_rotateObject);
        PowerRenewal();
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (CanActive(coll))
        {
            float power = 0;
            if (m_const_power == 0)
                power = m_max_power * m_power;
            else
                power = m_const_power;

            coll.rigidbody.AddForce(transform.right * power, ForceMode2D.Force);
        }
    }
}
