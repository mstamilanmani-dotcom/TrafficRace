using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 5f, -9f);

    public float smoothSpeed = 5f;
    public float lookHeight = 1.5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position +
                                  target.rotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        Vector3 lookPosition = target.position +
                               Vector3.up * lookHeight;

        transform.LookAt(lookPosition);
    }
}
