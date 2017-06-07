using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoricObjectController : MonoBehaviour
{
    public HistoricObject[] m_objects;

    void FixedUpdate()
    {
        for (int i = 0; i < m_objects.Length; ++i)
        {
            RotateAround(m_objects[i]);
        }
    }

    private void RotateAround(HistoricObject _object)
    {
        _object.Object.RotateAround(_object.LookObject.position, _object.LookObject.up, Time.deltaTime * _object.Speed);
    }


    [System.Serializable]
    public class HistoricObject
    {
        public Transform Object;
        public Transform LookObject;
        public float Speed;
    }
}
