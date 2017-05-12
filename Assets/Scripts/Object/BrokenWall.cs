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

        //Destroy(gameObject, m_BreakTime);
        // 파괴시 재사용이 불가능하다고 판단, 재시작시 살려내기 위해 SetActive 이용
        StartCoroutine(StageManager.Instance.EnableGameObject(gameObject, false, m_BreakTime));
        // 부서지는 이펙트 추가.
        // 부서지는 사운드 추가.
    }
}
