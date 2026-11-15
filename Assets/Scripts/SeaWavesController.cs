using UnityEngine;

public class SeaWavesController : MonoBehaviour
{
    [Header("Ocean Movement")]
    public Transform waterSurface;
    public float waveSpeed = 0.5f;
    public float waveHeight = 0.15f;

    [Header("Sea Waves Sound")]
    public AudioSource waveAudio;
    public Transform playerCar;
    public float maxDistance = 100f;

    [Range(0f, 1f)]
    public float maxVolume = 0.8f;

    private Vector3 startPosition;

    void Start()
    {
        if (waterSurface != null)
            startPosition = waterSurface.localPosition;

        if (waveAudio != null)
        {
            waveAudio.loop = true;
            waveAudio.volume = 0f;
            waveAudio.Play();
        }
    }

    void Update()
    {
        AnimateWaves();
        UpdateWaveSound();
    }

    void AnimateWaves()
    {
        if (waterSurface == null)
            return;

        Vector3 position = startPosition;

        position.y += Mathf.Sin(
            Time.time * waveSpeed
        ) * waveHeight;

        waterSurface.localPosition = position;
    }

    void UpdateWaveSound()
    {
        if (waveAudio == null || playerCar == null)
            return;

        float distance = Vector3.Distance(
            playerCar.position,
            waterSurface != null
                ? waterSurface.position
                : transform.position
        );

        float targetVolume =
            Mathf.Clamp01(1f - distance / maxDistance)
            * maxVolume;

        waveAudio.volume = Mathf.Lerp(
            waveAudio.volume,
            targetVolume,
            Time.deltaTime * 2f
        );
    }
}
