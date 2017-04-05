using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : Singleton<DragUI>, IBeginDragHandler, IDragHandler, IEndDragHandler {
    GameObject go;

    public void OnBeginDrag(PointerEventData eventData)
    {
        go = ObjectCreator.Instance.CreateObject("brick");
        go.GetComponent<Objects>().CanMove(true);

        TouchEvent.Instance.SetTouched(go);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        go.GetComponent<Objects>().CanMove(false);
    }
}
