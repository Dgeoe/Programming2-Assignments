using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;       

public class Rings : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;         
    public float spawnDistance = 20f;  
    private Transform[] firepoints;       
    private Transform firePoint;   

    public string sceneToLoad;  // Set in inspector
    private string saveFilePath;
    private TimeTrialStuff progress;

    [Header("Ring Count")]
    public int Ringx;
    public Animator loopmasker; 


    private void Start()
    {
        // Setup JSON file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "player_progress.json");

        LoadProgress();

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

        Ringx = progress.Ringx;

    }

    private void Update()
    {
        if (progress.Ringx >= 3)
        {
            Debug.Log("its at");
            Debug.Log(progress.Ringx);
            //SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in");
            // Increment Ringx and save
            progress.Ringx++;
            Debug.Log(progress.Ringx);
            Ringx = progress.Ringx;
            SaveProgress();

            // Spawn object
            if (progress.Ringx < 3)
            {
                Vector3 spawnPosition = firePoint.position + firePoint.forward * spawnDistance;
                Instantiate(objectToSpawn, spawnPosition, objectToSpawn.transform.rotation);
            }
            // Check condition
            else if (progress.Ringx >= 3)
            {
                StartCoroutine(PlayAnimationThenLoadScene());
            }
            else
            {
                Debug.Log("Ring Counter Broke");
            }
        }
    }

    private IEnumerator PlayAnimationThenLoadScene()
    {
        if (loopmasker != null)
        {
            loopmasker.SetBool("Ring", true);
            float animLength = loopmasker.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("loopmasker not assigned!");
        }
    }

    private void LoadProgress()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            progress = JsonUtility.FromJson<TimeTrialStuff>(json);
        }
        else
        {
            progress = new TimeTrialStuff();
            SaveProgress();
        }
    }

    private void SaveProgress()
    {
        string json = JsonUtility.ToJson(progress);
        File.WriteAllText(saveFilePath, json);
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
