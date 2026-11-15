using UnityEngine;

public class MobileInputController : MonoBehaviour
{
    public PlayerCarController carController;

    private bool IsReady()
    {
        if (carController != null)
            return true;

        Debug.LogWarning("PlayerCarController is not assigned.");
        return false;
    }

    public void LeftButtonDown()
    {
        if (IsReady())
            carController.SetSteering(-1f);
    }

    public void RightButtonDown()
    {
        if (IsReady())
            carController.SetSteering(1f);
    }

    public void SteeringRelease()
    {
        if (IsReady())
            carController.SetSteering(0f);
    }

    public void AccelerationDown()
    {
        if (IsReady())
            carController.SetAcceleration(true);
    }

    public void AccelerationUp()
    {
        if (IsReady())
            carController.SetAcceleration(false);
    }

    public void BrakeDown()
    {
        if (IsReady())
            carController.SetBraking(true);
    }

    public void BrakeUp()
    {
        if (IsReady())
            carController.SetBraking(false);
    }

    public void ReverseDown()
    {
        if (IsReady())
            carController.SetReversing(true);
    }

    public void ReverseUp()
    {
        if (IsReady())
            carController.SetReversing(false);
    }
}
