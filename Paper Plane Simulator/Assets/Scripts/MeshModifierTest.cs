using UnityEngine;

public class MeshModifierTest : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] crumpledVertices;

    [Header("Crumple Physics")]
    public float crumpleIntensity = 1f;  // How much vertices are displaced on impact
    public float crumpleSmoothness = 1f; // lower = more jagged
    public float sphereFactor = 0.5f;    // Pulls the mesh into a perfect ball/sphere shape at centre when = 1f 

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        crumpledVertices = new Vector3[originalVertices.Length];

        Crumple();
    }

    void Crumple()
    {
        Vector3 meshCenter = mesh.bounds.center;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];

            // Calculate a random noise based on position
            float noise = Mathf.PerlinNoise(vertex.x * crumpleSmoothness, vertex.z * crumpleSmoothness);

            // Displace the vertex 
            Vector3 randomDirection = Random.onUnitSphere; // Random direction
            Vector3 displacement = randomDirection * noise * crumpleIntensity;

            // Make vertices sphereified (crumple the paper into shape)
            Vector3 towardCenter = (vertex - meshCenter).normalized * sphereFactor;
            crumpledVertices[i] = vertex + displacement + towardCenter;
        }

        // Update the mesh 
        mesh.vertices = crumpledVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    // for future trsting below 
    /// Visualize crumple effect in real time 
    void OnValidate()
    {
        if (mesh == null) return;

        Crumple();
    }
}
