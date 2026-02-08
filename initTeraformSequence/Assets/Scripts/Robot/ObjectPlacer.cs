using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private GameObject cursorHighlightPrefab;

    private GameObject cursorHighlight;

    private void Start()
    {
        // Instantiate the cursor highlight
        if (cursorHighlightPrefab != null)
        {
            cursorHighlight = Instantiate(cursorHighlightPrefab, Vector3.zero, Quaternion.identity);
            cursorHighlight.SetActive(true);
        }
    }

    private void Update()
    {
        // 1. Get the position of THIS object (the builder/player/ghost)
        Vector3 currentPos = transform.position;
        Debug.Log("x " + currentPos.x + " z  " + currentPos.z);


        // 2. Calculate the grid position based on where THIS object is standing
        Vector2Int gridPos = gridSystem.GetGridPos(currentPos);
        Debug.Log("grid pos" + gridPos);
        Vector3 worldCenter = gridSystem.GetWorldPos(gridPos);
        Debug.Log("x " + worldCenter.x + " z  " + worldCenter.z);

        // 3. Move the highlight cursor to the snapped position
        if (cursorHighlight != null)
        {
            cursorHighlight.transform.position = worldCenter;
            cursorHighlight.SetActive(true);
        }

    }
}