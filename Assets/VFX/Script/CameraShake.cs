using UnityEngine;
using Random = UnityEngine.Random; // Random 충돌 해결

public class CameraShake : MonoBehaviour
{
    public float strength = 0.2f;
    public float duration = 0.3f;

    private Vector3 originalPos;
    private float time;

    private Camera cam;

    // 카메라 가져오기
    public void FetchCameras()
    {
        cam = Camera.main;

        if (cam != null)
            originalPos = cam.transform.localPosition;
    }

    // 흔들림 실행
    public void Animate(float t)
    {
        if (cam == null) return;

        time += Time.deltaTime;

        if (time < duration)
        {
            cam.transform.localPosition = originalPos + Random.insideUnitSphere * strength;
        }
        else
        {
            cam.transform.localPosition = originalPos;
        }
    }

    // 흔들림 종료
    public void StopShake()
    {
        if (cam != null)
            cam.transform.localPosition = originalPos;

        time = 0f;
    }
}