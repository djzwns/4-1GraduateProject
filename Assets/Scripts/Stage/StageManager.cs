using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Borodar.FarlandSkies.LowPoly;

/// <summary>
/// 스테이지 관리. 
/// 스테이지를 생성하고 시작할 수 있도록 해줌.
/// </summary>
public class StageManager : Singleton<StageManager>
{
    private GameObject m_Map;
    private float[] m_time = { 60, 75, 100 };

    private StageCreator m_stageCreator;
    private List<GameObject> m_GameObjectList;

    public bool m_IsClear { get; set; }

    public delegate void StageClearHandler();
    public StageClearHandler stageclear_callback;

    void Awake()
    {
        SceneManager.Instance.TimeOfDay = m_time[StageInformation.m_stageNum % 3];

        string current_stage = StageInformation.Instance.GetCurrentStage().name;
        m_stageCreator = gameObject.AddComponent<StageCreator>();

        // 스테이지 생성
        MapCreate(current_stage);

        m_GameObjectList = new List<GameObject>();        
    }

    void Update()
    {
        if (!m_IsClear) return;

        if (stageclear_callback != null)
            stageclear_callback();

        m_IsClear = false;
    }

    /// <summary>
    /// 다음 맵 준비
    /// </summary>
    private void LoadNextStage()
    {
        string nextStage = StageInformation.Instance.GetNextStage();

        // 마지막 맵이면 아무것도 ..
        if (nextStage.Equals("end")) return;
        LoadStage(nextStage);
    }

    /// <summary>
    /// 다음 스테이지 버튼 클릭시 실행
    /// </summary>
    public void NextStage()
    {
        LoadNextStage();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Travel");
    }

    private void MapCreate(string _mapName)
    {
        LoadStage(_mapName);
        m_Map = m_stageCreator.Create(_mapName);
    }

    /// <summary>
    /// 스테이지정보 클래스 갱신
    /// </summary>
    /// <param name="_mapName"></param>
    private void LoadStage(string _mapName)
    {
        StageInformation.Stage stage = StageInformation.LoadStageInfo(_mapName);
        StageInformation.Instance.SetStage(stage);
    }

    public void Save()
    {
        StageInformation.Instance.StarReset();
        int ateStarCount = 0;
        m_GameObjectList.ForEach(go =>
       {
           if (go.GetComponent<Star>() == null) return;
           ++ateStarCount;
           int star_num = 0;
           int.TryParse(go.name, out star_num);
           StageInformation.Instance.SetStarAte(star_num, true);
       });

        StageInformation.Instance.RenewalStage();
        if(ateStarCount != 0)
            StageInformation.Instance.Save();
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
