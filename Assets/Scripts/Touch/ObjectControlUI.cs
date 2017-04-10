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

    void Update()
    {
        PositionUpdate();
    }

    // 선택된 오브젝트의 위치로 UI 좌표 조정
    private void PositionUpdate()
    {
        if (!m_IsOn) return;

        Vector3 objPos = m_Object.transform.position;
        Vector3 worldToScreenPosition = Camera.main.WorldToScreenPoint(objPos);

        m_UI.position = worldToScreenPosition;
    }

    public void SetObject(GameObject _object)
    {
        m_Object = _object;

        OnControlUI();
    }

    private void OnControlUI()
    {
        if (m_Object == null)
        {
            m_IsOn = false;
            m_UI.gameObject.SetActive(false);
        }
        else
        {
            m_IsOn = true;
            m_UI.gameObject.SetActive(true);
            // slider 넣어주고 다른 오브젝트 누를 때 이전 오브젝트의 slider 를 없에줘야함.
            // slider 사이즈가 너무 작음, UI 켜졌을 때 카메라 고정 시키기
            m_Object.GetComponent<Objects>().SetSlider(m_PowerBar, m_RotateBar);
        }
    }
}
