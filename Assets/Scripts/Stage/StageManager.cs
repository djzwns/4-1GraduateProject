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

    public delegate void GameClearHandler();
    public GameClearHandler gameclear_callback;

    // 맵이 생성 될 때 호출
    public delegate void StageInitializeHandler();
    public StageInitializeHandler stageinit_callback;

    // 맵이 생성 된 후 호출
    public delegate void StageCreateCompleteHandler();
    public StageCreateCompleteHandler stagecreated_callback;

    void Awake()
    {
        string current_stage = StageInformation.Instance.GetCurrentStage().name;
        m_stageCreator = gameObject.AddComponent<StageCreator>();

        // 스테이지 생성
        MapCreate(current_stage);

        m_GameObjectList = new List<GameObject>();

        gameclear_callback += Clear;
    }

    public bool NextStage()
    {
        string nextStage = StageInformation.Instance.GetNextStage();

        // 마지막 맵이면 아무것도 ..
        if (nextStage.Equals("end")) return false;
        m_IsClear = false;

        if (gameclear_callback != null)
            gameclear_callback();

        GameObject destroyObject = m_Map;
        destroyObject.SetActive(false);
        Destroy(destroyObject);

        MapCreate(nextStage);

        
        return true;
    }

    private void MapCreate(string _mapName)
    {
        //stageinit_callback();

        LoadStageInfo(_mapName);
        m_Map = m_stageCreator.Create(_mapName);
    }

    public void Save()
    {
        StageInformation.Instance.StarReset();
        m_GameObjectList.ForEach(go =>
       {
           if (go.GetComponent<Star>() == null) return;
           int star_num = 0;
           int.TryParse(go.name, out star_num);
           StageInformation.Instance.SetStarAte(star_num, true);
       });

        StageInformation.Instance.RenewalStage();
        StageInformation.Instance.Save();
    }

    /// <summary>
    /// 스테이지 정보 불러오기
    /// </summary>
    /// <param name="_fileName"></param>
    private void LoadStageInfo(string _fileName)
    {
        string json = FileReadWrite.Read(_fileName);

        if (json == null) { return; }

        StageInformation.Stage stage_data;
        stage_data = JsonUtility.FromJson<StageInformation.Stage>(json);
        StageInformation.Instance.SetStage(stage_data);
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

    private void Clear()
    {
        Star[] star = GameObject.Find("starHouse").GetComponentsInChildren<Star>(includeInactive: true);
        for (int i = 0; i < star.Length; ++i) Destroy(star[i].gameObject);
        m_GameObjectList.Clear();
    }
}
