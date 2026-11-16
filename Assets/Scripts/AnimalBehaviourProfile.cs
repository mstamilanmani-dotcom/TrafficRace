using UnityEngine;

[RequireComponent(typeof(WildAnimalAIController))]
public class AnimalBehaviourProfile : MonoBehaviour
{
    private WildAnimalAIController animalAI;

    void Awake()
    {
        animalAI = GetComponent<WildAnimalAIController>();
        ApplyProfile();
    }

    void ApplyProfile()
    {
        switch (animalAI.animalType)
        {
            case WildAnimalType.Lion:
                SetProfile(1.5f, 10f, 3f, 7f);
                break;

            case WildAnimalType.Tiger:
                SetProfile(1.8f, 12f, 3f, 6f);
                break;

            case WildAnimalType.Cheetah:
                SetProfile(2.5f, 15f, 2f, 5f);
                break;

            case WildAnimalType.Elephant:
                SetProfile(1.2f, 8f, 4f, 8f);
                break;

            case WildAnimalType.Deer:
                SetProfile(2.2f, 12f, 2f, 5f);
                break;

            case WildAnimalType.Giraffe:
            case WildAnimalType.Zebra:
                SetProfile(1.8f, 10f, 3f, 6f);
                break;

            case WildAnimalType.Bear:
            case WildAnimalType.Wolf:
                SetProfile(1.7f, 10f, 3f, 7f);
                break;

            case WildAnimalType.Cow:
            case WildAnimalType.Goat:
            case WildAnimalType.Horse:
                SetProfile(1.4f, 8f, 3f, 7f);
                break;

            default:
                SetProfile(1.6f, 8f, 2f, 6f);
                break;
        }
    }

    void SetProfile(
        float speed,
        float radius,
        float minWait,
        float maxWait)
    {
        animalAI.moveSpeed = speed;
        animalAI.wanderRadius = radius;
        animalAI.minWaitTime = minWait;
        animalAI.maxWaitTime = maxWait;
    }
}
