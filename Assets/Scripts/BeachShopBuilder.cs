using UnityEngine;
using TMPro;

public class BeachShopBuilder : MonoBehaviour
{
    [Header("Shop Settings")]
    public Vector3 shopSize = new Vector3(10f, 6f, 8f);
    public float shopDistanceFromRoad = 18f;

    [Header("Materials")]
    public Material wallMaterial;
    public Material roofMaterial;
    public Material boardMaterial;
    public Material glassMaterial;
    public Material doorMaterial;
    public Material lightMaterial;

    [Header("Tamil Font")]
    public TMP_FontAsset tamilFont;

    private string[] shopNames =
    {
        "மணி மளிகை",
        "அண்ணா டீ கடை",
        "அம்மா உணவகம்"
    };

    private Color[] shopColors =
    {
        new Color(0.85f, 0.65f, 0.35f),
        new Color(0.35f, 0.65f, 0.8f),
        new Color(0.75f, 0.45f, 0.4f)
    };

    void Start()
    {
        BuildShops();
    }

    public void BuildShops()
    {
        for (int i = 0; i < shopNames.Length; i++)
        {
            float zPosition = -50f + i * 50f;

            CreateShop(
                shopNames[i],
                new Vector3(-shopDistanceFromRoad, 0f, zPosition),
                shopColors[i]
            );
        }
    }

    void CreateShop(string shopName, Vector3 position, Color shopColor)
    {
        GameObject shop = new GameObject(shopName);
        shop.transform.SetParent(transform);
        shop.transform.localPosition = position;

        GameObject building = CreatePart(
            "ShopBuilding",
            shop.transform,
            new Vector3(0f, shopSize.y / 2f, 0f),
            shopSize,
            wallMaterial
        );

        if (wallMaterial == null)
            building.GetComponent<Renderer>().material.color = shopColor;

        CreatePart(
            "ShopRoof",
            shop.transform,
            new Vector3(0f, shopSize.y + 0.2f, 0f),
            new Vector3(shopSize.x + 0.5f, 0.4f, shopSize.z + 0.5f),
            roofMaterial
        );

        CreateDoor(shop.transform);
        CreateWindows(shop.transform);
        CreateShopLights(shop.transform);
        CreateNameBoard(shop.transform, shopName);
    }

    void CreateDoor(Transform shop)
    {
        CreatePart(
            "ShopDoor",
            shop,
            new Vector3(0f, 1.2f, -shopSize.z / 2f - 0.1f),
            new Vector3(1.8f, 2.4f, 0.15f),
            doorMaterial
        );
    }

    void CreateWindows(Transform shop)
    {
        for (int i = -1; i <= 1; i += 2)
        {
            CreatePart(
                "GlassWindow",
                shop,
                new Vector3(i * 3f, 2.5f, -shopSize.z / 2f - 0.12f),
                new Vector3(2.3f, 1.8f, 0.12f),
                glassMaterial
            );
        }
    }

    void CreateShopLights(Transform shop)
    {
        for (int i = -1; i <= 1; i += 2)
        {
            GameObject lightObject = new GameObject("ShopLEDLight");
            lightObject.transform.SetParent(shop);

            lightObject.transform.localPosition =
                new Vector3(i * 2.5f, shopSize.y - 0.7f, -shopSize.z / 2f - 0.3f);

            Light shopLight = lightObject.AddComponent<Light>();
            shopLight.type = LightType.Point;
            shopLight.color = new Color(1f, 0.8f, 0.5f);
            shopLight.intensity = 3f;
            shopLight.range = 8f;
        }
    }

    void CreateNameBoard(Transform shop, string shopName)
    {
        CreatePart(
            "TamilShopNameBoard",
            shop,
            new Vector3(0f, shopSize.y - 1.2f, -shopSize.z / 2f - 0.2f),
            new Vector3(6.5f, 1.2f, 0.2f),
            boardMaterial
        );

        GameObject textObject = new GameObject("TamilShopText");
        textObject.transform.SetParent(shop);

        textObject.transform.localPosition =
            new Vector3(0f, shopSize.y - 1.2f, -shopSize.z / 2f - 0.32f);

        textObject.transform.localRotation =
            Quaternion.Euler(0f, 180f, 0f);

        TextMeshPro text = textObject.AddComponent<TextMeshPro>();
        text.text = shopName;
        text.fontSize = 4f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.enableAutoSizing = true;
        text.rectTransform.sizeDelta = new Vector2(6.5f, 1.2f);

        if (tamilFont != null)
            text.font = tamilFont;
    }

    GameObject CreatePart(
        string partName,
        Transform parent,
        Vector3 localPosition,
        Vector3 scale,
        Material material)
    {
        GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.name = partName;
        part.transform.SetParent(parent);
        part.transform.localPosition = localPosition;
        part.transform.localScale = scale;

        if (material != null)
            part.GetComponent<Renderer>().sharedMaterial = material;

        return part;
    }
}
