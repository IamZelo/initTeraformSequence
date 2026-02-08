using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class ObjectPlacer : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private GridSystem gridSystem;
    [SerializeField]private Camera mainCamera;
    [SerializeField]private GameObject buildingPrefab; // Drag your 3D building here
    [SerializeField]private GameObject cursorHighlightPrefab; // A semi-transparent square to show where you are aiming

    [Header("Input")]
    public LayerMask groundLayer; // Set this to the layer your "Plane" is on


    private GameObject cursorHighlight;
  
    private void Start()
    {
        // Instantiate the cursor highlight and disable it initially
        if (cursorHighlightPrefab != null)
        {
            cursorHighlight = Instantiate(cursorHighlightPrefab, new Vector3(0,0,0), Quaternion.identity);
            cursorHighlight.SetActive(true);
        }
    }
    private void Update()
    {
        // 1. Raycast from mouse to world
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            // 2. Calculate the grid position
            Vector2Int gridPos = gridSystem.GetGridPos(hit.point);
            Vector3 worldCenter = gridSystem.GetWorldPos(gridPos);

            // 3. Move the highlight cursor to the snapped position
            if (cursorHighlight != null)
            {
                cursorHighlight.transform.position = worldCenter;
                cursorHighlight.SetActive(true);
            }

            // 4. Place object on Left Click
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (gridSystem.IsCellEmpty(gridPos))
                {
                    PlaceBuilding(gridPos, worldCenter);
                }
                else
                {
                    Debug.Log("Cannot build here! Cell is occupied.");
                }
            }
        }
        else
        {
            if (cursorHighlight != null) cursorHighlight.SetActive(false);
        }
    }

    void PlaceBuilding(Vector2Int gridPos, Vector3 position)
    {
        float offset = buildingPrefab.transform.localScale.y / 2f; 
        GameObject newBuilding = Instantiate(buildingPrefab, new Vector3(position.x, position.y + offset, position.z), Quaternion.identity);
        gridSystem.RegisterObject(gridPos, newBuilding);
    }
}