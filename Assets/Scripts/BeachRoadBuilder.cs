using UnityEngine;

public class BeachRoadBuilder : MonoBehaviour
{
    [Header("Road Settings")]
    public float roadWidth = 8f;
    public float roadLength = 200f;

    [Header("Beach Settings")]
    public float beachWidth = 25f;
    public float oceanWidth = 60f;

    [Header("Materials")]
    public Material roadMaterial;
    public Material sandMaterial;
    public Material waterMaterial;
    public Material palmMaterial;
    public Material roadLineMaterial;

    [Header("Palm Trees")]
    public int palmTreeCount = 12;
    public float palmSpacing = 15f;

    void Start()
    {
        BuildBeachRoad();
    }

    public void BuildBeachRoad()
    {
        CreateSurface(
            "BeachRoad",
            new Vector3(0f, 0f, 0f),
            new Vector3(roadWidth, 0.3f, roadLength),
            roadMaterial
        );

        CreateSurface(
            "BeachSand",
            new Vector3(roadWidth / 2f + beachWidth / 2f,
                        -0.2f, 0f),
            new Vector3(beachWidth, 0.2f, roadLength),
            sandMaterial
        );

        CreateSurface(
            "Ocean",
            new Vector3(
                roadWidth / 2f + beachWidth + oceanWidth / 2f,
                -0.3f,
                0f
            ),
            new Vector3(oceanWidth, 0.2f, roadLength),
            waterMaterial
        );

        CreateRoadLines();
        CreatePalmTrees();
    }

    void CreateRoadLines()
    {
        for (float z = -roadLength / 2f + 5f;
             z < roadLength / 2f;
             z += 10f)
        {
            CreateSurface(
                "CentreRoadLine",
                new Vector3(0f, 0.17f, z),
                new Vector3(0.12f, 0.03f, 4f),
                roadLineMaterial
            );
        }
    }

    void CreatePalmTrees()
    {
        for (int i = 0; i < palmTreeCount; i++)
        {
            float z = -roadLength / 2f + 10f +
                      i * palmSpacing;

            CreatePalmTree(
                new Vector3(-roadWidth / 2f - 5f, 0f, z)
            );
        }
    }

    void CreatePalmTree(Vector3 position)
    {
        GameObject trunk =
            GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        trunk.name = "PalmTree_Trunk";
        trunk.transform.SetParent(transform);
        trunk.transform.position = position +
                                   new Vector3(0f, 3f, 0f);
        trunk.transform.localScale =
            new Vector3(0.35f, 3f, 0.35f);

        ApplyMaterial(trunk, palmMaterial);

        for (int i = 0; i < 5; i++)
        {
            GameObject leaf =
                GameObject.CreatePrimitive(PrimitiveType.Capsule);

            float angle = i * 72f;

            leaf.name = "PalmTree_Leaf";
            leaf.transform.SetParent(transform);
            leaf.transform.position =
                position + new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad) * 1.5f,
                    6f,
                    Mathf.Sin(angle * Mathf.Deg2Rad) * 1.5f
                );

            leaf.transform.rotation =
                Quaternion.Euler(0f, 0f, 45f);

            leaf.transform.localScale =
                new Vector3(0.18f, 2f, 0.18f);

            ApplyMaterial(leaf, palmMaterial);
        }
    }

    void CreateSurface(
        string objectName,
        Vector3 position,
        Vector3 scale,
        Material material)
    {
        GameObject surface =
            GameObject.CreatePrimitive(PrimitiveType.Cube);

        surface.name = objectName;
        surface.transform.SetParent(transform);
        surface.transform.localPosition = position;
        surface.transform.localScale = scale;

        ApplyMaterial(surface, material);
    }

    void ApplyMaterial(GameObject obj, Material material)
    {
        if (material != null)
            obj.GetComponent<Renderer>().sharedMaterial = material;
    }
}
