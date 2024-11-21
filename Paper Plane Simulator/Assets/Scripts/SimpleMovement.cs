using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of horizontal movement
    public float upwardForce = 10f; // Force applied when holding space

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down arrow keys

        // Calculate movement direction
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed;

        // Apply horizontal movement
        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = movement.x;
        newVelocity.z = movement.z;
        rb.linearVelocity = newVelocity;

        // Apply constant upward force when holding Space
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Force);
        }
    }
}
