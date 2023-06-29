using System;
using System.Collections;
using UnityEngine;


public class TickManager : MonoBehaviour
{
    public static Action OnTick;
    public const float TickInterval = 0.05f;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(TickInterval);
            //OnTick?.Invoke();
        }
    }

    private float _elapsedTickTime;
    private void Update()
    {
        _elapsedTickTime += Time.deltaTime;
        while (_elapsedTickTime >= TickInterval)
        {
            OnTick?.Invoke();
            _elapsedTickTime -= TickInterval;
        }
    }

    public void Test()
    {
        OnTick?.Invoke();
    }
}
