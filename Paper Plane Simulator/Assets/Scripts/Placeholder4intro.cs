using UnityEngine;

public class Placeholder4intro : MonoBehaviour
{
    public GameObject objectToDestroy;  
    public GameObject objectToEnable;   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("player in");
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy);
            }

            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }
        }
    }
}

