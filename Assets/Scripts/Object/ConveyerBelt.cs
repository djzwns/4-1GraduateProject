using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Objects
{
    private float m_max_power = 4f;
    [Tooltip("음수는 왼쪽 양수는 오른쪽")] [Range(-4f, 4f)]
    public float m_const_power = 0;
    public Transform[] wheels;

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

    void FixedUpdate()
    {
        for (int i = 0; i < wheels.Length; ++i)
        {
            Rotate(wheels[i]);
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (CanActive(coll))
        {
            coll.rigidbody.AddForce(transform.right * GetPower(), ForceMode2D.Force);
        }
    }

    private void Rotate(Transform _transform)
    {
        if (GetPower() != 0)
        {
            _transform.Rotate(Vector3.down * GetPower() * 10f);
        }
    }

    private float GetPower()
    {
        float power = 0;
        if (m_const_power == 0)
            power = m_max_power * m_power;
        else
            power = m_const_power;

        return power;
    }
}
