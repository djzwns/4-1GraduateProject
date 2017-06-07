using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float m_amplitude;
    public float m_speed;
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.position += m_amplitude * (Vector3.up * Mathf.Sin(Time.time * m_speed));
    }
}
