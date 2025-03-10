using UnityEngine;
using UnityEngine.SceneManagement; 

public class CrumpleScript : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] crumpledVertices;

    public float crumpleIntensity = 0.0055f; 
    public float crumpleSmoothness = 1f;
    public float sphereFactor = 0.0005f;

    //How long to transition
    public float crumpleDuration = 1f; 
    private float crumpleTimer = 0f;
    private bool isCrumpling = false;

    public GameObject sphereObject; //I will fix this later but for now this is an easy fix 

    private MeshRenderer meshRenderer;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        crumpledVertices = new Vector3[originalVertices.Length];

        meshRenderer = GetComponent<MeshRenderer>();
        if (sphereObject != null)
            sphereObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isCrumpling)
        {
            isCrumpling = true;
            crumpleTimer = 0f; // Reset timer
        }
    }

    void Update()
    {
        if (isCrumpling)
        {
            crumpleTimer += Time.deltaTime;

            // Calculate how far along the transition is
            float progress = Mathf.Clamp01(crumpleTimer / crumpleDuration);

            //Crumple
            Crumple(progress);

            if (progress >= 1f)
            {
                FinalizeCrumpling();
            }
        }
    }

    /// Crumples the mesh over time based on the progress (0 to 1).
    void Crumple(float progress)
    {
        Vector3 meshCenter = mesh.bounds.center;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];

            float noise = Mathf.PerlinNoise(vertex.x * crumpleSmoothness, vertex.z * crumpleSmoothness);
            Vector3 randomDirection = Random.onUnitSphere;
            Vector3 displacement = randomDirection * noise * crumpleIntensity * progress;
            Vector3 towardCenter = (vertex - meshCenter).normalized * sphereFactor * progress;

            crumpledVertices[i] = vertex + displacement + towardCenter;
        }

        mesh.vertices = crumpledVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    /// Enables the sphere fix, disables the mesh, and resets the scene.
    void FinalizeCrumpling()
    {
        isCrumpling = false;
        if (meshRenderer != null)
            meshRenderer.enabled = false;

        if (sphereObject != null)
            sphereObject.SetActive(true);

        Invoke(nameof(ResetScene), 2f);
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
