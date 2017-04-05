using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : Singleton<ObjectCreator> {

    public GameObject CreateObject(string _objName)
    {
        GameObject newObject = Instantiate(Resources.Load("Prefabs/" + _objName), Input.GetTouch(0).position, Quaternion.identity) as GameObject;

        return newObject;
    }
}
