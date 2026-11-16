using UnityEngine;

public class AnimalHerdFollower : MonoBehaviour
{
    [Header("Herd Leader")]
    public Transform leader;

    [Header("Follow Settings")]
    public Vector3 followOffset =
        new Vector3(2f, 0f, -3f);

    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float stopDistance = 0.5f;

    private WildAnimalAIController animalAI;
    private Animator animalAnimator;

    void Awake()
    {
        animalAI = GetComponent<WildAnimalAIController>();
        animalAnimator = GetComponent<Animator>();

        if (animalAI != null)
            animalAI.enabled = false;
    }

    void Update()
    {
        if (leader == null)
            return;

        Vector3 targetPosition =
            leader.TransformPoint(followOffset);

        targetPosition.y = transform.position.y;

        Vector3 direction =
            targetPosition - transform.position;

        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            direction.y = 0f;

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

        if (animalAnimator != null)
        {
            animalAnimator.SetBool(
                "IsWalking",
                distance > stopDistance
            );
        }
    }

    public void SetLeader(Transform newLeader)
    {
        leader = newLeader;
    }
}
