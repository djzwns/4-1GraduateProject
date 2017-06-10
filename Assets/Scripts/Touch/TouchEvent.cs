using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent : Singleton<TouchEvent>
{
    private Timer m_timer;
    private Camera m_Camera;
    private CameraController m_CamController; 
    // 터치 한 오브젝트
    public GameObject m_Touched { get; private set; }  
    // 오브젝트를 움직이고 있는지
    public bool m_IsMoved { get; private set; }
    // UI 드래그중
    private bool m_UIDrag = false;

    private float m_DeltaMagDiff; // 핀치 동작 양

    private int m_TouchCount;

    public Transform m_WasteUI;

    #region MonoBehaviourFunctions

    void Awake()
    {
        m_Camera = Camera.main;
        m_CamController = m_Camera.GetComponent<CameraController>();

        m_timer = new Timer(0.5f);
    }


    void Update()
    {
        if (CanTouchActive())
            TouchState();
    }

    #endregion //MonoBehaviourFunctions

    #region Functions
    public void SetTouched(GameObject _go)
    {
        if (_go == null) return;

        m_Touched = _go;
        m_IsMoved = true;
    }

    private void ObjectTouched(Vector2 _touchPos)
    {
        // 이미 눌린 오브젝트가 있으면 별다른 동작 없이 반환
        //if (m_Touched != null) return true;

        Ray ray = m_Camera.ScreenPointToRay(_touchPos);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider == null || hit.collider.tag != "Object")
        {
            if(m_Touched != null)
                m_Touched.GetComponent<Objects>().SetSlider();
            m_Touched = null;
            return;
        }

        m_Touched = hit.collider.gameObject;
    }

    private Vector2 TouchDeltaPosition()
    {
        Touch touch = Input.GetTouch(0);

        Vector2 touchPrevPosition = touch.position - touch.deltaPosition;

        Vector2 deltaPosition = touchPrevPosition - touch.position;

        return deltaPosition;
    }
    
    // 핀치 동작
    private bool PinchZoom()
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

    /// <summary>
    /// 터치 시작
    /// </summary>
    /// <param name="_touchPos"></param>
    private void TouchBegan(Vector2 _touchPos)
    {
        ObjectTouched(_touchPos);
    }

    /// <summary>
    /// 터치 종료
    /// </summary>
    private void TouchEnded()
    {
        m_UIDrag = false;
        m_timer.Reset();

        if (m_Touched == null)
        {
            ObjectControlUI.Instance.SetObject(m_Touched);
            return;
        }
        if (!m_IsMoved) ObjectControlUI.Instance.SetObject(m_Touched);
        else SendMessage("BasketOff");

        m_Touched.GetComponent<Objects>().CanMove(false);
        if (OnUITouchPosition(m_WasteUI)) Destroy(m_Touched);
        m_WasteUI.localScale = Vector3.one;
        m_Touched = null;
        m_IsMoved = false;
    }

    /// <summary>
    ///  터치 후 멈춤
    /// </summary>
    private void TouchStationary()
    {
        if (m_Touched == null) return;

        // 오브젝트 터치시 타이머 작동
        m_timer.Update(Time.deltaTime);

        // 시간 초과 시 오브젝트 움직이기 가능.
        if (!m_timer.IsTimeOut()) return;
        // 쓰레기통 오픈 - 오브젝트가 움직이기 가능해지는 처음 한번만 실행
        if(!m_IsMoved) SendMessage("BasketOn");

        m_Touched.GetComponent<Objects>().CanMove(true);
        m_IsMoved = true;
    }

    /// <summary>
    /// 드래그 동작
    /// </summary>
    private void TouchMoved()
    {
        if (OnUITouchPosition(m_WasteUI))
            m_WasteUI.localScale = Vector3.one * 1.2f;
        else
            m_WasteUI.localScale = Vector3.one;


        if (m_IsMoved)
        {
            SendMessage("BasketOn");
            return;
        }

        if (!m_UIDrag)
        {
            m_CamController.Move(TouchDeltaPosition());
        }
    }

    // 터치 상태 별 동작
    private void TouchState()
    {
        if (PinchZoom())
            m_CamController.Zoom(m_DeltaMagDiff);

        if (m_TouchCount != 1) return;

        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                TouchBegan(touchPos);
                break;

            case TouchPhase.Moved:
                TouchMoved();
                break;

            case TouchPhase.Stationary:
                TouchStationary();
                break;

            case TouchPhase.Ended:
                TouchEnded();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 터치 동작 가능 여부 반환
    /// </summary>
    /// <returns></returns>
    private bool CanTouchActive()
    {
        m_TouchCount = Input.touchCount;

        // 터치 포인트가 없을 떄
        if (m_TouchCount == 0) return false;

        // UI 터치
        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            m_UIDrag = true;
            return false;
        }

        // 게임시작 했을 때
        if (GameManager.Instance.m_IsPlaying) return false;

        // 일시정지 일 때
        if (GameManager.Instance.m_IsPause) return false;

        return true;
    }

    /// <summary>
    /// 터치 좌표에 ui가 있는지 확인.
    /// </summary>
    /// <param name="_ui"></param>
    /// <returns></returns>
    private bool OnUITouchPosition(Transform _ui)
    {
        if (m_Touched == null) return false;
        RectTransform rect = _ui.GetComponent<RectTransform>();

        if (rect == null) return false;

        Vector2 transPosition = m_Touched.transform.position;
        Vector2 transformWorldToScreenPos = m_Camera.WorldToScreenPoint(transPosition);

        bool result = RectTransformUtility.RectangleContainsScreenPoint(rect, transformWorldToScreenPos);
        
        return result;
    }

    #endregion // Functions
}
