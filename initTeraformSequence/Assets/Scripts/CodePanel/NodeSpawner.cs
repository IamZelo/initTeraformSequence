using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSpawner : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] BlockView prefab;
    [SerializeField] RectTransform parentForSpawned;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SpawnNew();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SpawnNew();
        //if (eventData.pointerDrag == null)
        //    return;
    }

    void SpawnNew()
    {
        BlockView node = Instantiate(prefab, transform);

        RectTransform rect = node.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = Vector3.one;
        rect.localRotation = Quaternion.identity;
        rect.SetParent(parentForSpawned, true);
    }
}
