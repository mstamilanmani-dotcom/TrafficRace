using UnityEngine;

public class BeachParkedVehiclesBuilder : MonoBehaviour
{
    [Header("Parking Position")]
    public float parkingX = 10f;
    public float parkingZ = 0f;
    public float slotSpacing = 4.5f;
    public int vehicleCount = 8;

    [Header("Vehicle Materials")]
    public Material[] carMaterials;
    public Material bikeMaterial;
    public Material windowMaterial;
    public Material tyreMaterial;
    public Material lightMaterial;

    void Start()
    {
        BuildParkedVehicles();
    }

    public void BuildParkedVehicles()
    {
        for (int i = 0; i < vehicleCount; i++)
        {
            float z = parkingZ -
                (vehicleCount * slotSpacing / 2f)
                + (i * slotSpacing)
                + (slotSpacing / 2f);

            if (i % 2 == 0)
                CreateCar(i, z);
            else
                CreateBike(i, z);
        }
    }

    void CreateCar(int index, float z)
    {
        GameObject car = new GameObject("Parked_Car_" + index);
        car.transform.SetParent(transform, false);
        car.transform.localPosition = new Vector3(parkingX, 0.16f, z);

        Material bodyMaterial = GetCarMaterial(index);

        CreatePart(car.transform, "Car_Body",
            new Vector3(0f, 0.85f, 0f),
            new Vector3(1.9f, 0.65f, 3.5f), bodyMaterial);

        CreatePart(car.transform, "Car_Cabin",
            new Vector3(0f, 1.42f, -0.15f),
            new Vector3(1.55f, 0.75f, 1.8f), windowMaterial);

        CreatePart(car.transform, "Front_Windshield",
            new Vector3(0f, 1.48f, 0.78f),
            new Vector3(1.35f, 0.55f, 0.08f), windowMaterial);

        CreatePart(car.transform, "Rear_Windshield",
            new Vector3(0f, 1.48f, -1.08f),
            new Vector3(1.35f, 0.55f, 0.08f), windowMaterial);

        for (int side = -1; side <= 1; side += 2)
        {
            for (int end = -1; end <= 1; end += 2)
            {
                CreateWheel(car.transform,
                    new Vector3(side * 0.98f, 0.48f, end * 1.12f));
            }
        }

        CreatePart(car.transform, "Front_Light_Left",
            new Vector3(-0.65f, 0.85f, 1.77f),
            new Vector3(0.35f, 0.18f, 0.06f), lightMaterial);

        CreatePart(car.transform, "Front_Light_Right",
            new Vector3(0.65f, 0.85f, 1.77f),
            new Vector3(0.35f, 0.18f, 0.06f), lightMaterial);
    }

    void CreateBike(int index, float z)
    {
        GameObject bike = new GameObject("Parked_Bike_" + index);
        bike.transform.SetParent(transform, false);
        bike.transform.localPosition = new Vector3(parkingX, 0.16f, z);

        CreateWheel(bike.transform, new Vector3(0f, 0.45f, 0.85f));
        CreateWheel(bike.transform, new Vector3(0f, 0.45f, -0.85f));

        CreatePart(bike.transform, "Bike_Frame",
            new Vector3(0f, 0.85f, 0f),
            new Vector3(0.18f, 0.2f, 1.3f), bikeMaterial);

        CreatePart(bike.transform, "Bike_FuelTank",
            new Vector3(0f, 1.05f, 0.1f),
            new Vector3(0.55f, 0.45f, 0.65f), bikeMaterial);

        CreatePart(bike.transform, "Bike_Seat",
            new Vector3(0f, 1.15f, -0.48f),
            new Vector3(0.45f, 0.18f, 0.55f), tyreMaterial);

        CreatePart(bike.transform, "Bike_Handle",
            new Vector3(0f, 1.35f, 0.85f),
            new Vector3(0.9f, 0.08f, 0.08f), tyreMaterial);

        CreatePart(bike.transform, "Bike_Headlight",
            new Vector3(0f, 1.15f, 0.95f),
            new Vector3(0.3f, 0.3f, 0.15f), lightMaterial);
    }

    void CreateWheel(Transform parent, Vector3 position)
    {
        GameObject wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        wheel.name = "Vehicle_Wheel";
        wheel.transform.SetParent(parent, false);
        wheel.transform.localPosition = position;
        wheel.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        wheel.transform.localScale = new Vector3(0.22f, 0.38f, 0.38f);

        ApplyMaterial(wheel, tyreMaterial);
    }

    Material GetCarMaterial(int index)
    {
        if (carMaterials != null && carMaterials.Length > 0)
            return carMaterials[index % carMaterials.Length];

        return null;
    }

    void CreatePart(
        Transform parent,
        string objectName,
        Vector3 position,
        Vector3 scale,
        Material material)
    {
        GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.name = objectName;
        part.transform.SetParent(parent, false);
        part.transform.localPosition = position;
        part.transform.localScale = scale;

        ApplyMaterial(part, material);
    }

    void ApplyMaterial(GameObject obj, Material material)
    {
        if (material != null)
            obj.GetComponent<Renderer>().sharedMaterial = material;
    }
}
