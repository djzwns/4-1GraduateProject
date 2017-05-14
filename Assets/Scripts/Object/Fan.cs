using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public FanBody m_body;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag != "ball") return;

        m_body.Blow(coll.gameObject);
    }
}
