using UnityEngine;

public class VehicleEnterExitManager : MonoBehaviour
{
    [Header("Player")]
    public Transform playerTransform;
    public Behaviour playerMovementController;
    public Renderer[] playerRenderers;

    [Header("Systems")]
    public CameraFollow cameraFollow;
    public MobileInputController mobileInput;

    [Header("UI Buttons")]
    public GameObject enterButton;
    public GameObject exitButton;

    [Header("Settings")]
    public float enterDistance = 4f;

    private DrivableVehicle nearbyVehicle;
    private DrivableVehicle currentVehicle;
    private PlayerCarController previousController;
    private Transform previousCameraTarget;
    private Transform previousParent;

    void Start()
    {
        if (enterButton != null)
            enterButton.SetActive(false);

        if (exitButton != null)
            exitButton.SetActive(false);
    }

    void Update()
    {
        if (currentVehicle != null || playerTransform == null)
            return;

        FindNearestVehicle();
    }

    void FindNearestVehicle()
    {
        nearbyVehicle = null;
        float nearestDistance = enterDistance;

        DrivableVehicle[] vehicles =
            FindObjectsOfType<DrivableVehicle>();

        foreach (DrivableVehicle vehicle in vehicles)
        {
            if (vehicle == null || vehicle == currentVehicle)
                continue;

            float distance = Vector3.Distance(
                playerTransform.position,
                vehicle.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearbyVehicle = vehicle;
            }
        }

        if (enterButton != null)
            enterButton.SetActive(nearbyVehicle != null);
    }

    public void TryEnterVehicle()
    {
        if (nearbyVehicle == null || playerTransform == null)
            return;

        currentVehicle = nearbyVehicle;
        previousParent = playerTransform.parent;

        if (cameraFollow != null)
            previousCameraTarget = cameraFollow.target;

        if (mobileInput != null)
            previousController = mobileInput.carController;

        if (playerMovementController != null)
            playerMovementController.enabled = false;

        if (playerRenderers != null)
        {
            foreach (Renderer item in playerRenderers)
            {
                if (item != null)
                    item.enabled = false;
            }
        }

        playerTransform.SetParent(currentVehicle.transform);
        playerTransform.localPosition =
            new Vector3(0f, 0.8f, -0.2f);

        currentVehicle.SetDriving(true);

        if (mobileInput != null)
            mobileInput.carController =
                currentVehicle.drivingController;

        if (cameraFollow != null)
            cameraFollow.target = currentVehicle.transform;

        if (enterButton != null)
            enterButton.SetActive(false);

        if (exitButton != null)
            exitButton.SetActive(true);
    }

    public void ExitCurrentVehicle()
    {
        if (currentVehicle == null)
            return;

        DrivableVehicle exitingVehicle = currentVehicle;

        currentVehicle.SetDriving(false);

        playerTransform.SetParent(previousParent);

        float sideDistance =
            exitingVehicle.vehicleKind == VehicleKind.Car ? 2.5f : 1.5f;

        playerTransform.position =
            exitingVehicle.transform.position +
            exitingVehicle.transform.right * sideDistance;

        playerTransform.rotation = Quaternion.Euler(
            0f,
            exitingVehicle.transform.eulerAngles.y,
            0f
        );

        if (playerRenderers != null)
        {
            foreach (Renderer item in playerRenderers)
            {
                if (item != null)
                    item.enabled = true;
            }
        }

        if (playerMovementController != null)
            playerMovementController.enabled = true;

        if (mobileInput != null)
            mobileInput.carController = previousController;

        if (cameraFollow != null)
            cameraFollow.target = previousCameraTarget;

        currentVehicle = null;
        nearbyVehicle = null;

        if (exitButton != null)
            exitButton.SetActive(false);
    }
}
