using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIAnimator : MonoBehaviour {
    
    #region MonoBehaviour Functions

    void Awake()
    {        
        MoveInit();
    }

    void MoveInit()
    {
        if (m_MoveIn.Enable)
        {
            m_MoveIn.Done = false;
            m_MoveIn.EndPos = transform.GetComponent<RectTransform>().anchoredPosition;

            switch (m_MoveIn.MoveFrom)
            {
                case ePosMove.ParentPosition:
                    m_MoveIn.BeginPos = transform.parent.GetComponent<RectTransform>().pivot;
                    break;

                case ePosMove.UpperScreenEdge:
                    m_MoveIn.BeginPos = GameObject.Find("Panel (Top Center)").GetComponent<RectTransform>().anchoredPosition + (Vector2.up * transform.GetComponent<RectTransform>().offsetMax.y);
                    break;

                case ePosMove.LeftScreenEdge:
                    m_MoveIn.BeginPos = GameObject.Find("Panel (Middle Left)").GetComponent<RectTransform>().pivot - (Vector2.right * transform.GetComponent<RectTransform>().offsetMax.x);
                    break;

                case ePosMove.RightScreenEdge:
                    m_MoveIn.BeginPos = GameObject.Find("Panel (Middle Right)").GetComponent<RectTransform>().pivot + (Vector2.right * transform.GetComponent<RectTransform>().offsetMax.x);
                    break;

                case ePosMove.BottomScreenEdge:
                    m_MoveIn.BeginPos = GameObject.Find("Panel (Bottom Center)").GetComponent<RectTransform>().pivot - (Vector2.up * transform.GetComponent<RectTransform>().offsetMax.y);
                    break;
            }
        }

        if (m_MoveOut.Enable)
        {
            m_MoveOut.Done = false;
            m_MoveOut.BeginPos = transform.GetComponent<RectTransform>().anchoredPosition;

            switch (m_MoveOut.MoveTo)
            {
                case ePosMove.ParentPosition:
                    m_MoveOut.EndPos = transform.parent.GetComponent<RectTransform>().pivot;
                    break;

                case ePosMove.UpperScreenEdge:
                    m_MoveOut.EndPos = GameObject.Find("Panel (Top Center)").GetComponent<RectTransform>().pivot + (Vector2.up * transform.GetComponent<RectTransform>().offsetMax.y);
                    break;

                case ePosMove.LeftScreenEdge:
                    m_MoveOut.EndPos = GameObject.Find("Panel (Middle Left)").GetComponent<RectTransform>().pivot - (Vector2.right * transform.GetComponent<RectTransform>().offsetMax.x);
                    break;

                case ePosMove.RightScreenEdge:
                    m_MoveOut.EndPos = GameObject.Find("Panel (Middle Right)").GetComponent<RectTransform>().pivot + (Vector2.right * transform.GetComponent<RectTransform>().offsetMax.x);
                    break;

                case ePosMove.BottomScreenEdge:
                    m_MoveOut.EndPos = GameObject.Find("Panel (Bottom Center)").GetComponent<RectTransform>().pivot - (Vector2.up * transform.GetComponent<RectTransform>().offsetMax.y);
                    break;
            }
        }

        transform.localPosition = m_MoveIn.BeginPos;
    }

    void FadeInit()
    {
        if (m_FadeIn.Enable)
        {
            m_FadeIn.Done = false;
        }

        if (m_FadeOut.Enable)
        {
        }
    }

    void ScaleInit()
    {
        if (m_ScaleIn.Enable)
        {
            m_ScaleIn.Done = false;
        }

        if (m_ScaleOut.Enable)
        {
        }
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

    #endregion //Enumeration

    #region Animation Class
    [System.Serializable]
    public abstract class cAnim
    {
        [HideInInspector]
        public bool Animating;
        [HideInInspector]
        public bool Began;
        [HideInInspector]
        public bool Done;

        public bool     Enable;
        public float    Delay;
        public float    Time;
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
    }

    [System.Serializable]
    public class cScaleOut : cAnim
    {
        public Vector3 Size;
    }

    [System.Serializable]
    public class cScaleLoop : cAnim
    {
        public Vector3 Min;
        public Vector3 Max;
    }

    #endregion // Animation Class
}
