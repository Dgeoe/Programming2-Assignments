using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene reset

public class WaypointChaser : MonoBehaviour
{
    // Waypoints
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    // Movement variables
    public float speed = 5f;
    public float arrivalDistance = 0.5f;
    public float rotationSpeed = 5f;

    // Raycasting (Cone/Cylinder detection)
    public Transform firingPoint;
    public float baseRaycastDistance = 10f;
    private float extendedRaycastDistance; // 4x the base distance
    public int numRays = 20;  // Number of rays to form a cone
    public float spreadAngle = 60f;  // Spread angle of the cone

    private bool isChasingPlayer = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        extendedRaycastDistance = baseRaycastDistance * 4; // Extend ray length
    }

    void Update()
    {
        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            ShootRaycasts();
            TravelToWaypoint();
        }
    }

    private bool ArriveAtWaypoint()
    {
        return Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= arrivalDistance;
    }

    private void TravelToWaypoint()
    {
        if (ArriveAtWaypoint())
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        MoveTowards(waypoints[currentWaypointIndex].position);
    }

    private void ShootRaycasts()
    {
        for (int i = 0; i < numRays; i++)
        {
            // Calculate angle for this ray
            float angleY = Random.Range(-spreadAngle, spreadAngle);  // Spread horizontally
            float angleX = Random.Range(-spreadAngle / 2, spreadAngle / 2);  // Spread vertically (smaller range)

            // Generate direction for this ray
            Vector3 direction = Quaternion.Euler(angleX, angleY, 0) * transform.forward;

            if (CastRay(firingPoint.position, direction))
            {
                isChasingPlayer = true;
            }
        }
    }

    private bool CastRay(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, extendedRaycastDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                isChasingPlayer = true; // Switch to chase mode
                return true;
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        if (player == null) return;

        RotateTowardsPlayer();
        MoveTowards(player.position);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

    // ---------------- COLLISION DETECTION ----------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetScene(); // Reset scene if enemy collides with player
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ResetScene(); // Reset scene if enemy collides with player
        }
    }

    // ---------------------- GIZMOS FOR DEBUGGING ----------------------
    private void OnDrawGizmos()
    {
        if (firingPoint == null) return;

        Gizmos.color = Color.red;

        for (int i = 0; i < numRays; i++)
        {
            float angleY = Random.Range(-spreadAngle, spreadAngle);
            float angleX = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Vector3 direction = Quaternion.Euler(angleX, angleY, 0) * transform.forward;

            Gizmos.DrawRay(firingPoint.position, direction * extendedRaycastDistance);
        }

        // Waypoint visualization
        Gizmos.color = Color.green;
        foreach (var waypoint in waypoints)
        {
            if (waypoint != null)
            {
                Gizmos.DrawSphere(waypoint.position, 0.3f);
            }
        }
    }
}

