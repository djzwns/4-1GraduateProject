using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;

        // 부딪히면 삭제
        //Destroy(this.gameObject);
        StartCoroutine(StageManager.Instance.EnableGameObject(gameObject, false));
        // 먹을때 사운드 추가
        // 점수 관련 추가( 별 갯수 증가, 점수 등등.. )
        // 맵 정보상에서 먹은거로 표시해서 
        // 다음에 다시 플레이 하더라도 생성되지 않도록
        // 데이터 수정.        
    }
}
