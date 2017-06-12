using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIAnimator : MonoBehaviour {
    
    #region MonoBehaviour Functions

    void Start()
    {
        m_bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(transform);

        MoveInit();
        ScaleInit();
    }

    /// <summary>
    /// 찾은 오브젝트의 위치를 스크린좌표로 반환
    /// </summary>
    private Vector3 FindObjectPosition(string _name)
    {
        Vector3 findObjectPosition = GameObject.Find(_name).transform.position;

        return findObjectPosition;
    }

    private void MoveInInit()
    {
        if (m_MoveIn.Enable)
        {
            m_MoveOriginal = transform.position;//RectTransformUtility.PixelAdjustPoint(transform.position, transform, GUIAnimSystem.Instance.GetCanvas(transform));
            
            m_MoveIn.EndPos = m_MoveOriginal;

            switch (m_MoveIn.MoveFrom)
            {
                case ePosMove.ParentPosition:
                    m_MoveIn.BeginPos = transform.parent.position;
                    break;

                case ePosMove.SelfPosition:
                    m_MoveIn.BeginPos = transform.position;
                    break;

                case ePosMove.UpperScreenEdge:
                    m_MoveIn.BeginPos = FindObjectPosition("Panel (Top Center)") + (Vector3.up * m_bounds.size.y * 2);
                    break;

                case ePosMove.LeftScreenEdge:
                    m_MoveIn.BeginPos = FindObjectPosition("Panel (Middle Left)") + (Vector3.left * m_bounds.size.x * 2);
                    break;

                case ePosMove.RightScreenEdge:
                    m_MoveIn.BeginPos = FindObjectPosition("Panel (Middle Right)") + (Vector3.right * m_bounds.size.x * 2);
                    break;

                case ePosMove.BottomScreenEdge:
                    m_MoveIn.BeginPos = FindObjectPosition("Panel (Bottom Center)") + (Vector3.down * m_bounds.size.y * 2);
                    break;
            }
        }
    }

    private void MoveOutInit()
    {
        if (m_MoveOut.Enable)
        {
            m_MoveOut.BeginPos = m_MoveOriginal;

            switch (m_MoveOut.MoveTo)
            {
                case ePosMove.ParentPosition:
                    m_MoveOut.EndPos = transform.parent.position;
                    break;

                case ePosMove.SelfPosition:
                    m_MoveOut.EndPos = transform.position;
                    break;

                case ePosMove.UpperScreenEdge:
                    m_MoveOut.EndPos = FindObjectPosition("Panel (Top Center)") + (Vector3.up * m_bounds.size.y * 2);
                    break;

                case ePosMove.LeftScreenEdge:
                    m_MoveOut.EndPos = FindObjectPosition("Panel (Middle Left)") + (Vector3.left * m_bounds.size.x * 2);
                    break;

                case ePosMove.RightScreenEdge:
                    m_MoveOut.EndPos = FindObjectPosition("Panel (Middle Right)") + (Vector3.right * m_bounds.size.x * 2);
                    break;

                case ePosMove.BottomScreenEdge:
                    m_MoveOut.EndPos = FindObjectPosition("Panel (Bottom Center)") + (Vector3.down * m_bounds.size.y * 2);
                    break;
            }
        }
    }

    /// <summary>
    /// Move 애니메이션 초기 설정.
    /// </summary>
    private void MoveInit()
    {
        MoveInInit();
        MoveOutInit();

        if(m_MoveIn.Enable)
            transform.position = m_MoveIn.BeginPos;
    }

    /// <summary>
    /// Fade 애니메이션 초기 설정
    /// </summary>
    void FadeInit()
    {
        if (m_FadeIn.Enable)
        {
        }

        if (m_FadeOut.Enable)
        {
        }
    }

    /// <summary>
    /// Scale 애니메이션 초기 설정
    /// </summary>
    void ScaleInit()
    {
        if (m_ScaleIn.Enable)
        {
            m_ScaleOriginal = transform.localScale;
        }

        if (m_ScaleOut.Enable)
        {
        }

        if (m_ScaleIn.ScaleFrom == eScale.Zero)
            this.transform.localScale = Vector3.zero;
    }


    #endregion // MonoBehaviour Functions

    #region Variables

    [HideInInspector]
    public float        m_FadeOriginal;
    public cFade        m_FadeIn;
    public cFade        m_FadeOut;
    public cFadeLoop    m_FadeLoop;

    [HideInInspector]
    public Vector3      m_MoveOriginal;
    public cMoveIn      m_MoveIn;
    public cMoveOut     m_MoveOut;

    [HideInInspector]
    public Vector3      m_ScaleOriginal;
    public cScaleIn     m_ScaleIn;
    public cScaleOut    m_ScaleOut;
    public cScaleLoop   m_ScaleLoop;

    private Bounds m_bounds;

    #endregion // Variables

    #region Animator Functions
    
    public void MoveIn()
    {
        GUIAnimSystem.Instance.MoveIn(transform);
    }

    public void MoveOut()
    {
        GUIAnimSystem.Instance.MoveOut(transform);
    }

    #endregion // Animator Functions

    #region Enumeration

    public enum ePosMove
    {
        ParentPosition      = 0,
        SelfPosition        = 1,
        UpperScreenEdge     = 2,
        LeftScreenEdge      = 3,
        RightScreenEdge     = 4,
        BottomScreenEdge    = 5
    }

    public enum eScale
    {
        None = 0,
        Zero = 1
    }

    #endregion //Enumeration

    #region Animation Class
    [System.Serializable]
    public abstract class cAnim
    {
        public bool     Enable;
        public float    Delay;
        public float    Time;
        [HideInInspector]
        public bool     Began;
        [HideInInspector]
        public bool     Ended;
    }

    [System.Serializable]
    public class cFade : cAnim
    {
        [HideInInspector]
        public float    EndAlpha;
        public bool     FadeChildren;
    }

    [System.Serializable]
    public class cFadeLoop : cAnim
    {
    }

    [System.Serializable]
    public class cMoveIn : cAnim
    {
        [HideInInspector]
        public Vector3 BeginPos;
        [HideInInspector]
        public Vector3 EndPos;
        public ePosMove MoveFrom;
    }

    [System.Serializable]
    public class cMoveOut : cAnim
    {
        [HideInInspector]
        public Vector3 BeginPos;
        [HideInInspector]
        public Vector3 EndPos;
        public ePosMove MoveTo;
    }

    [System.Serializable]
    public class cScaleIn : cAnim
    {
        public Vector3 Size;
        public eScale ScaleFrom;
    }

    [System.Serializable]
    public class cScaleOut : cAnim
    {
        public Vector3 Size;
        public eScale ScaleTo;
    }

    [System.Serializable]
    public class cScaleLoop : cAnim
    {
        public Vector3 Min;
        public Vector3 Max;
    }

    #endregion // Animation Class
}
