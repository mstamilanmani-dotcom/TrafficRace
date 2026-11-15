using UnityEngine;

public class BeachOceanAnimator : MonoBehaviour
{
    [Header("Ocean")]
    public Renderer oceanRenderer;
    public float textureScrollSpeed = 0.025f;

    [Header("Wave Settings")]
    public float shorelineX = 29f;
    public float waveSpacing = 3.2f;
    public int waveCount = 3;
    public float waveAmplitude = 0.8f;
    public float waveSpeed = 1.4f;
    public float waveLength = 200f;
    public float foamHeight = -0.16f;

    [Header("Foam")]
    public Material foamMaterial;
    public float foamWidth = 0.22f;
    public int pointsPerWave = 50;

    private LineRenderer[] waveLines;
    private Material oceanMaterialInstance;

    void Start()
    {
        if (oceanRenderer == null)
            oceanRenderer = GetComponent<Renderer>();

        if (oceanRenderer != null && oceanRenderer.sharedMaterial != null)
            oceanMaterialInstance = oceanRenderer.material;

        BuildWaves();
    }

    void BuildWaves()
    {
        int count = Mathf.Max(1, waveCount);
        int points = Mathf.Max(2, pointsPerWave);

        waveLines = new LineRenderer[count];

        for (int i = 0; i < count; i++)
        {
            GameObject wave = new GameObject("OceanFoamWave_" + i);
            wave.transform.SetParent(transform, false);

            LineRenderer line = wave.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.positionCount = points;
            line.startWidth = foamWidth;
            line.endWidth = foamWidth * 0.65f;
            line.numCapVertices = 3;
            line.alignment = LineAlignment.View;
            line.shadowCastingMode =
                UnityEngine.Rendering.ShadowCastingMode.Off;
            line.receiveShadows = false;

            if (foamMaterial != null)
                line.sharedMaterial = foamMaterial;

            waveLines[i] = line;
        }
    }

    void Update()
    {
        AnimateOceanTexture();
        AnimateWaves();
    }

    void AnimateOceanTexture()
    {
        if (oceanMaterialInstance == null)
            return;

        Vector2 offset = oceanMaterialInstance.mainTextureOffset;
        offset.x += textureScrollSpeed * Time.deltaTime;
        oceanMaterialInstance.mainTextureOffset = offset;
    }

    void AnimateWaves()
    {
        if (waveLines == null)
            return;

        for (int i = 0; i < waveLines.Length; i++)
        {
            LineRenderer line = waveLines[i];
            if (line == null) continue;

            float baseX = shorelineX + i * waveSpacing;
            float phase = i * 1.7f;

            for (int p = 0; p < line.positionCount; p++)
            {
                float t = (float)p / (line.positionCount - 1);
                float z = -waveLength / 2f + t * waveLength;

                float movement =
                    Mathf.Sin(Time.time * waveSpeed + phase) *
                    waveAmplitude;

                float ripple =
                    Mathf.Sin(z * 0.07f + Time.time * 1.8f + phase) *
                    0.35f;

                float y =
                    foamHeight +
                    Mathf.Sin(z * 0.11f + Time.time * 2f + phase) *
                    0.025f;

                line.SetPosition(
                    p,
                    new Vector3(baseX + movement + ripple, y, z)
                );
            }
        }
    }

    void OnDestroy()
    {
        if (oceanMaterialInstance != null)
            Destroy(oceanMaterialInstance);
    }
}
