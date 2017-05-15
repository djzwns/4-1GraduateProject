using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_Main : MonoBehaviour {

    public GUIAnimator m_Title;
    public GUIAnimator m_Start;

    void Awake()
    {
        BGMManager.Instance.BGMChange(-1);
    }

	void Start ()
    {
        m_Title.MoveIn();
        m_Start.MoveIn();
	}

    public void HideAllGUIs()
    {
        m_Title.MoveOut();
        m_Start.MoveOut();
    }
}
