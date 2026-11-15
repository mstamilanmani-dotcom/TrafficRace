using UnityEngine;

public class BeachWaterMaterialController : MonoBehaviour
{
    [Header("Water Renderer")]
    public Renderer waterRenderer;
    public Texture2D waterNormalMap;

    [Header("Water Appearance")]
    public Color waterColor = new Color(0.02f, 0.35f, 0.48f, 1f);
    [Range(0f, 1f)] public float smoothness = 0.88f;
    [Range(0f, 1f)] public float metallic = 0.05f;
    public float normalStrength = 0.65f;

    [Header("Water Movement")]
    public float textureScrollX = 0.012f;
    public float textureScrollY = 0.006f;

    [Header("Shoreline Foam")]
    public Renderer foamRenderer;
    public Color foamColor = Color.white;
    [Range(0f, 1f)] public float foamTransparency = 0.85f;

    private Material waterMaterial;
    private Material foamMaterialInstance;

    void Start()
    {
        SetupWater();
        SetupFoam();
    }

    void SetupWater()
    {
        if (waterRenderer == null)
            return;

        waterMaterial = waterRenderer.material;

        SetColor(waterMaterial, waterColor);

        SetFloat(waterMaterial, "_Smoothness", smoothness);
        SetFloat(waterMaterial, "_Glossiness", smoothness);
        SetFloat(waterMaterial, "_Metallic", metallic);

        if (waterNormalMap != null &&
            waterMaterial.HasProperty("_BumpMap"))
        {
            waterMaterial.SetTexture("_BumpMap", waterNormalMap);
            waterMaterial.SetFloat("_BumpScale", normalStrength);
            waterMaterial.EnableKeyword("_NORMALMAP");
        }
    }

    void SetupFoam()
    {
        if (foamRenderer == null)
            return;

        foamMaterialInstance = foamRenderer.material;
        Color finalColor = foamColor;
        finalColor.a = foamTransparency;

        SetColor(foamMaterialInstance, finalColor);
    }

    void Update()
    {
        if (waterMaterial == null)
            return;

        Vector2 offset = waterMaterial.mainTextureOffset;
        offset.x += textureScrollX * Time.deltaTime;
        offset.y += textureScrollY * Time.deltaTime;

        waterMaterial.mainTextureOffset = offset;
    }

    void SetColor(Material material, Color color)
    {
        if (material.HasProperty("_BaseColor"))
            material.SetColor("_BaseColor", color);

        if (material.HasProperty("_Color"))
            material.SetColor("_Color", color);
    }

    void SetFloat(Material material, string property, float value)
    {
        if (material.HasProperty(property))
            material.SetFloat(property, value);
    }

    void OnDestroy()
    {
        if (waterMaterial != null)
            Destroy(waterMaterial);

        if (foamMaterialInstance != null)
            Destroy(foamMaterialInstance);
    }
}
