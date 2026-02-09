using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        
        BlockView dragged = eventData.pointerDrag.GetComponent<BlockView>();
        BlockView parent = transform.parent.GetComponent<BlockView>();

        
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, -rectTransform.rect.height);

        parent.LinkNext(dragged);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            canvasGroup.alpha = 0.5f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = 0f;
    }
}
