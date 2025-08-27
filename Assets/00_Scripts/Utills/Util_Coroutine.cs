using System;
using UnityEngine;
using System.Collections;

public class Util_Coroutine
{
    public static IEnumerator Delay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public static IEnumerator ParabolaMove(Transform obj, Vector3 start, Vector3 end, float height, float duration, Action action = null)
    {
        float time = 0.0f;
        while(time < duration)
        {
            float t = time / duration;

            // 수평 보간
            Vector3 flatPos = Vector3.Lerp(start, end, t);

            // 위로 솟는 y값 (포물선)
            float y = Mathf.Sin(Mathf.PI * t) * height;

            obj.position = new Vector3(flatPos.x, flatPos.y + y, flatPos.z);

            time += Time.deltaTime; 
            yield return null; // 다음 프레임까지 대기
        }

        if(action != null)
        {
            action?.Invoke(); // 완료 후 액션 호출
        }
        // 마지막 위치 설정

        obj.position = end; // 최종 위치 설정
    }
}

