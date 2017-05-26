using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : MonoBehaviour {

    /// <summary>
    /// 맵 생성 후 반환
    /// </summary>
    /// <param name="_stageName"></param>
    /// <returns></returns>
    public GameObject Create(string _stageName)
    {
        GameObject map = Instantiate(Resources.Load("Prefabs/Stage/" + _stageName), Vector3.zero, Quaternion.identity) as GameObject;

        if (map == null) return null;
        return map;
    }
}
