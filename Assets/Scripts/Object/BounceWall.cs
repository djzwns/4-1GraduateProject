using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWall : Objects
{
    
    public Animator m_anim;

    void Start()
    {
        m_PowerEnable = false;
    }

    void Update()
    {
        FollowToFinger();
        RotateObject(this.transform);
        SetBounciness();
    }

    private void SetBounciness()
    {
        if (m_PowerBar == null) return;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "ball") return;

        coll.rigidbody.AddForce(coll.relativeVelocity * -0.1f);

        m_anim.SetBool("play", true);
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "ball") return;

        m_anim.SetBool("play", false);
    }
}
