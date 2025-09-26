using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform lastSlot; // ostatni poprawny slot

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastSlot = transform.parent; // zapamiêtaj, gdzie by³
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // jeœli nie trafi w slot ? wraca
        if (transform.parent == canvas.transform)
        {
            ResetToLastSlot();
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void SetSlot(InventorySlot slot)
    {
        transform.SetParent(slot.transform);
        rectTransform.anchoredPosition = Vector2.zero;
        lastSlot = slot.transform; // zapamiêtaj nowy slot
    }

    public void ResetToLastSlot()
    {
        transform.SetParent(lastSlot);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
