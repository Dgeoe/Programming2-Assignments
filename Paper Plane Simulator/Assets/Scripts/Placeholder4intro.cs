using UnityEngine;

public class Placeholder4intro : MonoBehaviour
{
    public GameObject objectToDestroy;  
    public GameObject objectToEnable;   

    private DisolveTest disolveTest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("player in");
            if (objectToDestroy != null)
            {
                disolveTest.StartDissolver();
            }

            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }
        }
    }
}

