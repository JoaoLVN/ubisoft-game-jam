using UnityEngine;
using DG.Tweening;

public class JuiceHelper : MonoBehaviour
{
    public static void ScreenShake(float time, float strength, int vibrato)
    {
        Camera.main.transform.parent.DOShakePosition(time, new Vector3(strength, strength, 0f), vibrato).SetUpdate(true);
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
