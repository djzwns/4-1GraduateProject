using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWall : Objects
{
    
    public Animator m_anim;

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
        {
            coll.rigidbody.AddForce(coll.relativeVelocity * -0.1f);
            BGMManager.Instance.PlaySound(m_effectSound);

            m_anim.SetBool("play", true);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (CanActive(coll))
            m_anim.SetBool("play", false);
    }
}
