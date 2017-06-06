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
    public float GetAudioVolume(int _index)
    {
        if (_index >= m_audioSourceCount) return 1;
        return m_audioSource[_index].volume;
    }
    public void SetAudioVolume(int _index, float _value)
    {
        if (_index >= m_audioSourceCount) return;
        m_audioSource[_index].volume = _value;
    }

    public AudioClip m_button_clip;
    public AudioClip[] m_bgm_clip;

    private bool m_isMute = false;

    void OnLevelWasLoaded()
    {
        GUIAnimSystem.Instance.AllButtonAddEvents(PlaySoundButton);
    }

    void Awake()
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

    public void PlayOneShot(AudioClip _audioClip)
    {
        if(m_audioSource[1].isPlaying == false)
            m_audioSource[1].PlayOneShot(_audioClip);
    }

    public void PlaySound(AudioClip _audioClip)
    {
        m_audioSource[1].PlayOneShot(_audioClip);
    }

    public void PlaySoundButton()
    {
        PlayOneShot(m_button_clip);
    }

    /// <summary>
    /// bgm 변경
    /// </summary>
    /// <param name="_musicNumber">원하는 브금 번호</param>
    public void BGMChange(int _musicNumber)
    {
        if (!CanChange(_musicNumber)) return;

        StartCoroutine(Change(_musicNumber));
    }

    private bool CanChange(int _number)
    {
        return 0 <= _number && _number < m_bgm_clip.Length;
    }

    private IEnumerator Change(int _musicNumber)
    {
        yield return FadeOutSound();

        if(m_audioSource[0].clip == m_bgm_clip[_musicNumber])
            m_audioSource[0].Pause();

        else
            m_audioSource[0].Stop();

        m_audioSource[0].clip = m_bgm_clip[_musicNumber];
        m_audioSource[0].Play();
        yield return FadeInSound();
    }

    private IEnumerator FadeOutSound()
    {
        float max_volume = PlayerPrefs.GetFloat("BGMVolume", 1);
        float volume = m_audioSource[0].volume;
        while (volume > 0)
        {
            volume = m_audioSource[0].volume - 0.1f;
            m_audioSource[0].volume = Mathf.Clamp(volume, 0f, max_volume);

            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator FadeInSound()
    {
        float max_volume = PlayerPrefs.GetFloat("BGMVolume", 1);
        float volume = m_audioSource[0].volume;
        while (volume < max_volume)
        {
            volume = m_audioSource[0].volume + 0.1f;
            m_audioSource[0].volume = Mathf.Clamp(volume, 0f, max_volume);

            yield return new WaitForSeconds(0.02f);
        }
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
