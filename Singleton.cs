using UnityEngine;
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (Application.isPlaying == false)
            {
#if DEBUG
                Debug.LogError($"Singleton <{typeof(T)}> cannot be returned, because the application is not running.");
#endif
                return null;
            }

            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T).Name + " (Singleton)";
            }

            if (instance != null)
            {
                Debug.LogWarning($"Singleton <{typeof(T)}> instance is already created!");
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("duplicating" + typeof(T) + " component");
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}