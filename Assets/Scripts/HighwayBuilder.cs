using UnityEngine;

public class HighwayBuilder : MonoBehaviour
{
    [Header("Highway Settings")]
    public float laneWidth = 3.5f;
    public float roadLength = 100f;
    public int roadSectionCount = 3;

    [Header("Road Objects")]
    public float roadThickness = 0.3f;
    public float dividerWidth = 0.8f;
    public float dividerHeight = 0.8f;

    [Header("Materials")]
    public Material roadMaterial;
    public Material lineMaterial;
    public Material dividerMaterial;

    public Transform[] roadSections;

    void Start()
    {
        BuildHighway();
    }

    public void BuildHighway()
    {
        roadSections = new Transform[roadSectionCount];

        for (int i = 0; i < roadSectionCount; i++)
        {
            GameObject section = new GameObject(
                "RoadSection_" + i
            );

            section.transform.SetParent(transform);
            section.transform.position =
                transform.position +
                Vector3.forward * roadLength * i;

            roadSections[i] = section.transform;

            CreateRoad(section.transform);
            CreateLaneMarkings(section.transform);
            CreateDivider(section.transform);
        }
    }

    void CreateRoad(Transform parent)
    {
        float totalWidth =
            laneWidth * 4f + dividerWidth;

        CreateCube(
            "RoadSurface",
            parent,
            new Vector3(0f, 0f, roadLength / 2f),
            new Vector3(totalWidth, roadThickness, roadLength),
            roadMaterial
        );
    }

    void CreateLaneMarkings(Transform parent)
    {
        float[] linePositions =
        {
            -laneWidth,
            0f,
            laneWidth
        };

        foreach (float x in linePositions)
        {
            if (Mathf.Abs(x) < 0.01f)
                continue;

            for (float z = 5f; z < roadLength; z += 10f)
            {
                CreateCube(
                    "LaneMarking",
                    parent,
                    new Vector3(
                        x,
                        roadThickness / 2f + 0.01f,
                        z
                    ),
                    new Vector3(0.12f, 0.03f, 4f),
                    lineMaterial
                );
            }
        }
    }

    void CreateDivider(Transform parent)
    {
        CreateCube(
            "CentreDivider",
            parent,
            new Vector3(
                0f,
                dividerHeight / 2f,
                roadLength / 2f
            ),
            new Vector3(
                dividerWidth,
                dividerHeight,
                roadLength
            ),
            dividerMaterial
        );
    }

    void CreateCube(
        string objectName,
        Transform parent,
        Vector3 localPosition,
        Vector3 scale,
        Material material
    )
    {
        GameObject obj = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );

        obj.name = objectName;
        obj.transform.SetParent(parent);
        obj.transform.localPosition = localPosition;
        obj.transform.localScale = scale;

        if (material != null)
        {
            obj.GetComponent<Renderer>().material = material;
        }
    }
}
