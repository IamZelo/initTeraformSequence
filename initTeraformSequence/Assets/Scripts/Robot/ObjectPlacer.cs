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
            cursorHighlight = Instantiate(cursorHighlightPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            cursorHighlight.SetActive(false);
        }
    }

    //private void Update()
    //{
    //    // 1. Get the position of THIS object (the builder/player/ghost)
    //    Vector3 currentPos = transform.position;

    //    // 2. Calculate the grid position based on where THIS object is standing

    //    Vector2Int gridPos = gridSystem.GetGridPos(currentPos);
    //    Vector3 worldCenter = gridSystem.GetWorldPos(gridPos);

    //    // 3. Move the highlight cursor to the snapped position
    //    if (cursorHighlight != null)
    //    {
    //        cursorHighlight.transform.position = worldCenter;
    //        cursorHighlight.SetActive(true);
    //    }

    //}

    public void CursorShow(Vector3 pos)
    {
        Vector2Int gridPos = gridSystem.GetGridPos(pos);
        Vector3 worldCenter = gridSystem.GetWorldPos(gridPos);
        if (cursorHighlight != null)
        {
            cursorHighlight.transform.position = new Vector3(worldCenter.x, worldCenter.y + 1, worldCenter.z);
            cursorHighlight.SetActive(true);
        }
    }

    public void CursorHide() {         
        if (cursorHighlight != null)
        {
            cursorHighlight.SetActive(false);
        }
    }

}