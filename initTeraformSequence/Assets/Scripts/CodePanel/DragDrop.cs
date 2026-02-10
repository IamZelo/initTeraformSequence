using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField] private Transform plane;
    [SerializeField] private Canvas canvas;
    [SerializeField] private BlockView view;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        view = GetComponent<BlockView>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
        view.Unlink();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        rectTransform.anchoredPosition += delta;

        MoveFollowers(view.NextView);
    }

    void MoveFollowers(BlockView follower)
    {
        int count = 1;
        while (follower != null)
        {
            follower.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition + (new Vector2(0, -71) * count);
            count++;
            follower = follower.NextView;
        }
    }
}
