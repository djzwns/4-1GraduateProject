using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHall : MonoBehaviour
{
    public Transform m_whiteHall;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;

        coll.transform.position = m_whiteHall.position;
    }
}
