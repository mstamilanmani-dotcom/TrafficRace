using UnityEngine;

public class PetCompanionController : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public float followDistance = 2f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 8f;

    [Header("Vehicle Settings")]
    public Vector3 carSeatPosition =
        new Vector3(0f, 0.5f, -0.6f);

    public Vector3 bikeSeatPosition =
        new Vector3(0f, 0.7f, -0.8f);

    private Transform vehicle;
    private bool ridingVehicle;
    private Animator petAnimator;

    void Awake()
    {
        petAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ridingVehicle && vehicle != null)
        {
            FollowVehicleSeat();
            return;
        }

        if (player != null)
            FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition =
            player.position -
            player.forward * followDistance;

        targetPosition.y = transform.position.y;

        float distance = Vector3.Distance(
            transform.position, targetPosition
        );

        if (distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            Vector3 direction =
                targetPosition - transform.position;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        if (petAnimator != null)
            petAnimator.SetBool("IsWalking", distance > 0.5f);
    }

    void FollowVehicleSeat()
    {
        if (petAnimator != null)
            petAnimator.SetBool("IsWalking", false);
    }

    public void EnterVehicle(Transform vehicleTransform, bool isBike)
    {
        if (vehicleTransform == null)
            return;

        vehicle = vehicleTransform;
        ridingVehicle = true;

        transform.SetParent(vehicle);

        transform.localPosition = isBike
            ? bikeSeatPosition
            : carSeatPosition;

        transform.localRotation = Quaternion.identity;
    }

    public void ExitVehicle(Transform playerTransform)
    {
        ridingVehicle = false;
        vehicle = null;

        transform.SetParent(null);

        if (playerTransform != null)
            player = playerTransform;
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
}
