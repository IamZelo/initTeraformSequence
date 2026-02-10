using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSpawner : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] BlockView prefab;
    [SerializeField] RectTransform parentForSpawned;


    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        SpawnNew();
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
