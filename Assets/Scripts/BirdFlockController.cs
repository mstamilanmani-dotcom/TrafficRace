using UnityEngine;

public class BirdFlockController : MonoBehaviour
{
    [Header("Flock Settings")]
    public Transform flockCenter;
    public float flockRadius = 12f;
    public float minHeight = 8f;
    public float maxHeight = 18f;

    [Header("Flight Settings")]
    public float flySpeed = 5f;
    public float turnSpeed = 3f;
    public float destinationChangeTime = 4f;

    private Vector3 targetPosition;
    private float timer;
    private Animator birdAnimator;

    void Start()
    {
        birdAnimator = GetComponent<Animator>();
        ChooseNewDestination();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= destinationChangeTime ||
            Vector3.Distance(transform.position, targetPosition) < 2f)
        {
            ChooseNewDestination();
        }

        Vector3 direction = targetPosition - transform.position;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            flySpeed * Time.deltaTime
        );

        if (birdAnimator != null)
        {
            birdAnimator.SetBool("IsFlying", true);
        }
    }

    void ChooseNewDestination()
    {
        timer = 0f;

        Vector3 center = flockCenter != null
            ? flockCenter.position
            : transform.position;

        Vector2 randomCircle = Random.insideUnitCircle * flockRadius;

        targetPosition = center + new Vector3(
            randomCircle.x,
            Random.Range(minHeight, maxHeight),
            randomCircle.y
        );
    }

    public void SetFlockCenter(Transform center)
    {
        flockCenter = center;
    }
}0

