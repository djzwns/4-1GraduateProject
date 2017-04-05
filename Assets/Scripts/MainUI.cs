using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

    public GUIAnimator m_Title;

	// Use this for initialization
	void Start () {
        //m_Title.m_FadeIn.EndAlpha = 0;
        //m_Title.MoveIn(GUIAnimSystem.eGUIMove.Self);
	}

    // 버튼을 일정 시간동안 비활성화 시킨다.
    private IEnumerator DisableButtonForSeconds(GameObject gObj, float disableTime)
    {
        GUIAnimSystem.Instance.EnableButton(gObj.transform, false);

        yield return new WaitForSeconds(disableTime);

        GUIAnimSystem.Instance.EnableButton(gObj.transform, true);
    }
}
