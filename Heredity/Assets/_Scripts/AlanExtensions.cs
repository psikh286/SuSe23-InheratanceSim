using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public static class AlanExtensions
{
    #region LerpTo
    // ReSharper disable Unity.PerformanceAnalysis
    public static IEnumerator LerpTo(Action<float> result, float from, float to, float delta, bool unscaledTime = false, AnimationCurve curve = null)
    {
        var current = 0f;
        
        curve ??= AnimationCurve.Linear(0, 0, 1, 1);

        while (current < 1)
        {
            current = Mathf.MoveTowards(current, 1, unscaledTime ? delta * Time.unscaledDeltaTime : delta * Time.deltaTime);
            var newValue = Mathf.Lerp(from, to, curve.Evaluate(current));
            result(newValue);

            yield return null;
        }
    }
    public static IEnumerator LerpTo(Action<Vector3> result, Vector3 from, Vector3 to, float delta, bool unscaledTime = false, AnimationCurve curve = null)
    {
        var current = 0f;

        curve ??= AnimationCurve.Linear(0, 0, 1, 1);

        while (current < 1)
        {
            current = Mathf.MoveTowards(current, 1, unscaledTime ? delta * Time.unscaledDeltaTime : delta * Time.deltaTime);
            var newValue = Vector3.Lerp(from, to, curve.Evaluate(current));
            result(newValue);
            
            yield return null;
        }
    }
    public static IEnumerator LerpTo(Action<Quaternion> result, Quaternion from, Quaternion to, float delta, bool unscaledTime = false, AnimationCurve curve = null)
    {
        var current = 0f;

        curve ??= AnimationCurve.Linear(0, 0, 1, 1);

        while (current < 1)
        {
            current = Mathf.MoveTowards(current, 1, unscaledTime ? delta * Time.unscaledDeltaTime : delta * Time.deltaTime);
            var newValue = Quaternion.Lerp(from, to, curve.Evaluate(current));
            result(newValue);
            
            yield return null;
        }
    }
    public static IEnumerator LerpTo(Action<Quaternion> result, Vector3 from, Vector3 to, float delta, bool unscaledTime = false, AnimationCurve curve = null)
    {
        var current = 0f;
        var qFrom = Quaternion.Euler(from);
        var qTo = Quaternion.Euler(to);

        curve ??= AnimationCurve.Linear(0, 0, 1, 1);

        while (current < 1)
        {
            current = Mathf.MoveTowards(current, 1, unscaledTime ? delta * Time.unscaledDeltaTime : delta * Time.deltaTime);
            var newValue = Quaternion.Lerp(qFrom, qTo, curve.Evaluate(current));
            result(newValue);
            
            yield return null;
        }
    }
    #endregion

    #region Shuffle
    private static readonly Random rng = new();
    public static void Shuffle<T>(this IList<T> list)  
    {
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }
    #endregion

    #region Find And Get
    
    public static T FindOfType<T>() where T : class
    {
        return Object.FindObjectsOfType<MonoBehaviour>().OfType<T>().Select(monoBehaviour => monoBehaviour as T).FirstOrDefault();
    }

    public static List<Transform> GetAllChildren(this Transform parent)
    {
        List<Transform> result = new();
        for (int i = 0; i < parent.childCount; i++)
        {
            result.Add(parent.GetChild(i));
        }
        return result;
    }

    public static T GetComponentInSibling<T>(this Transform self)
    {
        return self.parent.GetComponentInChildren<T>();
    }
    
    public static T[] GetComponentsInSibling<T>(this Transform self)
    {
        return self.parent.GetComponentsInChildren<T>();
    }

    public static int GetKeyIndex<T, T1>(this Dictionary<T, T1> dic, T key)
    {
        return dic.Keys.ToList().IndexOf(key);
    }
    
    #endregion
}
