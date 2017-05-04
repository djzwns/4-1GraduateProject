using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWall : Objects {

    public PhysicsMaterial2D m_Physics;
    public Animator m_anim;

    void Start()
    {
        // 공용 메테리얼을 사용해서 하나를 바꾸면 같은 재질 쓰는 모든 것에 영향이 감.
        m_Physics = gameObject.GetComponent<Collider2D>().sharedMaterial;
        m_PowerEnable = false;
    }

    void Update()
    {
        FollowToFinger();
        RotateObject();
        SetBounciness();
    }

    private void SetBounciness()
    {
        if (m_PowerBar == null) return;

        m_Physics.bounciness = m_PowerBar.value;
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
