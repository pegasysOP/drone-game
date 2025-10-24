using UnityEngine;

public class DroneController : MonoBehaviour
{
    public ControlPanel controlPanel;
    public Rigidbody rb;

    [Header("Movement Settings")]
    public float verticalSpeed = 1f;
    public float forwardSpeed = 2f;
    public float rotationSpeed = 90f;
    public float accelerationSmoothing = 5f;  // How quickly to reach target speed
    public float decelerationSmoothing = 10f; // How quickly to stop when input is 0

    private void FixedUpdate()
    {
        float verticalInput = controlPanel.Vertical;
        float horizontalInput = controlPanel.Horizontal;
        float forwardInput = controlPanel.Forward;

        // movement
        Vector3 verticalMovement = Vector3.up * verticalInput * verticalSpeed;
        Vector3 forwardMovement = transform.forward * forwardInput * forwardSpeed;
        Vector3 targetVelocity = verticalMovement + forwardMovement;

        // Use faster deceleration when there's no input
        bool hasInput = verticalInput != 0 || forwardInput != 0;
        float smoothing = hasInput ? accelerationSmoothing : decelerationSmoothing;

        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * smoothing);

        // rotation
        float rotationAmount = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
