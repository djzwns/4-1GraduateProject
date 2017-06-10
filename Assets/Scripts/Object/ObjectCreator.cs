using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : Singleton<ObjectCreator> {
    public GameObject m_parent;

    private StageInformation m_stage;

    void Start()
    {
        m_stage = StageInformation.Instance;
    }

    char delimiter = '-';
    public GameObject CreateObject(string _objName)
    {
        string[] splitName = m_stage.GetCurrentStage().name.Split(delimiter);
        if (splitName[0].Equals("Tutorial")) splitName[0] = "stage1";

        string path = "Prefabs/" + splitName[0] + "/" + _objName;
        GameObject newObject = Instantiate(Resources.Load(path), m_parent.transform) as GameObject; //) as GameObject;

        return newObject;
    }
}
