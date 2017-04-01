using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    Camera m_Camera;
    GameObject m_Touched;   // 터치 한 오브젝트

    float m_DeltaMagDiff; // 핀치 동작 양


    #region MonoBehaviourFunctions

    void Awake()
    {
        m_Camera = Camera.main;
    }


    void Update()
    {
        int touchCount = Input.touchCount;

        // 1포인트에만 반응
        if(touchCount == 1)
            TouchState();

        if (PinchZoom())
            m_Camera.GetComponent<CameraContorller>().Zoom(m_DeltaMagDiff);
    }

    #endregion //MonoBehaviourFunctions

    #region Functions

    bool IsTouchObject(Vector2 _touchPos)
    {
        Ray ray = m_Camera.ScreenPointToRay(_touchPos);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider == null) return false;
        if (hit.collider.tag != "Object") return false;
        

        m_Touched = hit.collider.gameObject;
        
        return true;
    }

    void TouchDrag()
    {
        Touch touch = Input.GetTouch(0);
    }

    // 핀치 동작
    bool PinchZoom()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);

        // 움직이기 전 좌표
        Vector2 firstTouchPrevPosition = firstTouch.position - firstTouch.deltaPosition;
        Vector2 secondTouchPrevPosition = secondTouch.position - secondTouch.deltaPosition;

        // 움직이기 전 후 벡터 길이
        float prevTouchDeltaMag = (firstTouchPrevPosition - secondTouchPrevPosition).magnitude;
        float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;

        // 음수 : 벌어짐, 양수 : 좁혀짐
        m_DeltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

        if (m_DeltaMagDiff != 0)
            return true;

        return false;
    }


    // 터치 상태 별 동작
    void TouchState()
    {
        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (IsTouchObject(touchPos))
                    m_Touched.GetComponent<Objects>().CanMove(true);

                break;

            case TouchPhase.Ended:
                m_Touched.GetComponent<Objects>().CanMove(false);
                m_Touched = null;
                break;

            case TouchPhase.Moved:
                break;

            default:
                break;
        }
    }

    #endregion // Functions
}
