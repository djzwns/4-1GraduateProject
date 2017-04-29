using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private int m_startingPage = 0;
    
    public float m_fastSwipeThresholdTime = 0.3f;
    public int m_fastSwipeThresholdDistance = 100;
    public float m_decelerationRate = 10f;
    // 스와이프하기 시작한 위치와 content의 위치 차
    private float m_difference;

    // 스와이프 속도 최대치
    private int m_fastSwipeThresholdMaxLimit;

    private ScrollRect m_scrollRectComponent;
    private RectTransform m_scrollRectTransform;
    private RectTransform m_content;

    // 드래그 방향
    private bool m_horizontal;

    // 페이지 관련 변수
    private int m_pageCount;
    public int m_currentPage { get; private set; }

    // 선형보간 실행, 위치
    private bool m_lerp;
    private Vector2 m_lerpTo;

    // 모든 페이지 위치 리스트
    private List<Vector2> m_pagePositions = new List<Vector2>();
    
    private bool m_dragging;
    private float m_timeStamp;
    private Vector2 m_startPosition;
    

    void Start()
    {
        m_scrollRectComponent = GetComponent<ScrollRect>();
        m_scrollRectTransform = GetComponent<RectTransform>();
        m_content = m_scrollRectComponent.content;
        m_pageCount = m_content.childCount;

        if (m_scrollRectComponent.horizontal && !m_scrollRectComponent.vertical)
            m_horizontal = true;
        else if (!m_scrollRectComponent.horizontal && m_scrollRectComponent.vertical)
            m_horizontal = false;
        else
            m_horizontal = true;

        m_lerp = false;

        // 초기 설정.
        SetPagePositions();
        SetPage(m_startingPage);
    }

    void Update()
    {
        if (m_lerp)
        {
            float decelerate = Mathf.Min(m_decelerationRate * Time.deltaTime, 1f);
            m_content.anchoredPosition = Vector2.Lerp(m_content.anchoredPosition, m_lerpTo, decelerate);

            if (Vector2.SqrMagnitude(m_content.anchoredPosition - m_lerpTo) < 0.25f)
            {
                m_content.anchoredPosition = m_lerpTo;
                m_lerp = false;

                m_scrollRectComponent.velocity = Vector2.zero;
            }
        }
    }

    /// <summary>
    /// 페이지 좌표 설정
    /// </summary>
    private void SetPagePositions()
    {
        int width = 0;
        int height = 0;
        int offsetX = 0;
        int offsetY = 0;
        int contentWidth = 0;
        int contentHeight = 0;

        if (m_horizontal)
        {
            width = (int)m_scrollRectTransform.rect.width;

            offsetX = width / 2;
            contentWidth = width * m_pageCount;

            m_fastSwipeThresholdMaxLimit = width;
        }
        else
        {
            height = (int)m_scrollRectTransform.rect.height;
            offsetY = height / 2;
            contentHeight = height * m_pageCount;
            m_fastSwipeThresholdMaxLimit = height;
        }

        // content 의 width 사이즈를 초기화
        Vector2 newSize = new Vector2(contentWidth, contentHeight);
        m_content.sizeDelta = newSize;
        // content 의 앵커 좌표 초기화
        Vector2 newPosition = new Vector2(contentWidth * 0.5f, contentHeight * 0.5f);
        m_content.anchoredPosition = newPosition;

        // content 에 있는 모든 요소의 좌표 초기화
        for (int i = 0; i < m_pageCount; ++i)
        {
            RectTransform child = m_content.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition;
            if (m_horizontal)
                childPosition = new Vector2(i * width - contentWidth * 0.5f + offsetX, 0f);
            else
                childPosition = new Vector2(i * height - contentHeight * 0.5f + offsetY, 0f);

            child.anchoredPosition = childPosition;
            m_pagePositions.Add(-childPosition);
        }
    }

    // 현재 페이지 설정
    private void SetPage(int _pageIndex)
    {
        _pageIndex = Mathf.Clamp(_pageIndex, 0, m_pageCount - 1);
        m_content.anchoredPosition = m_pagePositions[_pageIndex];
        m_currentPage = _pageIndex;
    }

    // 선형보간할 페이지 설정
    private void LerpToPage(int _pageIndex)
    {
        _pageIndex = Mathf.Clamp(_pageIndex, 0, m_pageCount - 1);
        m_lerpTo = m_pagePositions[_pageIndex];
        m_lerp = true;
        m_currentPage = _pageIndex;
    }
    
    private void NextScreen()
    {
        LerpToPage(m_currentPage + 1);
    }

    private void PreviousScreen()
    {
        LerpToPage(m_currentPage - 1);
    }

    // 가장 가까운 페이지를 반환
    private int GetNearestPage()
    {
        Vector2 currentPosition = m_content.anchoredPosition;

        float distance = float.MaxValue;
        int nearstPage = m_currentPage;

        for (int i = 0; i < m_pagePositions.Count; ++i)
        {
            float dist = Vector2.SqrMagnitude(currentPosition - m_pagePositions[i]);
            if (dist < distance)
            {
                distance = dist;
                nearstPage = i;
            }
        }

        return nearstPage;
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        // 선형 보간으로 이동중일 때 화면에 손가락 터치시 보간을 멈춤.
        m_lerp = false;
        // 아직 드래그 시작 안함
        m_dragging = false;
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        if (m_horizontal)
            m_difference = m_startPosition.x - m_content.anchoredPosition.x;
        else
            m_difference = m_startPosition.y - m_content.anchoredPosition.y;

        if (Swipping())
        {
            if (m_difference > 0)
                NextScreen();
            else
                PreviousScreen();
        }
        else
            LerpToPage(GetNearestPage());

        m_dragging = false;
    }
    
    private bool SwipeTimeOver()
    {
        return Time.unscaledTime - m_timeStamp > m_fastSwipeThresholdTime;
    }
    
    private bool SwipeMove()
    {
        return Mathf.Abs(m_difference) < m_fastSwipeThresholdDistance;
    }
    
    private bool SwipeThresholdLimit()
    {
        return Mathf.Abs(m_difference) > m_fastSwipeThresholdMaxLimit;
    }

    private bool Swipping()
    {
        if (SwipeTimeOver()) return false;
        if (SwipeMove()) return false;
        if (SwipeThresholdLimit()) return false;

        return true;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        if (!m_dragging)
        {
            m_dragging = true;
            // 현재 프레임 시작 시간을 기억해둠
            m_timeStamp = Time.unscaledTime;
            // content의 현재 위치를 기억해둠
            m_startPosition = m_content.anchoredPosition;
        }
    }
}
