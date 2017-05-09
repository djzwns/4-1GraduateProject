using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            if (FindObjectsOfType(typeof(T)).Length > 1)
            {
                Debug.LogError("싱글톤은 하나보다 많을 수 없음.");
                return null;
            }
            else
            {
                _instance = (T)FindObjectOfType(typeof(T));
                return _instance;
            }
        }
    }
}
