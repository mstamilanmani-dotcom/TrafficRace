using UnityEngine;

public class BeachRoadBuilder : MonoBehaviour
{
    [Header("Road Settings")]
    public float roadWidth = 8f;
    public float roadLength = 200f;
    public float roadHeight = 0.3f;

    [Header("Beach Settings")]
    public float beachWidth = 25f;
    public float oceanWidth = 60f;

    [Header("Materials")]
    public Material roadMaterial;
    public Material sandMaterial;
    public Material waterMaterial;
    public Material palmMaterial;
    public Material roadLineMaterial;

    [Header("Road Markings")]
    public float centreLineSpacing = 10f;
    public float centreLineLength = 4f;
    public float edgeLineWidth = 0.12f;

    [Header("Palm Trees")]
    public int palmTreeCount = 8;
    public float palmSpacing = 20f;
    public float palmTreeX = 22f;

    void Start()
    {
        BuildBeachRoad();
    }

    public void BuildBeachRoad()
    {
        CreateSurface(
            "BeachRoad",
            new Vector3(0f, 0f, 0f),
            new Vector3(roadWidth, roadHeight, roadLength),
            roadMaterial
        );

        CreateSurface(
            "BeachSand",
            new Vector3(
                roadWidth / 2f + beachWidth / 2f,
                -0.2f,
                0f
            ),
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
        float roadTop = roadHeight / 2f + 0.025f;

        // Dashed centre divider
        for (
            float z = -roadLength / 2f + 5f;
            z < roadLength / 2f;
            z += centreLineSpacing
        )
        {
            CreateSurface(
                "CentreRoadLine",
                new Vector3(0f, roadTop, z),
                new Vector3(0.14f, 0.04f, centreLineLength),
                roadLineMaterial
            );
        }

        // Solid left road edge
        CreateSurface(
            "LeftRoadEdgeLine",
            new Vector3(
                -roadWidth / 2f + edgeLineWidth / 2f,
                roadTop,
                0f
            ),
            new Vector3(
                edgeLineWidth,
                0.04f,
                roadLength
            ),
            roadLineMaterial
        );

        // Solid right road edge
        CreateSurface(
            "RightRoadEdgeLine",
            new Vector3(
                roadWidth / 2f - edgeLineWidth / 2f,
                roadTop,
                0f
            ),
            new Vector3(
                edgeLineWidth,
                0.04f,
                roadLength
            ),
            roadLineMaterial
        );
    }

    void CreatePalmTrees()
    {
        if (palmTreeCount <= 0)
            return;

        for (int i = 0; i < palmTreeCount; i++)
        {
            float z = -roadLength / 2f + 10f
                      + i * palmSpacing;

            if (z >= roadLength / 2f)
                break;

            CreatePalmTree(
                new Vector3(palmTreeX, 0f, z)
            );
        }
    }

    void CreatePalmTree(Vector3 position)
    {
        GameObject tree = new GameObject("Beach_PalmTree");
        tree.transform.SetParent(transform, false);
        tree.transform.localPosition = position;

        GameObject trunk =
            GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        trunk.name = "PalmTree_Trunk";
        trunk.transform.SetParent(tree.transform, false);
        trunk.transform.localPosition = new Vector3(0f, 3f, 0f);
        trunk.transform.localScale = new Vector3(0.35f, 3f, 0.35f);

        ApplyMaterial(trunk, palmMaterial);

        for (int i = 0; i < 6; i++)
        {
            float angle = i * 60f;
            float radians = angle * Mathf.Deg2Rad;

            GameObject leaf =
                GameObject.CreatePrimitive(PrimitiveType.Capsule);

            leaf.name = "PalmTree_Leaf";
            leaf.transform.SetParent(tree.transform, false);

            leaf.transform.localPosition = new Vector3(
                Mathf.Cos(radians) * 1.1f,
                5.8f,
                Mathf.Sin(radians) * 1.1f
            );

            leaf.transform.localRotation =
                Quaternion.Euler(0f, angle, 55f);

            leaf.transform.localScale =
                new Vector3(0.18f, 1.7f, 0.3f);

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
        surface.transform.SetParent(transform, false);
        surface.transform.localPosition = position;
        surface.transform.localScale = scale;

        ApplyMaterial(surface, material);
    }

    void ApplyMaterial(GameObject obj, Material material)
    {
        if (material != null)
        {
            Renderer objectRenderer = obj.GetComponent<Renderer>();

            if (objectRenderer != null)
                objectRenderer.sharedMaterial = material;
        }
    }
}
