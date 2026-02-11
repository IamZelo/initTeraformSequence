using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteNode : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        eventData.pointerDrag.GetComponent<BlockView>().Unlink();
        Destroy(eventData.pointerDrag);
    }
}
