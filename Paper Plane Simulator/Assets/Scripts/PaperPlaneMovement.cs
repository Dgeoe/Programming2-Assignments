using UnityEngine;
using UnityEngine.InputSystem;

public class PaperPlaneMovement : MonoBehaviour
{
    public float forwardSpeed = 5f;   // Constantly applied
    public float turnSpeed = 2f;     
    public float glideDescentRate = 2f; // How fast paper plane should descends
    public PlayerInputActions playerControls;
    public bool gravity; // Turn gravity on or off
    public bool CanShoot = false; // For later upgrade, ignore for now

    private Rigidbody rb;
    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = gravity;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * forwardSpeed + Vector3.down * glideDescentRate;

        // Read input w/ new system
        moveDirection = move.ReadValue<Vector2>();
        float pitch = moveDirection.y; // Vertical input
        float turn = moveDirection.x;  // Horizontal input

        Vector3 rotation = new Vector3(-pitch, turn, -turn * 0.5f) * turnSpeed; // Simulate rolling when turning
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.fixedDeltaTime));
    }
}
