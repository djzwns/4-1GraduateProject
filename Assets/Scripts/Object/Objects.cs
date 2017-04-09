using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour {
    
    bool m_CanMove      = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FollowToFinger();
	}

    void FollowToFinger()
    {
        if (!m_CanMove) return;

        Vector2 touchPos = Input.GetTouch(0).position;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

        transform.position = worldPos;
    }

    public void CanMove(bool _move)
    {
        m_CanMove = _move;
    }

    public void OpenSettingUI()
    {
    }
}
