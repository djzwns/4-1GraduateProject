using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWall : Objects {

    public PhysicsMaterial2D m_Physics;

    void Start()
    {
        m_Physics = gameObject.GetComponent<Collider2D>().sharedMaterial;
    }

    void Update()
    {
        SetBounciness();
    }

    private void SetBounciness()
    {
        if (m_PowerBar == null) return;
        Debug.Log(m_PowerBar.value);
        m_Physics.bounciness = m_PowerBar.value;
    }
}
