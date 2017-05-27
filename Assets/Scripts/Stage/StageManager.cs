using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 관리. 
/// 스테이지를 생성하고 시작할 수 있도록 해줌.
/// </summary>
public class StageManager : Singleton<StageManager>
{
    private GameObject m_Map;
    private StageCreator m_stageCreator;
    private List<GameObject> m_GameObjectList;

    public bool m_IsClear { get; set; }

    void Awake()
    {
        m_stageCreator = gameObject.AddComponent<StageCreator>();
        // 스테이지 생성
        m_Map = m_stageCreator.Create(StageInformation.Instance.GetCurrentStage());
        m_GameObjectList = new List<GameObject>();
    }

    public bool NextStage()
    {
        string nextStage = StageInformation.Instance.GetNextStage();

        // 마지막 맵이면 아무것도 ..
        if (nextStage.Equals("end")) return false;
        m_IsClear = false;

        GameObject destroyObject = m_Map;
        destroyObject.SetActive(false);
        Destroy(destroyObject);

        m_Map = m_stageCreator.Create(nextStage);

        return true;
    }

    public void Save()
    {
        if (StageInformation.Instance.RenewalStage())
            PlayerPrefs.SetInt("RB_LastStage", StageInformation.m_lastStage);
    }


    /// <summary>
    /// 오브젝트 사용, 미사용 설정
    /// </summary>
    /// <param name="_object"></param>
    /// <param name="_enable"></param>
    /// <param name="_disableTime"></param>
    /// <returns></returns>
    public IEnumerator EnableGameObject(GameObject _object, bool _enable, float _disableTime = 0)
    {
        yield return new WaitForSeconds(_disableTime);
        m_GameObjectList.Add(_object);

        _object.SetActive(_enable);
    }

    /// <summary>
    /// 스테이지에서 재설정이 필요한 오브젝트들을 리셋 시킴.
    /// </summary>
    public void ResetStageObject()
    {
        m_GameObjectList.ForEach(go => { go.SetActive(true); });
        m_GameObjectList.Clear();
    }
}
