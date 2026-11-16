using UnityEngine;
using TMPro;

public class BeachParkingBuilder : MonoBehaviour
{
    [Header("Parking Settings")]
    public int parkingSlots = 8;
    public float parkingWidth = 7f;
    public float parkingLength = 40f;
    public float slotSpacing = 4.5f;

    [Header("Position")]
    public float parkingX = 10f;
    public float parkingZ = 0f;

    [Header("Materials")]
    public Material concreteMaterial;
    public Material whiteLineMaterial;
    public Material signMaterial;
    public Material poleMaterial;

    [Header("Sign")]
    public TMP_FontAsset englishFont;

    void Start()
    {
        BuildParkingArea();
    }

    public void BuildParkingArea()
    {
        CreatePart(
            "BeachParking_Floor",
            new Vector3(parkingX, 0.08f, parkingZ),
            new Vector3(parkingWidth, 0.16f, parkingLength),
            concreteMaterial
        );

        CreateParkingLines();
        CreateParkingSign();
    }

    void CreateParkingLines()
    {
        int slots = Mathf.Max(1, parkingSlots);
        float spacing = Mathf.Max(0.1f, slotSpacing);

        // Equal spacing on both sides of the parking centre
        float startZ = parkingZ - (slots * spacing / 2f);

        for (int i = 0; i <= slots; i++)
        {
            float z = startZ + i * spacing;

            CreatePart(
                "Parking_White_Line",
                new Vector3(parkingX, 0.18f, z),
                new Vector3(parkingWidth - 0.4f, 0.035f, 0.12f),
                whiteLineMaterial
            );
        }

        // Side boundary lines
        for (int side = -1; side <= 1; side += 2)
        {
            CreatePart(
                "Parking_Side_Line",
                new Vector3(
                    parkingX + side * (parkingWidth / 2f - 0.2f),
                    0.18f,
                    parkingZ
                ),
                new Vector3(0.12f, 0.035f, parkingLength),
                whiteLineMaterial
            );
        }
    }

    void CreateParkingSign()
    {
        float signX = parkingX;
        float signZ = parkingZ - parkingLength / 2f - 2f;

        CreatePart(
            "Parking_Sign_Pole",
            new Vector3(signX, 1.5f, signZ),
            new Vector3(0.18f, 3f, 0.18f),
            poleMaterial
        );

        GameObject board = CreatePart(
            "Parking_Sign_Board",
            new Vector3(signX, 3.2f, signZ),
            new Vector3(2.5f, 1f, 0.15f),
            signMaterial
        );

        GameObject textObject =
            new GameObject("Parking_Sign_Text");

        textObject.transform.SetParent(board.transform, false);

        // Text faces the front (+Z) side of the board
        textObject.transform.localPosition =
            new Vector3(0f, 0f, 0.09f);

        TextMeshPro text =
            textObject.AddComponent<TextMeshPro>();

        text.text = "BEACH PARKING";
        text.fontSize = 2f;
        text.fontSizeMin = 0.5f;
        text.fontSizeMax = 2f;
        text.enableAutoSizing = true;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;

        if (englishFont != null)
            text.font = englishFont;
    }

    GameObject CreatePart(
        string objectName,
        Vector3 position,
        Vector3 scale,
        Material material)
    {
        GameObject obj =
            GameObject.CreatePrimitive(PrimitiveType.Cube);

        obj.name = objectName;
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = position;
        obj.transform.localScale = scale;

        if (material != null)
            obj.GetComponent<Renderer>().sharedMaterial = material;

        return obj;
    }
}
