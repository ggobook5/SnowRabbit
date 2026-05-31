using System.Collections;
using UnityEngine;

public class UpDownSpike : MonoBehaviour
{
    public float moveDistance = 1.2f;
    public float upTime = 0.5f;
    public float downTime = 1f;
    public float waitTime = 1f;
    private float defaultY;
    private bool isUp = true;
    private float timeCount = 0f;

    private WaitForSecondsRealtime wait;

    private void Start()
    {
        defaultY = transform.position.y;
        wait = new WaitForSecondsRealtime(waitTime);
        StartCoroutine(UpDown());
    }

    private IEnumerator UpDown()
    {
        float timeRatio;
        float targetY;

        while (true)
        {
            timeCount += Time.deltaTime;

            if (isUp)
            {
                timeCount = Mathf.Clamp(timeCount, 0f, upTime);
                timeRatio = timeCount / upTime;
                targetY = Mathf.Lerp(0f, moveDistance, timeRatio);
            }
            else
            {
                timeCount = Mathf.Clamp(timeCount, 0f, downTime);
                timeRatio = timeCount / downTime;
                targetY = Mathf.Lerp(moveDistance, 0f, timeRatio);
            }

            transform.position = new Vector2(transform.position.x, defaultY + targetY);

            if (targetY.Equals(0f) || targetY.Equals(moveDistance))
            {
                yield return wait;

                timeCount = 0;
                isUp = !isUp;
            }

            yield return null;
        }
    }
}
