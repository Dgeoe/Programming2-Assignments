using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of horizontal movement (WASD)
    public float upwardForce = 10f; // (HOLD SPACE)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical");   

        // Calculate movement direction
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed;

        // Very Basic 3D movement horizontal movement
        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = movement.x;
        newVelocity.z = movement.z;
        rb.linearVelocity = newVelocity;

        // Constant Upward force 
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Force);
        }
    }
}
