using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JuiceHelper : MonoBehaviour
{
    public static void ScreenShake(float time, float strength, int vibrato)
    {
        Camera.main.DOShakePosition(time, strength, vibrato).SetUpdate(true);
    }

    public static void FreezeFrame(float time)
    {
        Time.timeScale = 0f;

        Camera.main.transform.DOMove(Vector3.zero, time).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
        });
    }
}
