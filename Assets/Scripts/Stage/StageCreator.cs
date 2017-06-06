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

        StarCreate();

        if (map == null) return null;
        return map;
    }

    private void StarCreate()
    {
        GameObject starHouse = GameObject.Find("starHouse");
        if (starHouse == null) starHouse = new GameObject("starHouse");

        StageInformation.Stage stage = StageInformation.Instance.GetCurrentStage();

        int star_count = stage.star.Length;

        // 다 먹었으면 별을 생성하지 않아도 됨
        if (StageInformation.Instance.AllStarAte()) return;
        
        for (int i = 0; i < star_count; ++i)
        {
            Object star = Resources.Load("Prefabs/star");
            Instantiate(star, stage.star[i].position, ((GameObject)star).transform.rotation, starHouse.transform).name = i.ToString();
        }
    }
}
