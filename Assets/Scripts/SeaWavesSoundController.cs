using UnityEngine;

public class SeaWavesSoundController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource waveAudio;
    public Transform playerCar;
    public Transform oceanPosition;

    [Header("Sound Settings")]
    public float maxDistance = 100f;
    [Range(0f, 1f)]
    public float maxVolume = 0.8f;

    void Start()
    {
        if (waveAudio != null)
        {
            waveAudio.loop = true;
            waveAudio.volume = 0f;
            waveAudio.Play();
        }
    }

    void Update()
    {
        if (waveAudio == null ||
            playerCar == null ||
            oceanPosition == null)
            return;

        float distance = Vector3.Distance(
            playerCar.position,
            oceanPosition.position
        );

        float volume = Mathf.Clamp01(
            1f - distance / maxDistance
        ) * maxVolume;

        waveAudio.volume = Mathf.Lerp(
            waveAudio.volume,
            volume,
            Time.deltaTime * 2f
        );
    }
}
