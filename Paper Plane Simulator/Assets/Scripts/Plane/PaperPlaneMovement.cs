using System.Collections;
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

    [Header("Boost Settings")]
    public float boostMultiplier = 2f;
    public float boostDuration = 2f;
    public float boostCooldown = 3f;

    private bool isBoosting = false;
    private bool WindTunnelBoost = false;
    private bool canBoost = true;
    private InputAction boostAction;

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

        boostAction = playerControls.Player.Boost;
        boostAction.Enable();
        boostAction.performed += ctx => TryBoost();
    }

    private void OnDisable()
    {
        move.Disable();
        boostAction.Disable();
        boostAction.performed -= ctx => TryBoost();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = gravity;
    }

void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * forwardSpeed + Vector3.down * glideDescentRate;

        moveDirection = move.ReadValue<Vector2>();
        float pitch = moveDirection.y;
        float turn = moveDirection.x;

        if (moveDirection != Vector2.zero)
        {
            // Apply input-based rotation
            Vector3 rotation = new Vector3(-pitch, turn, -turn * 0.5f) * turnSpeed;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.fixedDeltaTime));
        }
        else
        {
            // No input: smooth reset to upright rotation (identity in local space)
            Quaternion targetRotation = Quaternion.Euler(0f, rb.rotation.eulerAngles.y, 0f);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * 0.05f * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WindTunnel"))
        {
            StartCoroutine(HandleBoost());
        }
        else if (other.CompareTag("BoostPad"))
        {
            rb.AddForce(Vector3.up * 100f, ForceMode.Impulse); // Apply an upward impulse
        }
    }

    private IEnumerator BoostForwardSpeed()
    {
        float originalSpeed = forwardSpeed;
        float boostedSpeed = forwardSpeed * 3f;
        forwardSpeed = boostedSpeed;
        WindTunnelBoost = true;
        float duration = 3f; 
        float halfDuration = duration * 0.5f; 
        float elapsedTime = 0f;

        // Stay at full boost briefly
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Now smoothly lerp back to original speed
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            forwardSpeed = Mathf.Lerp(boostedSpeed, originalSpeed, elapsedTime / halfDuration);
            yield return null;
        }

        forwardSpeed = originalSpeed; 
    }

    private void TryBoost()
    {
        if (canBoost && !isBoosting)
        {
            StartCoroutine(HandleBoost());
        }
    }


    private IEnumerator HandleBoost()
    {
        isBoosting = true;
        canBoost = false;

        float originalSpeed = forwardSpeed;
        float boostedSpeed = forwardSpeed * boostMultiplier;
        forwardSpeed = boostedSpeed;

        float duration = boostDuration;
        float halfDuration = duration * 0.5f;
        float elapsedTime = 0f;

        // Stay at full boost briefly
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Smoothly return to normal speed
        elapsedTime = 0f;
        while (elapsedTime < halfDuration)
        {
            elapsedTime += Time.deltaTime;
            forwardSpeed = Mathf.Lerp(boostedSpeed, originalSpeed, elapsedTime / halfDuration);
            yield return null;
        }

        forwardSpeed = originalSpeed;
        isBoosting = false;

        yield return new WaitForSeconds(boostCooldown);
        canBoost = true;
    }


    public bool IsBoosting()
    {
        return isBoosting;
    }
    public bool IsWinded()
    {
        return WindTunnelBoost;
    }

}
