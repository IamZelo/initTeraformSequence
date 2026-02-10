using UnityEngine;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridSystem gridSystem;

    [Header("Prefabs")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject flagPrefab;
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private GameObject mudPrefab;

    [Header("Settings")]
    public float padding = 0.5f; // Small offset to center objects


    void Start()
    {
        LoadLevel("Level1");
    }
    public void LoadLevel(string levelName)
    {
        // 1. Load the text file from the Resources folder
        TextAsset levelFile = Resources.Load<TextAsset>(levelName);

        if (levelFile == null)
        {
            Debug.LogError($"Level file '{levelName}' not found in Resources folder!");
            return;
        }

        // 2. Clear existing level (Optional, if you have a cleanup function)
        // gridSystem.ClearAll(); 

        // 3. Split the text into lines (Rows)
        string[] rows = levelFile.text.Split('\n');

        // 4. Loop through rows (Height)
        // We iterate backwards or forwards depending on how you want "Up" to look
        // Usually: Line 0 is the "Top" of the map (High Z value)
        int height = rows.Length;

        for (int y = 0; y < height; y++)
        {
            string row = rows[y].Trim(); // Remove accidental spaces

            // Loop through characters (Width)
            for (int x = 0; x < row.Length; x++)
            {
                char tileType = row[x];

                // Calculate Grid Coordinate
                // Invert Y so the text file matches the 3D world (Top of file = High Z)
                Vector2Int gridPos = new Vector2Int(x, height - 1 - y);

                SpawnObject(tileType, gridPos);
            }
        }
    }

    void SpawnObject(char type, Vector2Int pos)
    {
        Vector3 worldPos = gridSystem.GetWorldPos(pos);
        GameObject objToSpawn = null;
        bool isObstacle = false;

        switch (type)
        {
            case 'X': // Rock
                objToSpawn = rockPrefab;
                isObstacle = true;
                break;
            case 'F': // Flag
                objToSpawn = flagPrefab;
                // Flags are usually NOT obstacles so you can stand on them
                break;
            case 'S': // Start (Move the Robot here)
                SpawnRobot(worldPos);
                break;
            case 'M': // Mud
                objToSpawn = mudPrefab;
                // Mud might slow you down, but is it an obstacle?
                break;
            
            case '.': // Empty Grass
                // Do nothing
                break;


        }

        if (objToSpawn != null)
        {
            GameObject newObj = Instantiate(objToSpawn, worldPos, Quaternion.identity);

            // IMPORTANT: Register obstacles so the robot can't walk through them
            if (isObstacle)
            {
                gridSystem.RegisterObject(pos, newObj);
            }
        }
    }

    void SpawnRobot(Vector3 targetPosition)
    {
        if (robotPrefab != null)
        {
            // FOUND IT: Just teleport it to the new start spot
            // We use transform.position to snap it instantly
            robotPrefab.transform.position = new Vector3(targetPosition.x, targetPosition.y + 20, targetPosition.z);

            // Optional: Reset rotation to face forward (North)
            robotPrefab.transform.rotation = Quaternion.identity;

            Debug.Log("Moved existing Robot to start position.");
        }
        else
        {
            Debug.LogError("No Robot in scene AND no RobotPrefab assigned!");
        }
    }
}