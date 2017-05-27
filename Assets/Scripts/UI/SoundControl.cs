using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public Sprite[] m_HandlerImage;

    public Slider[] m_SoundBars;
    private BGMManager m_bgm;

    // PlayerPrefs 에 사용할 키값
    private string[] m_sound = { "BGMVolume", "FXSoundVolume" };
    

    void Start()
    {
        m_bgm = BGMManager.Instance;

        SoundInitialize();
    }

    // 사운드 초기화
    private void SoundInitialize()
    {
        for (int i = 0; i < m_SoundBars.Length; ++i)
        {
            SoundSettingLoad(i);
        }
    }

    // 사운드 설정값 불러옴
    private void SoundSettingLoad(int _index)
    {
        float value;

        // 데이터 불러오기
        value = PlayerPrefs.GetFloat(m_sound[_index], m_bgm.GetAudioVolume(_index));
        
        // bgm 매니저를 통해 볼륨 조절
        m_bgm.SetAudioVolume(_index, value);

        if (m_SoundBars[0] == null) return;

        // 불러온 값을 슬라이더에 적용
        m_SoundBars[_index].value = (int)(value * 10f);
        
        // 슬라이더에 콜백함수 등록
        m_SoundBars[_index].onValueChanged.AddListener(delegate { ValueChanged(_index); });

        if (value > 0) return;

        m_SoundBars[_index].handleRect.GetComponent<Image>().sprite = m_HandlerImage[0];
    }

    // 변한 값을 기억하고 적용시킨다.
    private void ValueChanged(int _index)
    {
        float value = m_SoundBars[_index].value;

        m_bgm.SetAudioVolume(_index, value * 0.1f);
        PlayerPrefs.SetFloat(m_sound[_index], m_bgm.GetAudioVolume(_index));

        // 스프라이트 변환
        if (value > 0)
            m_SoundBars[_index].handleRect.GetComponent<Image>().sprite = m_HandlerImage[1];
        else
            m_SoundBars[_index].handleRect.GetComponent<Image>().sprite = m_HandlerImage[0];
    }
}
