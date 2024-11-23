using UnityEngine;

public class PaperAirplaneMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;   // Constant forward speed
    public float turnSpeed = 2f;     // Turning speed
    public float glideDescentRate = 0.5f; // How fast the plane descends

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Gravity is manually controlled
    }

    void FixedUpdate()
    {
        // Always move forward
        rb.linearVelocity = transform.forward * forwardSpeed + Vector3.down * glideDescentRate;

        // Handle turning (yaw and roll)
        float turn = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float pitch = Input.GetAxis("Vertical");  // W/S or Up/Down

        // Apply rotation
        Vector3 rotation = new Vector3(-pitch, turn, -turn * 0.5f) * turnSpeed; // Simulate rolling when turning
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.fixedDeltaTime));
    }
}
