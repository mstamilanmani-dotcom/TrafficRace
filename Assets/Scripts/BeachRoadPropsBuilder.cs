using UnityEngine;
using TMPro;

public class BeachRoadPropsBuilder : MonoBehaviour
{
    [Header("Road Settings")]
    public float roadLength = 200f;
    public float streetLightSpacing = 35f;

    [Header("Materials")]
    public Material poleMaterial;
    public Material lampMaterial;
    public Material shelterMaterial;
    public Material glassMaterial;
    public Material benchMaterial;
    public Material signMaterial;

    [Header("Tamil Sign")]
    public TMP_FontAsset tamilFont;

    void Start()
    {
        BuildStreetLights();
        BuildBusStop();
    }

    public void BuildStreetLights()
    {
        for (float z = -roadLength / 2f + 10f;
             z < roadLength / 2f;
             z += streetLightSpacing)
        {
            CreateStreetLight(-6.5f, z);
            CreateStreetLight(6.5f, z);
        }
    }

    void CreateStreetLight(float x, float z)
    {
        GameObject pole = CreatePart(
            "StreetLight_Pole",
            new Vector3(x, 4f, z),
            new Vector3(0.25f, 8f, 0.25f),
            poleMaterial
        );

        CreatePart(
            "StreetLight_Arm",
            new Vector3(x + (x < 0 ? 1.2f : -1.2f), 7.7f, z),
            new Vector3(2.5f, 0.18f, 0.18f),
            poleMaterial
        );

        GameObject lamp = CreatePart(
            "StreetLight_Lamp",
            new Vector3(x + (x < 0 ? 2.2f : -2.2f), 7.5f, z),
            new Vector3(0.8f, 0.25f, 0.8f),
            lampMaterial
        );

        Light light = lamp.AddComponent<Light>();
        light.type = LightType.Point;
        light.color = new Color(1f, 0.85f, 0.6f);
        light.range = 18f;
        light.intensity = 2f;
    }

    public void BuildBusStop()
    {
        float x = -9.5f;

        // Shelter roof
        CreatePart(
            "BusStop_Roof",
            new Vector3(x, 3f, 0f),
            new Vector3(4.5f, 0.3f, 6f),
            shelterMaterial
        );

        // Four support pillars
        for (int side = -1; side <= 1; side += 2)
        {
            for (int front = -1; front <= 1; front += 2)
            {
                CreatePart(
                    "BusStop_Pillar",
                    new Vector3(x + side * 2f, 1.5f, front * 2.6f),
                    new Vector3(0.18f, 3f, 0.18f),
                    poleMaterial
                );
            }
        }

        // Back glass panels
        CreatePart(
            "BusStop_BackGlass",
            new Vector3(x - 2f, 1.6f, 0f),
            new Vector3(0.12f, 2.5f, 5f),
            glassMaterial
        );

        // Bench
        CreatePart(
            "BusStop_Bench",
            new Vector3(x, 0.75f, 0f),
            new Vector3(2.8f, 0.18f, 0.65f),
            benchMaterial
        );

        // Sign pole
        CreatePart(
            "BusStop_SignPole",
            new Vector3(x + 2.8f, 1.8f, -3.7f),
            new Vector3(0.15f, 3.6f, 0.15f),
            poleMaterial
        );

        // Sign board
        GameObject board = CreatePart(
            "BusStop_SignBoard",
            new Vector3(x + 2.8f, 3.4f, -3.7f),
            new Vector3(0.25f, 1.1f, 3f),
            signMaterial
        );

        // Tamil text facing the road
        GameObject textObject = new GameObject("BusStop_TamilText");
        textObject.transform.SetParent(board.transform, false);
        textObject.transform.localPosition = new Vector3(0.55f, 0f, 0f);
        textObject.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

        TextMeshPro text = textObject.AddComponent<TextMeshPro>();
        text.text = "பேருந்து நிறுத்தம்";
        text.fontSize = 3.5f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;

        if (tamilFont != null)
            text.font = tamilFont;
    }

    GameObject CreatePart(
        string objectName,
        Vector3 localPosition,
        Vector3 localScale,
        Material material)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = objectName;
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = localPosition;
        obj.transform.localScale = localScale;

        if (material != null)
            obj.GetComponent<Renderer>().sharedMaterial = material;

        return obj;
    }
}
