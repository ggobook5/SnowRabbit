using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(ParticleSystem))]
[DisallowMultipleComponent]
public class VFXControl : MonoBehaviour
{
    // ===============================
    // 옵션
    // ===============================
    public enum ClearBehavior
    {
        None,
        Disable,
        Destroy
    }

    public static bool GlobalDisableCameraShake;
    public static bool GlobalDisableLights;

    [Header("Clear")]
    public ClearBehavior clearBehavior = ClearBehavior.Destroy;

    [Header("Camera Shake")]
    public CameraShake cameraShake;

    [Header("Light")]
    public AnimatedLight[] animatedLights;
    public ParticleSystem fadeOutReference;

    float time;
    ParticleSystem rootParticleSystem;

    bool isFadingOut;
    float fadingOutStartTime;

    // ===============================
    void Awake()
    {
        rootParticleSystem = GetComponent<ParticleSystem>();

        if (cameraShake != null && cameraShake.enabled)
        {
            cameraShake.FetchCameras();
        }
    }

    void OnEnable()
    {
        if (animatedLights != null)
        {
            foreach (var l in animatedLights)
            {
                if (l.light != null)
                    l.light.enabled = !GlobalDisableLights;
            }
        }
    }

    void OnDisable()
    {
        ResetState();
    }

    // ===============================
    void Update()
    {
        time += Time.deltaTime;

        Animate(time);

        // Fade Out
        if (fadeOutReference != null &&
            !fadeOutReference.isEmitting &&
            (fadeOutReference.isPlaying || isFadingOut))
        {
            FadeOut(time);
        }

        // Clear
        if (clearBehavior != ClearBehavior.None)
        {
            if (!rootParticleSystem.IsAlive(true))
            {
                if (clearBehavior == ClearBehavior.Destroy)
                    Destroy(gameObject);
                else
                    gameObject.SetActive(false);
            }
        }
    }

    // ===============================
    public void Animate(float t)
    {
        if (animatedLights != null && !GlobalDisableLights)
        {
            foreach (var l in animatedLights)
                l.Animate(t);
        }

        if (cameraShake != null && cameraShake.enabled && !GlobalDisableCameraShake)
        {
            cameraShake.Animate(t);
        }
    }

    public void FadeOut(float t)
    {
        if (!isFadingOut)
        {
            isFadingOut = true;
            fadingOutStartTime = t;
        }

        foreach (var l in animatedLights)
            l.AnimateFadeOut(t - fadingOutStartTime);
    }

    public void ResetState()
    {
        time = 0f;
        isFadingOut = false;

        if (animatedLights != null)
        {
            foreach (var l in animatedLights)
                l.Reset();
        }

        if (cameraShake != null)
            cameraShake.StopShake();
    }

    // ===============================
    //  Light 클래스
    // ===============================
    [System.Serializable]
    public class AnimatedLight
    {
        public Light light;

        public bool animateIntensity = true;
        public float intensityStart = 5f;
        public float intensityEnd = 0f;
        public float duration = 0.5f;

        public void Animate(float t)
        {
            if (light == null) return;

            float delta = Mathf.Clamp01(t / duration);
            light.intensity = Mathf.Lerp(intensityStart, intensityEnd, delta);
        }

        public void AnimateFadeOut(float t)
        {
            if (light == null) return;
            light.intensity *= 1f - Mathf.Clamp01(t / duration);
        }

        public void Reset()
        {
            if (light != null)
                light.intensity = intensityStart;
        }
    }
}