using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Transform[] m_fans;
    
	void FixedUpdate ()
    {
        for (int i = 0; i < m_fans.Length; ++i)
        {
            Rotate(m_fans[i]);
        }
	}

    private void Rotate(Transform _trans)
    {
        _trans.Rotate(Vector3.left * 10f);
    }
}
