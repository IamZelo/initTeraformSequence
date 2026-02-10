using UnityEngine;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float cellSize = 24f; // Size of one grid square (SimCity style)
    [SerializeField] private GameObject plane;

    [Header("Pre-Start Settings")]
    [SerializeField] private GameObject plantPrefab; 
    [SerializeField] private int startingPlantCount = 5;

    // Store placed objects using grid coordinates as the key
    private Dictionary<Vector2Int, GameObject> placedObjects = new Dictionary<Vector2Int, GameObject>();


    private void SpawnStartingPlants()
    {
        if (plantPrefab == null || startingPlantCount <= 0) return;

        // 1. Calculate Grid Dimensions
        float worldWidth = 10f * plane.transform.localScale.x;
        float worldDepth = 10f * plane.transform.localScale.z;

        int maxGridX = Mathf.FloorToInt(worldWidth / cellSize);
        int maxGridY = Mathf.FloorToInt(worldDepth / cellSize);

        int spawnedCount = 0;
        int attempts = 0;

        // 2. Loop until we spawn enough plants (or give up after too many tries)
        while (spawnedCount < startingPlantCount && attempts < 100)
        {
            attempts++;

            // Pick a random spot
            int randX = Random.Range(0, maxGridX);
            int randY = Random.Range(0, maxGridY);
            Vector2Int randomPos = new Vector2Int(randX, randY);

            // 3. Only place if the spot is empty
            if (IsCellEmpty(randomPos))
            {
                // Calculate world position
                Vector3 worldPos = GetWorldPos(randomPos);

                // Instantiate (Adjust Y slightly if pivot is at bottom)
                GameObject newPlant = Instantiate(plantPrefab, worldPos, Quaternion.identity);

                // Important: Add to the dictionary so the robot knows it's there!
                RegisterObject(randomPos, newPlant);

                spawnedCount++;
            }
        }
    }



    // Convert World Position -> Grid Coordinate (e.g., 15.5 -> 1)
    public Vector2Int GetGridPos(Vector3 worldPosition)
    {
        // 1. Calculate the actual size of the plane in world units
        // Unity's default plane is 10x10, so we multiply by localScale
        float worldWidth = 10f * plane.transform.localScale.x;
        float worldDepth = 10f * plane.transform.localScale.z;

        // 2. Find the Bottom-Left corner (The Grid's "Zero" point)
        // Since the pivot is in the center, we subtract half the size from the position
        float startX = plane.transform.position.x - (worldWidth / 2f);
        float startZ = plane.transform.position.z - (worldDepth / 2f);

        // 3. Calculate how far the object is from that corner
        float relativeX = worldPosition.x - startX;
        float relativeZ = worldPosition.z - startZ;

        // 4. Divide by your cell size (24) to get the index
        int x = Mathf.FloorToInt(relativeX / cellSize);
        int y = Mathf.FloorToInt(relativeZ / cellSize);

        return new Vector2Int(x, y);
    }
    public bool IsWorldPosValid(Vector3 worldPosition)
    {
        // 1. Get the current grid index for this position
        Vector2Int gridPos = GetGridPos(worldPosition);

        // 2. Calculate the maximum number of cells that fit on this plane
        // (Plane Size / Cell Size) = Total Cells
        float worldWidth = 10f * plane.transform.localScale.x;
        float worldDepth = 10f * plane.transform.localScale.z;

        int maxGridX = Mathf.FloorToInt(worldWidth / cellSize);
        int maxGridY = Mathf.FloorToInt(worldDepth / cellSize);

        // 3. Check if the index is within bounds [0 to Max-1]
        bool insideX = gridPos.x >= 0 && gridPos.x < maxGridX;
        bool insideY = gridPos.y >= 0 && gridPos.y < maxGridY;

        return insideX && insideY;
    }

    public float CellSize => cellSize;

    // Convert Grid Coordinate -> World Position (Centered)
    public Vector3 GetWorldPos(Vector2Int gridPos)
    {
        float worldWidth = 10f * plane.transform.localScale.x;
        float worldDepth = 10f * plane.transform.localScale.z;

        float startX = plane.transform.position.x - (worldWidth / 2f);
        float startZ = plane.transform.position.z - (worldDepth / 2f);

        // Start + (Index * 24) + (Half Cell Size to center it)
        float x = startX + (gridPos.x * cellSize) + (cellSize * 0.5f);
        float z = startZ + (gridPos.y * cellSize) + (cellSize * 0.5f);

        return new Vector3(x, plane.transform.position.y, z);
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

    public GameObject GetObject(Vector2Int gridPos)
    { 
        return placedObjects.GetValueOrDefault(gridPos);
    }


    public GameObject RemoveObject(Vector2Int gridPos)
    {
        if(placedObjects.TryGetValue(gridPos, out GameObject obj))
        {
            placedObjects.Remove(gridPos);
            return obj;
        }
        return null;
    }


    // Debug visualization to see the grid in the Scene view
    private void OnDrawGizmos()
    {
        float offset = 0.4f;
        if(cellSize < 1f)
        {
            return;
        }

        Gizmos.color = Color.red;

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