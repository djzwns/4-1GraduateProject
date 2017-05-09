using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : Singleton<ObjectCreator> {
    public GameObject m_parent;

    public GameObject CreateObject(string _objName)
    {
        GameObject newObject = Instantiate(Resources.Load("Prefabs/" + _objName), m_parent.transform) as GameObject; //) as GameObject;

        return newObject;
    }
}
