using UnityEngine;

/// <summary>
/// Creates invisible physical boundaries based on the camera view
/// </summary>
public class DiceBounds : MonoBehaviour
{
    public float wallThickness = 1f;

    void Start()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        CreateWall(new Vector3(0, height / 2 + wallThickness / 2, 0), new Vector3(width, wallThickness, 1)); // Top
        CreateWall(new Vector3(0, -height / 2 - wallThickness / 2, 0), new Vector3(width, wallThickness, 1)); // Bottom
        CreateWall(new Vector3(-width / 2 - wallThickness / 2, 0, 0), new Vector3(wallThickness, height, 1)); // Left
        CreateWall(new Vector3(width / 2 + wallThickness / 2, 0, 0), new Vector3(wallThickness, height, 1)); // Right
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = new GameObject("Wall");
        wall.transform.position = position;
        wall.transform.localScale = scale;

        BoxCollider collider = wall.AddComponent<BoxCollider>();

        Rigidbody rb = wall.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }
}