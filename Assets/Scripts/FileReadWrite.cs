using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 파일을 불러오는 클래스
/// </summary>
public static class FileReadWrite 
{
    private static string m_devicePath;

    /// <summary>
    /// 읽은 파일 반환
    /// </summary>
    /// <param name="_fileName"></param>
    /// <returns></returns>
    public static string Read(string _fileName)
    {
        m_devicePath = Application.persistentDataPath;
        string filePath = m_devicePath + "/" + _fileName + ".json";
        
        string file = null;
        if (File.Exists(filePath))
        {
            file = File.ReadAllText(filePath);
            return file;
        }

        TextAsset txt = Resources.Load("MapData/" + _fileName) as TextAsset;

        if (txt != null)
        {
            file = txt.text;
        }

        return file;
    }

    /// <summary>
    /// 파일을 쓴다.
    /// </summary>
    /// <param name="_fileName">저장할 파일 이름</param>
    /// <param name="_contents">저장할 내용</param>
    public static void Write(string _fileName, string _contents)
    {
        m_devicePath = Application.persistentDataPath;
        string filePath = m_devicePath + "/";// + "/Resources/MapData/";
        File.WriteAllText(filePath + _fileName + ".json", _contents);
    }
}
