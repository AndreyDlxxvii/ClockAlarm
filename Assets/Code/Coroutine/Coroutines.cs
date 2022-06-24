using System.Collections;
using UnityEngine;


public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines _instance;
    private static Coroutines instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("[Coroutine manager]");
                _instance = go.AddComponent<Coroutines>();
            }

            return _instance;
        }
    }
    
    public static Coroutine StarRoutine(IEnumerator enumerator)
    {
        return instance.StartCoroutine(enumerator);
    }
    
    public static void StopRoutine (Coroutine routine)
    {
        if (routine != null)
        {
            instance.StopCoroutine(routine);
        }
    }
}