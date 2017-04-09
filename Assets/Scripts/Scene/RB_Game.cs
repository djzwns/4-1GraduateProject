using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB_Game : MonoBehaviour {

    bool m_BoxOpen;
    public GUIAnimator m_ObjectBox;
    public GUIAnimator m_OpenButton;

    void Awake()
    {
        m_BoxOpen = false;
    }

    public void ToggleObjectBox()
    {
        m_BoxOpen = !m_BoxOpen;
        if (m_BoxOpen == true)
        {
            m_OpenButton.MoveIn();
            m_ObjectBox.MoveIn();
        }

        else
        {
            m_OpenButton.MoveOut();
            m_ObjectBox.MoveOut();
        }

        StartCoroutine(DisableButtonForSeconds(m_OpenButton.gameObject, 1.0f));
    }

    private IEnumerator DisableButtonForSeconds(GameObject _gObj, float _disableTime)
    {
        GUIAnimSystem.Instance.EnableButton(_gObj.transform, false);

        yield return new WaitForSeconds(_disableTime);

        GUIAnimSystem.Instance.EnableButton(_gObj.transform, true);
    }
}
