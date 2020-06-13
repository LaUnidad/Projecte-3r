using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool waitingHitStop;

    public void HitStopLoL(float duration)
    {
        if (waitingHitStop)
            return;
        Debug.Log("Hit Stop");
        Time.timeScale = 0f;
        StartCoroutine(WaitHitStop(duration));
    }

    IEnumerator WaitHitStop(float duration)
    {
        waitingHitStop = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        waitingHitStop = false;
    }
}
