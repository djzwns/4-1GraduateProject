using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public AudioClip m_effectSound;
    private GameObject m_sceneController;

    void Start()
    {
        m_sceneController = GameObject.Find("-- SceneController --");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;

        // 부딪히면 삭제
        //Destroy(this.gameObject);
        StartCoroutine(StageManager.Instance.EnableGameObject(gameObject, false));
        // 먹을때 사운드 추가
        BGMManager.Instance.PlaySound(m_effectSound);
        // 점수 관련 추가( 별 갯수 증가, 점수 등등.. )
        m_sceneController.SendMessage("AddStar");
    }
}
