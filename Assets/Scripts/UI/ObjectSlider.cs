using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSlider : MonoBehaviour
{
    public Slider m_power;
    public Slider m_rotate;

    void Start()
    {
        m_rotate.onValueChanged.AddListener(delegate { PowerRotate(); });
    }

    /// <summary>
    /// 회전 슬라이더가 값이 바뀌면 호출.
    /// 파워 슬라이더를 회전 시킨다.
    /// </summary>
    void PowerRotate()
    {
        m_power.transform.rotation = Quaternion.AngleAxis(m_rotate.value, Vector3.back);
    }
}
