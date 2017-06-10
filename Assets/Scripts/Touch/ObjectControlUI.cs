using UnityEngine;
using UnityEngine.UI;

public class ObjectControlUI : Singleton<ObjectControlUI> {

    public RectTransform m_UI;
    public Slider m_PowerBar;
    public Slider m_RotateBar;

    private bool m_IsOn;
    private GameObject m_Object;    // 선택된 오브젝트

    void Awake()
    {
        m_UI.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        PositionUpdate();
    }

    // 선택된 오브젝트의 위치로 UI 좌표 조정
    private void PositionUpdate()
    {
        if (!m_IsOn) return;
        if (m_Object == null) return;

        Vector3 objPos = m_Object.transform.position;
        Vector3 worldToScreenPosition = CameraManager.Instance.WorldToScreenPosition(objPos);

        m_UI.position = worldToScreenPosition;
    }

    public void SetObject(GameObject _object)
    {
        if (m_Object != null)
        {
            m_UI.gameObject.SetActive(false);
            m_Object.GetComponent<Objects>().SetSlider();
        }
        m_Object = _object;

        OnControlUI();
    }

    private void OnControlUI()
    {
        if (m_Object == null)
        {
            m_IsOn = false;
            m_UI.gameObject.SetActive(false);
            return;
        }

        if (m_Object.GetComponent<Objects>().m_UIEnable)
        {
            m_IsOn = true;
            m_UI.gameObject.SetActive(true);
            
            Objects obj = m_Object.GetComponent<Objects>();
            obj.SetSlider(m_PowerBar, m_RotateBar);
            if (!obj.m_PowerEnable) m_PowerBar.interactable = false;
            else m_PowerBar.interactable = true;
        }
    }
}
