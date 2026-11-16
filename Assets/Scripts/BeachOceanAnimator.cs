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
    public float amplitude = 0.8f;
    public float speed = 1.4f;
    public float waveTravelDistance = 1.2f;
    public float waveTravelSpeed = 0.7f;
    public float waveLength = 200f;
    public float foamHeight = -0.16f;

    [Header("Foam")]
    public Material foamMaterial;
    public Color foamColor = Color.white;
    public float foamWidth = 0.22f;
    [Range(10, 100)] public int pointsPerWave = 50;

    private Material oceanMaterial;
    private LineRenderer[] waveLines;
    private float elapsedTime;

    void Start()
    {
        if (oceanRenderer == null)
            oceanRenderer = GetComponent<Renderer>();

        if (oceanRenderer != null)
            oceanMaterial = oceanRenderer.material;

        BuildWaves();
    }

    void BuildWaves()
    {
        waveLines = new LineRenderer[waveCount];

        for (int i = 0; i < waveCount; i++)
        {
            GameObject waveObject = new GameObject("Ocean_Foam_Wave_" + i);
            waveObject.transform.SetParent(transform, false);

            LineRenderer line = waveObject.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.positionCount = pointsPerWave;
            line.startWidth = foamWidth;
            line.endWidth = foamWidth * 0.65f;
            line.startColor = foamColor;
            line.endColor = foamColor;
            line.alignment = LineAlignment.View;
            line.textureMode = LineTextureMode.Tile;
            line.numCapVertices = 4;
            line.numCornerVertices = 4;
            line.receiveShadows = false;

            if (foamMaterial != null)
                line.sharedMaterial = foamMaterial;

            waveLines[i] = line;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        AnimateWaterTexture();
        AnimateWaves();
    }

    void AnimateWaterTexture()
    {
        if (oceanMaterial == null)
            return;

        Vector2 offset = oceanMaterial.mainTextureOffset;
        offset.x += textureScrollSpeed * Time.deltaTime;
        oceanMaterial.mainTextureOffset = offset;
    }

    void AnimateWaves()
    {
        if (waveLines == null)
            return;

        for (int i = 0; i < waveLines.Length; i++)
        {
            LineRenderer line = waveLines[i];

            if (line == null)
                continue;

            float baseX = shorelineX + 1.5f + i * waveSpacing;

            float travel = Mathf.Sin(
                elapsedTime * waveTravelSpeed + i * 1.7f
            ) * waveTravelDistance;

            for (int p = 0; p < pointsPerWave; p++)
            {
                float t = (float)p / (pointsPerWave - 1);
                float z = Mathf.Lerp(-waveLength / 2f, waveLength / 2f, t);

                float curve = Mathf.Sin(
                    z * 0.08f + elapsedTime * speed + i * 1.7f
                ) * amplitude;

                float x = baseX + travel + curve;
                float y = foamHeight + Mathf.Sin(
                    z * 0.12f + elapsedTime * speed
                ) * 0.025f;

                line.SetPosition(p, new Vector3(x, y, z));
            }
        }
    }

    void OnDestroy()
    {
        if (oceanMaterial != null)
            Destroy(oceanMaterial);
    }
}
