using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : Objects
{
    public float m_BreakTime;

    void OnCollisionEnter2D(Collision2D col)
    {
        // 공이 닿으면 부서짐
        if (col.gameObject.tag != "ball") return;

        Destroy(gameObject, m_BreakTime);
        // 부서지는 이펙트 추가..
    }
}
