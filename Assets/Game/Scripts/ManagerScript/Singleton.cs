using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _mIns;

    public static T Ins
    {
        get
        {
            if (_mIns != null) return _mIns;
            // Find singleton
            _mIns = FindObjectOfType<T>();

            // Create new instance if one doesn't already exist.
            if (_mIns != null) return _mIns;
            // Need to create a new GameObject to attach the singleton to.
            GameObject singletonObject = new();
            _mIns = singletonObject.AddComponent<T>();
            singletonObject.name = typeof(T) + " (Singleton)";
            return _mIns;
        }
    }

}
