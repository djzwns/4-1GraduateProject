using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    #region Singleton
    private static BGMManager _instance;

    public static BGMManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go =  Instantiate(Resources.Load("Prefabs/-- BGMManager --")) as GameObject;
                _instance = go.GetComponent<BGMManager>();
                return _instance;
            }

            return _instance;
        }
    }
    #endregion //Singleton

    public int m_audioSourceCount = 2;
    private AudioSource[] m_audioSource;

    public AudioClip m_button_clip;
    public AudioClip[] m_bgm_clip;

    private bool m_isMute = false;

    void OnLevelWasLoaded()
    {
        GUIAnimSystem.Instance.AllButtonAddEvents(PlaySoundButton);
    }

    void Start()
    {
        GUIAnimSystem.Instance.AllButtonAddEvents(PlaySoundButton);
        DontDestroyOnLoad(gameObject);

        if (m_audioSource == null)
        {
            m_audioSource = new AudioSource[m_audioSourceCount];

            for (int i = 0; i < m_audioSourceCount; ++i)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                m_audioSource[i] = audioSource;
            }
        }

        m_audioSource[0].loop = true;
        m_audioSource[0].clip = m_bgm_clip[0];
        m_audioSource[0].Play();
    }

    void PlayOneShot(AudioClip _audioClip)
    {
        if(m_audioSource[1].isPlaying == false)
            m_audioSource[1].PlayOneShot(_audioClip);
    }

    public void PlaySoundButton()
    {
        PlayOneShot(m_button_clip);
    }

    public void BGMChange(int _musicNumber)
    {
        if (_musicNumber == -1) return;
        // 페이드 효과 추가 예정...
        m_audioSource[0].Stop();
        m_audioSource[0].clip = m_bgm_clip[_musicNumber];
        m_audioSource[0].Play();
    }

    private void Mute()
    {
        m_isMute = true;
        for (int i = 0; i < m_audioSourceCount; ++i)
        {
            m_audioSource[i].volume = 0;
        }
    }

    private void UnMute()
    {
        m_isMute = false;
        for (int i = 0; i < m_audioSourceCount; ++i)
        {
            m_audioSource[i].volume = 1;
        }
    }

    public void ToggleMute()
    {
        if (m_isMute)
            UnMute();
        else
            Mute();
    }
}
