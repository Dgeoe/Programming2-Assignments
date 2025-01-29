using UnityEngine;

public class FanBoost : MonoBehaviour
{
    [Header("Fan Settings")]
    [Tooltip("The strength of the upward force.")]
    public float upwardForce = 5f;

    [Tooltip("Should the force be constant or gradually increase over time?")]
    public bool gradualForce = false;

    [Tooltip("Maximum upward force if gradual force is enabled.")]
    public float maxUpwardForce = 10f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (gradualForce)
            {
                float appliedForce = Mathf.Min(upwardForce * Time.deltaTime, maxUpwardForce);
                rb.AddForce(Vector3.up * appliedForce, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.up * upwardForce, ForceMode.Acceleration);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Optional: Add some effect or log when an object enters the fan
        Debug.Log($"{other.name} entered the fan's area of effect.");
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Add some effect or log when an object exits the fan
        Debug.Log($"{other.name} exited the fan's area of effect.");
    }
}
