using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var o = new GameObject() { name = $"{typeof(T)}_Singleton" };
                _instance = o.AddComponent<T>();
            }

            lock (_instance)
            {
                return _instance;
            }
        }
    }
}
