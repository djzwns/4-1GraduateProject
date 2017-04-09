using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIAnimSystem : Singleton<GUIAnimSystem> {

    [Range(0.5f, 10f)]
    public float m_GUISpeed;

    public void LoadLevel(string _LevelName, float _delay = 0)
    {
        StartCoroutine(DelayLoadLevel(_LevelName, _delay));
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

    // UI 에 지정된 모든 애니메이션을 실행시킴
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
        }        
    }

    public Canvas GetCanvas(Transform _trans)
    {
        return _trans.root.GetComponent<Canvas>();
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

        _gui.m_MoveIn.Animating = true;

        float t = 0f;
        while (!_gui.m_MoveIn.Done)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_MoveIn.Time;

            _gui.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(_gui.transform.localPosition, _gui.m_MoveIn.EndPos, t);

            if (t > 1)
            {
                _gui.m_MoveOut.Done = false;
                _gui.m_MoveIn.Done = true;
            }
            
            yield return new WaitForFixedUpdate();
        }

        _gui.m_MoveIn.Animating = false;
    }

    IEnumerator MoveOut(GUIAnimator _gui)
    {
        // 지연시간 지난 후 실행
        yield return new WaitForSeconds(_gui.m_MoveOut.Delay);

        _gui.m_MoveOut.Animating = true;

        float t = 0f;
        while (!_gui.m_MoveOut.Done)
        {
            t += Time.deltaTime * m_GUISpeed / _gui.m_MoveOut.Time;

            _gui.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(_gui.transform.localPosition, _gui.m_MoveOut.EndPos, t);

            if (t > 1)
            {
                _gui.m_MoveIn.Done = false;
                _gui.m_MoveOut.Done = true;
            }
            
            yield return new WaitForFixedUpdate();
        }

        _gui.m_MoveOut.Animating = false;
    }
}
