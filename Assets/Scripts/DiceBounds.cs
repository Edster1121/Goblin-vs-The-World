using UnityEngine;

/// <summary>
/// Creates 3D walls around the screen to keep dice inside
/// </summary>
public class DiceBounds : MonoBehaviour
{
    public float wallThickness = 1f;
    public float wallDepth = 10f;

    void Start()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        
        float zPos = 0.5f;

        CreateWall(new Vector3(0, height / 2 + wallThickness / 2, zPos), new Vector3(width, wallThickness, wallDepth)); // Top
        CreateWall(new Vector3(0, -height / 2 - wallThickness / 2, zPos), new Vector3(width, wallThickness, wallDepth)); // Bottom
        CreateWall(new Vector3(-width / 2 - wallThickness / 2, 0, zPos), new Vector3(wallThickness, height, wallDepth)); // Left
        CreateWall(new Vector3(width / 2 + wallThickness / 2, 0, zPos), new Vector3(wallThickness, height, wallDepth)); // Right
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = new GameObject("Wall");
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.AddComponent<BoxCollider>();
    }
}