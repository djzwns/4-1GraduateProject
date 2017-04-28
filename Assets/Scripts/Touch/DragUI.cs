using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : Singleton<DragUI>, /*IBeginDragHandler,*/ IDragHandler, IEndDragHandler {
    public string m_ObjectName;

    private GameObject m_Go;
    private bool m_Drag;

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if (Input.touchCount != 1) return;

    //}

    public void OnDrag(PointerEventData eventData)
    {
        if (m_Drag) return;
        if (eventData.currentInputModule.IsPointerOverGameObject(0)) return;
        m_Drag = true;

        // UI 밖으로 터치 포인트가 나가면 오브젝트를 생성
        m_Go = ObjectCreator.Instance.CreateObject(m_ObjectName);
        m_Go.GetComponent<Objects>().CanMove(true);

        TouchEvent.Instance.SetTouched(m_Go);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_Go.GetComponent<Objects>().CanMove(false);
        m_Drag = false;
    }
}
