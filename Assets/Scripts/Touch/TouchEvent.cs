using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent : Singleton<TouchEvent>
{
    Camera m_Camera;
    CameraController m_CamController;
    GameObject m_Touched;   // 터치 한 오브젝트

    float m_DeltaMagDiff; // 핀치 동작 양

    int m_TouchCount;
    bool m_IsMoved = false;

    #region MonoBehaviourFunctions

    void Awake()
    {
        m_Camera = Camera.main;
        m_CamController = m_Camera.GetComponent<CameraController>();
    }


    void Update()
    {
        // UI 터치
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }


        // 그 외
        m_TouchCount = Input.touchCount;

        TouchState();

        if (PinchZoom())
            m_CamController.Zoom(m_DeltaMagDiff);
    }

    #endregion //MonoBehaviourFunctions

    #region Functions
    public void SetTouched(GameObject _go)
    {
        if (_go == null) return;

        m_Touched = _go;
    }

    bool IsObjectTouched(Vector2 _touchPos)
    {
        // 이미 눌린 오브젝트가 있으면 별다른 동작 없이 반환
        if (m_Touched != null) return true;

        Ray ray = m_Camera.ScreenPointToRay(_touchPos);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        
        if (hit.collider == null) return false;
        if (hit.collider.tag != "Object") return false;

        m_Touched = hit.collider.gameObject;
        
        return true;
    }

    Vector2 TouchDeltaPosition()
    {
        Touch touch = Input.GetTouch(0);

        Vector2 touchPrevPosition = touch.position - touch.deltaPosition;

        Vector2 deltaPosition = touchPrevPosition - touch.position;

        return deltaPosition;
    }
    
    // 핀치 동작
    bool PinchZoom()
    {
        if (m_TouchCount != 2) return false;

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

    // 터치 시작
    void TouchBegan(Vector2 _touchPos)
    {
        if (!IsObjectTouched(_touchPos)) return;

        m_Touched.GetComponent<Objects>().CanMove(true);
    }

    // 터치 종료
    void TouchEnded()
    {
        if (m_Touched == null) return;
        if (m_IsMoved) ObjectControlUI.Instance.SetObject(m_Touched);

        m_Touched.GetComponent<Objects>().CanMove(false);
        m_Touched = null;
        m_IsMoved = false;
    }

    // 드래그
    void TouchMoved()
    {
        if (m_Touched != null) return;

        m_IsMoved = true;
        m_CamController.Move(TouchDeltaPosition());
    }

    // 터치 상태 별 동작
    void TouchState()
    {
        if (m_TouchCount != 1) return;

        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                TouchBegan(touchPos);
                break;

            case TouchPhase.Ended:
                TouchEnded();
                break;

            case TouchPhase.Moved:
                TouchMoved();
                break;

            default:
                break;
        }
    }

    #endregion // Functions
}
