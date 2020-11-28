using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineHelpers
{
    public static void DelayedCall(this MonoBehaviour mono, float delay, Action action)
    {
        mono.StartCoroutine(DelayRoutine(delay, action));
    }

    private static IEnumerator DelayRoutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

}
