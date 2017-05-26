using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : Objects
{

    void Start()
    {
        m_PowerEnable = false;
        m_rotateObject = this.transform;
    }

    void Update()
    {
        FollowToFinger(this.transform);
        RotateObject(m_rotateObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (CanActive(coll))
            BGMManager.Instance.PlaySound(m_effectSound);
    }
}
