using UnityEngine;
using System.Collections;
using System;

public class Robot : MonoBehaviour
{
    [SerializeField] private GridSystem grid;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 180f;


    [SerializeField] private GameObject buildingPrefab;

    public IEnumerator Move(float steps)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position + (transform.forward * steps * grid.CellSize);
        Debug.Log($"Robot starts moving {steps} steps. {startPos} and {targetPos}");


        float distance = Vector3.Distance(startPos, targetPos);
        float duration = distance / moveSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Wait for next frame
        }
        transform.position = targetPos; // Ensure we land exactly on the spot
    }
    public IEnumerator Turn(float angle)
    {
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = transform.rotation * Quaternion.Euler(0, angle, 0);

        float duration = Mathf.Abs(angle) / rotateSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRot;
    }

    public IEnumerator Place()
    {
        Vector3 position = transform.position;

        Vector2Int gridPos = grid.GetGridPos(position);
        if(grid.IsCellEmpty(gridPos))
        {
            GameObject newBuilding = Instantiate(buildingPrefab, new Vector3(position.x, 0, position.z), Quaternion.identity);
            grid.RegisterObject(gridPos, newBuilding);
        }
        else
        {
            Debug.LogWarning("Cannot place building at " + gridPos + " - Cell is occupied!");
        }


        yield return null;
    }

    
}