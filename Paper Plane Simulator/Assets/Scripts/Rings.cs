using UnityEngine;
using System.Collections;       

public class Rings : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;         
    public float spawnDistance = 20f;  
    private Transform[] firepoints;       
    private Transform firePoint;              

    private void Start()
    {
        //select spawn direction for next ring
        Transform firepointsParent = transform.Find("Firepoints");

        if (firepointsParent != null)
        {
            int count = firepointsParent.childCount;
            firepoints = new Transform[count];

            for (int i = 0; i < count; i++)
            {
                firepoints[i] = firepointsParent.GetChild(i);
            }
            firePoint = firepoints[Random.Range(0, count)];
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 spawnPosition = firePoint.position + firePoint.forward * spawnDistance;
            Instantiate(objectToSpawn, spawnPosition, objectToSpawn.transform.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(firePoint.position, firePoint.position + firePoint.forward * spawnDistance);
            Gizmos.DrawSphere(firePoint.position + firePoint.forward * spawnDistance, 0.1f);
        }
    }
}
