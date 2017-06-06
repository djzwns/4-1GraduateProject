using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GUIAnimSystem : Singleton<GUIAnimSystem>
{
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
        Button btn = _trans.GetComponent<Button>();
        btn.enabled = _enable;
    }

    public void EnableAllButton(bool _enable)
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.enabled = true;
        }
    }

    public void InteractableButton(Transform _trans, bool _enable)
    {
        Button btn = _trans.GetComponent<Button>();
        btn.interactable = _enable;
    }

    /// <summary>
    /// 특정 버튼에 이벤트 추가
    /// </summary>
    /// <param name="_trans"></param>
    /// <param name="_action"></param>
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
        if (gui.m_MoveIn.Enable && CanAnimationIn(gui.m_MoveIn, gui.m_MoveOut))
        {
            StartCoroutine(MoveIn(gui));
        }

        // 페이드
        if (gui.m_FadeIn.Enable && CanAnimationIn(gui.m_FadeIn, gui.m_FadeOut))
        {
            StartCoroutine(FadeIn(gui));
        }

        // 스케일
        if (gui.m_ScaleIn.Enable && CanAnimationIn(gui.m_ScaleIn, gui.m_ScaleOut))
        {
            StartCoroutine(ScaleIn(gui));
        }

        // 스케일 루프
        if (gui.m_ScaleLoop.Enable && !gui.m_ScaleLoop.Began)
        {
            StartCoroutine(ScaleLoop(gui));
        }
    }

    public void MoveOut(Transform _trans)
    {
        GUIAnimator gui = _trans.GetComponent<GUIAnimator>();

        // 움직임
        if (gui.m_MoveOut.Enable && CanAnimationOut(gui.m_MoveIn, gui.m_MoveOut))
        {
            StartCoroutine(MoveOut(gui));
        }

        // 페이드
        if (gui.m_FadeOut.Enable && CanAnimationOut(gui.m_FadeIn, gui.m_FadeOut))
        {
        }

        // 스케일
        if (gui.m_ScaleOut.Enable && CanAnimationOut(gui.m_ScaleIn, gui.m_ScaleOut))
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

        _gui.m_MoveOut.Ended = false;
        _gui.m_MoveIn.Began = true;
        _gui.m_MoveIn.Ended = false;

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_MoveIn.Time;

            _gui.transform.position = Vector3.Lerp(_gui.m_MoveIn.BeginPos, _gui.m_MoveIn.EndPos, t);

            yield return new WaitForFixedUpdate();
        }

        _gui.m_MoveIn.Began = false;
        _gui.m_MoveIn.Ended = true;
    }

    IEnumerator MoveOut(GUIAnimator _gui)
    {
        // 지연시간 지난 후 실행
        yield return new WaitForSeconds(_gui.m_MoveOut.Delay);

        _gui.m_MoveIn.Ended = false;
        _gui.m_MoveOut.Began = true;
        _gui.m_MoveOut.Ended = false;

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_MoveOut.Time;

            _gui.transform.position = Vector3.Lerp(_gui.m_MoveOut.BeginPos, _gui.m_MoveOut.EndPos, t);

            yield return new WaitForFixedUpdate();
        }

        _gui.m_MoveOut.Began = false;
        _gui.m_MoveOut.Ended = true;
    }

    IEnumerator ScaleIn(GUIAnimator _gui)
    {
        yield return new WaitForSeconds(_gui.m_ScaleIn.Delay);

        _gui.m_ScaleOut.Ended = false;
        _gui.m_ScaleIn.Began = true;
        _gui.m_ScaleIn.Ended = false;

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_ScaleIn.Time;

            _gui.transform.localScale = Vector3.Lerp(_gui.m_ScaleIn.Size, _gui.m_ScaleOriginal, t);

            yield return new WaitForFixedUpdate();
        }

        _gui.m_ScaleIn.Began = false;
        _gui.m_ScaleIn.Ended = true;
    }


    IEnumerator ScaleOut(GUIAnimator _gui)
    {
        yield return new WaitForSeconds(_gui.m_ScaleOut.Delay);

        _gui.m_ScaleIn.Ended = false;
        _gui.m_ScaleOut.Began = true;
        _gui.m_ScaleOut.Ended = false;

        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_ScaleOut.Time;

            _gui.transform.localScale = Vector3.Lerp(_gui.m_ScaleOriginal, _gui.m_ScaleOut.Size, t);

            yield return new WaitForFixedUpdate();
        }
        _gui.transform.localScale = Vector3.zero;
        _gui.m_ScaleOut.Began = false;
        _gui.m_ScaleOut.Ended = true;
    }

    IEnumerator ScaleLoop(GUIAnimator _gui)
    {
        yield return new WaitForSeconds(_gui.m_ScaleLoop.Delay);

        _gui.m_ScaleLoop.Began = true;
        _gui.m_ScaleLoop.Ended = false;

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


    private bool CanAnimationOut(GUIAnimator.cAnim _in, GUIAnimator.cAnim _out)
    {
        // 둘다 실행이 안된 상태 - 이미 밖으로 나가있기 때문에 실행 할 필요가 없음
        if (!_in.Ended && !_out.Ended) return false;

        // 이미 시작한 상태
        if (_out.Began) return false;
        if (_out.Ended && !_in.Ended) return false;
        
        return true;
    }

    private bool CanAnimationIn(GUIAnimator.cAnim _in, GUIAnimator.cAnim _out)
    {
        if (_in.Began) return false;
        if (_in.Ended && !_out.Ended) return false;

        return true;
    }
}
