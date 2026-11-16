using UnityEngine;

public enum WildAnimalType
{
    Cow,
    Goat,
    Deer,
    Elephant,
    Horse,
    Monkey,
    Peacock,
    Dog,
    Lion,
    Tiger,
    Leopard,
    Cheetah,
    Wolf,
    Bear,
    Fox,
    Giraffe,
    Zebra,
    Rhinoceros,
    Hippopotamus,
    Crocodile,
    Bison,
    Buffalo,
    Kangaroo,
    Camel,
    Gorilla,
    Chimpanzee,
    Squirrel,
    Rabbit,
    Eagle,
    Parrot,
    Flamingo,
    Turtle
}

public class WildAnimalAIController : MonoBehaviour
{
    [Header("Animal")]
    public WildAnimalType animalType;

    [Header("Movement")]
    public float moveSpeed = 1.8f;
    public float wanderRadius = 12f;
    public float rotationSpeed = 4f;

    [Header("Waiting")]
    public float minWaitTime = 2f;
    public float maxWaitTime = 6f;

    private Vector3 targetPosition;
    private float waitTimer;
    private bool waiting;
    private Animator animalAnimator;

    void Start()
    {
        animalAnimator = GetComponent<Animator>();
        ChooseNewDestination();
    }

    void Update()
    {
        if (waiting)
        {
            waitTimer -= Time.deltaTime;

            if (animalAnimator != null)
                animalAnimator.SetBool("IsWalking", false);

            if (waitTimer <= 0f)
            {
                waiting = false;
                ChooseNewDestination();
            }

            return;
        }

        MoveAnimal();
    }

    void MoveAnimal()
    {
        Vector3 direction =
            targetPosition - transform.position;

        direction.y = 0f;

        if (direction.magnitude < 0.4f)
        {
            waiting = true;
            waitTimer = Random.Range(
                minWaitTime, maxWaitTime
            );
            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (animalAnimator != null)
            animalAnimator.SetBool("IsWalking", true);
    }

    void ChooseNewDestination()
    {
        Vector2 randomPoint =
            Random.insideUnitCircle * wanderRadius;

        targetPosition = new Vector3(
            transform.position.x + randomPoint.x,
            transform.position.y,
            transform.position.z + randomPoint.y
        );
    }

    public void SetAnimalSpeed(float speed)
    {
        moveSpeed = Mathf.Max(0f, speed);
    }
}
