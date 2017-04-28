using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public float m_gizmoSize = 0.75f;
    public Color m_gizmoColor = Color.yellow;

    void OnDrawGizmos()
    {
        Gizmos.color = m_gizmoColor;
        Gizmos.DrawWireSphere(transform.position, m_gizmoSize);
    }
}
