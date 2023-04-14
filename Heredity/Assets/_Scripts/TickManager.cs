using System;
using UnityEngine;


public class TickManager : MonoBehaviour
{
    public static Action OnTick;

    public void Test()
    {
        OnTick?.Invoke();
    }
}
