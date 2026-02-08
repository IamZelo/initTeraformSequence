using UnityEngine;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float cellSize = 10f; // Size of one grid square (SimCity style)
    [SerializeField] private GameObject plane;

    // Store placed objects using grid coordinates as the key
    private Dictionary<Vector2Int, GameObject> placedObjects = new Dictionary<Vector2Int, GameObject>();

    // Convert World Position -> Grid Coordinate (e.g., 15.5 -> 1)
    public Vector2Int GetGridPos(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int z = Mathf.FloorToInt(worldPosition.z / cellSize);
        return new Vector2Int(x, z);
    }
    public float CellSize => cellSize;

    // Convert Grid Coordinate -> World Position (Centered)
    public Vector3 GetWorldPos(Vector2Int gridPos)
    {
        float x = gridPos.x * cellSize;
        float z = gridPos.y * cellSize;

        // Add half size to center the object in the square
        return new Vector3(x + (cellSize * 0.5f), 0, z + (cellSize * 0.5f));
    }

    public bool IsCellEmpty(Vector2Int gridPos)
    {
        return !placedObjects.ContainsKey(gridPos);
    }

    public void RegisterObject(Vector2Int gridPos, GameObject obj)
    {
        if (IsCellEmpty(gridPos))
        {
            placedObjects.Add(gridPos, obj);
        }
    }

    // Debug visualization to see the grid in the Scene view
    private void OnDrawGizmos()
    {
        float offset = 0.2f;
        if(cellSize < 1f)
        {
            return;
        }

        Gizmos.color = Color.green;

        int planeSizeX = Mathf.FloorToInt(plane.transform.localScale.x * 10f / 2);
        int planeSizeZ = Mathf.FloorToInt(plane.transform.localScale.z * 10f / 2);
        int negativePlaneSizeX = -planeSizeX;
        int negativePlaneSizeZ = -planeSizeZ;

        for (int x = negativePlaneSizeX; x < planeSizeX; x += (int)cellSize)
        {
            Gizmos.DrawLine(new Vector3(x, offset, negativePlaneSizeZ), new Vector3(x, offset, planeSizeZ));
            Gizmos.DrawLine(new Vector3(negativePlaneSizeX, offset, x), new Vector3(planeSizeX, offset, x));
        }
    }
}