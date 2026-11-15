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

    [Header("Tamil Font")]
    public TMP_FontAsset tamilFont;

    private string[] shopNames =
    {
        "மணி மளிகை",
        "அண்ணா டீ கடை",
        "அம்மா உணவகம்"
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
                new Vector3(
                    -shopDistanceFromRoad,
                    0f,
                    zPosition
                )
            );
        }
    }

    void CreateShop(string shopName, Vector3 position)
    {
        GameObject shop = new GameObject(shopName);
        shop.transform.SetParent(transform);
        shop.transform.localPosition = position;

        GameObject building = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );

        building.name = "ShopBuilding";
        building.transform.SetParent(shop.transform);
        building.transform.localPosition =
            new Vector3(0f, shopSize.y / 2f, 0f);
        building.transform.localScale = shopSize;

        ApplyMaterial(building, wallMaterial);

        GameObject roof = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );

        roof.name = "ShopRoof";
        roof.transform.SetParent(shop.transform);
        roof.transform.localPosition =
            new Vector3(0f, shopSize.y + 0.2f, 0f);
        roof.transform.localScale =
            new Vector3(shopSize.x + 0.5f, 0.4f, shopSize.z + 0.5f);

        ApplyMaterial(roof, roofMaterial);

        CreateNameBoard(shop.transform, shopName);
    }

    void CreateNameBoard(Transform shop, string shopName)
    {
        GameObject board = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );

        board.name = "TamilShopNameBoard";
        board.transform.SetParent(shop);

        board.transform.localPosition = new Vector3(
            shopSize.x / 2f + 0.1f,
            shopSize.y - 1.2f,
            0f
        );

        board.transform.localScale =
            new Vector3(0.2f, 1.2f, 6.5f);

        ApplyMaterial(board, boardMaterial);

        GameObject textObject = new GameObject("TamilShopText");
        textObject.transform.SetParent(board.transform);

        textObject.transform.localPosition =
            new Vector3(0.11f, 0f, 0f);

        textObject.transform.localRotation =
            Quaternion.Euler(0f, 90f, 0f);

        TextMeshPro text =
            textObject.AddComponent<TextMeshPro>();

        text.text = shopName;
        text.fontSize = 4f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.enableAutoSizing = true;
        text.rectTransform.sizeDelta =
            new Vector2(6f, 1f);

        if (tamilFont != null)
            text.font = tamilFont;
    }

    void ApplyMaterial(GameObject obj, Material material)
    {
        if (material != null)
            obj.GetComponent<Renderer>().sharedMaterial = material;
    }
}
