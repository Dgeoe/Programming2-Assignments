using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public Rigidbody targetRb;
    public PaperPlaneMovement planeScript; // Assign in inspector

    [Header("Positioning")]
    public Vector3 offset = new Vector3(0f, 4f, -25f); // Static offset, no lerp

    [Header("Follow & Rotation")]
    public float smoothTime = 0.3f;
    public float rotationSpeed = 5f;

    [Header("Dynamic Look Ahead")]
    public float normalLookAhead = 0.8f;
    public float boostLookAhead = 0.1f;
    public float lookAheadLerpSpeed = 2f;

    private float currentLookAhead;
    private Vector3 currentVelocity;

    [Header("FOV Boost Effect")]
    public Camera cam;
    public float normalFOV = 60f;
    public float boostFOV = 120f;
    public float fovLerpSpeed = 4f;

    [Header("Shake Settings")]
    public bool enableShake = true;
    public float shakeAmount = 0.05f;
    public float shakeSpeed = 20f;

    private float shakeTimer = 0f;

    void Start()
    {
        currentLookAhead = normalLookAhead;
        if (!cam) cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!target || !planeScript) return;

        bool isBoosting = planeScript.IsBoosting();

        // Lerp velocity lookahead for smoother camera focus during boost
        float targetLookAhead = isBoosting ? boostLookAhead : normalLookAhead;
        currentLookAhead = Mathf.Lerp(currentLookAhead, targetLookAhead, Time.deltaTime * lookAheadLerpSpeed);

        // Predictive camera offset
        Vector3 velocityOffset = targetRb ? targetRb.linearVelocity * currentLookAhead : Vector3.zero;
        Vector3 desiredPosition = target.position + target.TransformDirection(offset) + velocityOffset;

        // Optional shake during boost
        if (enableShake && isBoosting)
        {
            shakeTimer += Time.deltaTime * shakeSpeed;
            Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
            desiredPosition += shakeOffset;
        }
        else
        {
            shakeTimer = 0f;
        }

        // Smooth position update
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);

        // Smooth rotation to look ahead
        Quaternion desiredRotation = Quaternion.LookRotation((target.position + velocityOffset) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        // FOV transition during boost
        if (cam)
        {
            float targetFOV = isBoosting ? boostFOV : normalFOV;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
        }
    }
}