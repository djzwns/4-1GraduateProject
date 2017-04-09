using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_Main : MonoBehaviour {

    public GUIAnimator m_Title;
    public GUIAnimator m_Start;
    public GUIAnimator m_Exit;

	// Use this for initialization
	void Start () {
        m_Title.MoveIn();
        m_Start.MoveIn();
        m_Exit.MoveIn();
	}

    public void HideAllGUIs()
    {
        m_Title.MoveOut();
        m_Start.MoveOut();
        m_Exit.MoveOut();
    }
}
