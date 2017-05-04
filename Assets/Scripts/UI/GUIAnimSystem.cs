using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GUIAnimSystem : Singleton<GUIAnimSystem> {

    [Range(0.5f, 10f)]
    public float m_GUISpeed;

    public void LoadLevel(string _LevelName, float _delay = 0)
    {
        StartCoroutine(DelayLoadLevel(_LevelName, _delay));
    }

    public Canvas GetCanvas(Transform _trans)
    {
        return _trans.root.GetComponent<Canvas>();
    }

    IEnumerator DelayLoadLevel(string _LevelName, float _delay)
    {
        yield return new WaitForSeconds(_delay);

        UnityEngine.SceneManagement.SceneManager.LoadScene(_LevelName);
    }

    public void EnableButton(Transform _trans, bool _enable)
    {
        _trans.GetComponent<Button>().enabled = _enable;
    }

    public void EnableAllButton(bool _enable)
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.enabled = true;
        }
    }

    public void ButtonAddEvents(Transform _trans, UnityAction _action)
    {
        Button button = _trans.GetComponent<Button>();
        button.onClick.AddListener(_action);
    }

    /// <summary>
    /// 모든 버튼에 이벤트 추가
    /// </summary>
    /// <param name="_action"></param>
    public void AllButtonAddEvents(UnityAction _action)
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(_action);
        }
    }

    /// <summary>
    /// UI 에 지정된 모든 애니메이션을 실행시킴
    /// </summary>
    /// <param name="_trans"></param>
    public void MoveIn(Transform _trans)
    {
        GUIAnimator gui = _trans.GetComponent<GUIAnimator>();

        // 움직임
        if (gui.m_MoveIn.Enable)
        {
            StartCoroutine(MoveIn(gui));
        }

        // 페이드
        if (gui.m_FadeIn.Enable)
        {
            StartCoroutine(FadeIn(gui));
        }

        // 스케일
        if (gui.m_ScaleIn.Enable)
        {
            StartCoroutine(ScaleIn(gui));
        }

        // 스케일 루프
        if (gui.m_ScaleLoop.Enable)
        {
            StartCoroutine(ScaleLoop(gui));
        }
    }

    public void MoveOut(Transform _trans)
    {
        GUIAnimator gui = _trans.GetComponent<GUIAnimator>();

        // 움직임
        if (gui.m_MoveOut.Enable)
        {
            StartCoroutine(MoveOut(gui));
        }

        // 페이드
        if (gui.m_FadeOut.Enable)
        {
        }

        // 스케일
        if (gui.m_ScaleOut.Enable)
        {
            StartCoroutine(ScaleOut(gui));
        }
    }

    IEnumerator FadeIn(GUIAnimator _obj)
    {
        yield return new WaitForSeconds(_obj.m_FadeIn.Delay);

        CanvasGroup cg = _obj.GetComponent<CanvasGroup>();
        float endAlpha = _obj.m_FadeIn.EndAlpha;
        float rate = (endAlpha - cg.alpha) / _obj.m_FadeIn.Time;

        bool fading = true;
        float time = 0;

        while (fading)
        {
            time += Time.deltaTime;

            if (time > _obj.m_FadeIn.Time)
            {
                fading = false;
                cg.alpha = endAlpha;
            }
            else
            {
                cg.alpha = Mathf.Clamp(cg.alpha + (rate * Time.deltaTime), 0, 1);
            }

            yield return null;
        }
    }

    IEnumerator MoveIn(GUIAnimator _gui)
    {
        // 지연시간 지난 후 실행
        yield return new WaitForSeconds(_gui.m_MoveIn.Delay);
        
        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_MoveIn.Time;

            _gui.transform.position = Vector3.Lerp(_gui.m_MoveIn.BeginPos, _gui.m_MoveIn.EndPos, t);

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator MoveOut(GUIAnimator _gui)
    {
        // 지연시간 지난 후 실행
        yield return new WaitForSeconds(_gui.m_MoveOut.Delay);

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_MoveOut.Time;

            _gui.transform.position = Vector3.Lerp(_gui.m_MoveOut.BeginPos, _gui.m_MoveOut.EndPos, t);
                        
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ScaleIn(GUIAnimator _gui)
    {
        yield return new WaitForSeconds(_gui.m_ScaleIn.Delay);

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_ScaleIn.Time;

            _gui.transform.localScale = Vector3.Lerp(_gui.m_ScaleOriginal, _gui.m_ScaleIn.Size, t);
            
            yield return new WaitForFixedUpdate();
        }
    }


    IEnumerator ScaleOut(GUIAnimator _gui)
    {
        yield return new WaitForSeconds(_gui.m_ScaleOut.Delay);

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_ScaleOut.Time;

            _gui.transform.localScale = Vector3.Lerp(_gui.m_ScaleOut.Size, _gui.m_ScaleOriginal, t);
            
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ScaleLoop(GUIAnimator _gui)
    {
        yield return new WaitForSeconds(_gui.m_ScaleLoop.Delay);

        float t;
        while (true)
        {
            t = 0f;
            while (t <= 1)
            {
                t += Time.deltaTime * m_GUISpeed / _gui.m_ScaleLoop.Time;

                _gui.transform.localScale = Vector3.Lerp(_gui.m_ScaleLoop.Min, _gui.m_ScaleLoop.Max, t);

                yield return new WaitForFixedUpdate();
            }

            t = 0f;
            while (t <= 1)
            {
                t += Time.deltaTime * m_GUISpeed / _gui.m_ScaleLoop.Time;

                _gui.transform.localScale = Vector3.Lerp(_gui.m_ScaleLoop.Max, _gui.m_ScaleLoop.Min, t);

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
