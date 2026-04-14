using UnityEngine;
using UnityEngine.TestTools;

public class DroneVision : MonoBehaviour
{
    public float visionRange = 2f;
    public float visionAngle = 90f;
    public int visionSegments = 24;
    public GameObject eye;

    public Color patrol = new Color(0,1,0,0.6f);
    public Color alert = new Color(1, 0, 0, 0.6f);
    public Color searching = new Color(1, 1, 0, 0.6f);

    private Color currentColor;
    private Mesh visionConeMesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentColor = patrol;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || visionConeMesh == null)
        {
            visionConeMesh = BuildVisionMesh();
        }
        Color fillColor;

        if (Application.isPlaying)
        {
            fillColor = currentColor;
        }
        else
        {
            fillColor = patrol;
        }
        Color outlineColor = new Color(fillColor.r, fillColor.g, fillColor.b, 1f);
        Gizmos.color = fillColor;
        Gizmos.DrawMesh(visionConeMesh,eye.transform.position,eye.transform.rotation);
        Gizmos.color = outlineColor;

        Vector3 origin = eye.transform.position;
        float halfAngle = visionAngle * 0.5f;
        float angleStep = visionAngle / visionSegments;
        Vector3 previousPoint = Vector3.zero;

        for(int i = 0; i < visionSegments; i++)
        {
            float angle = (-halfAngle + angleStep * i) * Mathf.Deg2Rad;
            Vector3 direction = eye.transform.rotation * new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
            Vector3 point = origin + direction * visionRange;

            if (i == 0 || i == visionSegments)
            {
                Gizmos.DrawLine(origin, point);
            }
            if (i > 0)
            {
                Gizmos.DrawLine(previousPoint, point);
            }

            previousPoint = point;
        }
    }

    public void SetPatrol()
    {
        currentColor = patrol;
    }
    public void SetSearching()
    {
        currentColor = searching;
    }
    public void SetAlerted()
    {
        currentColor = alert;
    }
    private Mesh BuildVisionMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "VisionFill";

        int vertCount = visionSegments + 2;
        Vector3[] verts = new Vector3[vertCount];
        int[] tris = new int[visionSegments * 3];

        // Vertex 0 = vision tip (local origin)
        verts[0] = Vector3.zero;

        float halfAngle = visionAngle * 0.5f;
        float angleStep = visionAngle / visionSegments;

        for (int i = 0; i <= visionSegments; i++)
        {
            float angle = (-halfAngle + angleStep * i) * Mathf.Deg2Rad;
            verts[i + 1] = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * visionRange;
        }

        for (int i = 0; i < visionSegments; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = i + 2;
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        return mesh;
    }

}
