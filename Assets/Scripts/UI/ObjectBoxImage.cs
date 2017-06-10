using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBoxImage : MonoBehaviour
{
    public Sprite[] m_objSprites;
    public Image[] m_objImages;

    private int m_stageNum;

    void Start()
    {
        m_stageNum = StageInformation.m_stageNum;

        UpdateImage();
    }

    private void UpdateImage()
    {
        int stageNum = Mathf.Clamp((m_stageNum - 3) / 3, 0, 4);
        int spriteNum = stageNum * 6;
        for (int i = 0; i < m_objImages.Length; ++i)
        {
            m_objImages[i].sprite = m_objSprites[spriteNum + i];
        }
    }
}
