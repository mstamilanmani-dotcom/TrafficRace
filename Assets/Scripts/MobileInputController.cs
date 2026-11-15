
using UnityEngine;

public class MobileInputController : MonoBehaviour
{
    public PlayerCarController carController;

    public void LeftButtonDown()
    {
        carController.SetSteering(-1f);
    }

    public void RightButtonDown()
    {
        carController.SetSteering(1f);
    }

    public void SteeringRelease()
    {
        carController.SetSteering(0f);
    }

    public void AccelerationDown()
    {
        carController.SetAcceleration(true);
    }

    public void AccelerationUp()
    {
        carController.SetAcceleration(false);
    }

    public void BrakeDown()
    {
        carController.SetBraking(true);
    }

    public void BrakeUp()
    {
        carController.SetBraking(false);
    }
}
