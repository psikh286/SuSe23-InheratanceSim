using System;
using System.Collections;
using UnityEngine;


public class TickManager : MonoBehaviour
{
    public static Action OnTick;
    public const float TickDelay = 0.5f;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(TickDelay);
            OnTick?.Invoke();
        }
    }

    public void Test()
    {
        OnTick?.Invoke();
    }
}
