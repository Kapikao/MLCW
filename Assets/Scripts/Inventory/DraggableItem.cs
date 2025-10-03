using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int itemWidth = 1;
    public int itemHeight = 1;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private List<InventorySlot> occupiedSlots = new List<InventorySlot>();
    private List<InventorySlot> lastSlots = new List<InventorySlot>(); // zapamiêtane poprawne sloty

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // zapamiêtaj poprzednie sloty, zanim je wyczyœcisz
        lastSlots = new List<InventorySlot>(occupiedSlots);

        ClearSlots();
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
        // jeœli nie przypisano nowych slotów -> wróæ do poprzednich
        if (occupiedSlots.Count == 0)
        {
            ResetToLastSlot();
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void SetSlots(List<InventorySlot> slots)
    {
        ClearSlots();
        occupiedSlots = slots;
        lastSlots = new List<InventorySlot>(slots); // zapamiêtaj now¹ poprawn¹ pozycjê

        // ustaw parenta na lewy górny slot
        if (slots.Count > 0)
        {
            transform.SetParent(slots[0].transform);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void ResetToLastSlot()
    {
        if (lastSlots.Count > 0)
        {
            transform.SetParent(lastSlots[0].transform);
            rectTransform.anchoredPosition = Vector2.zero;
            occupiedSlots = new List<InventorySlot>(lastSlots);
        }
    }

    private void ClearSlots()
    {
        occupiedSlots.Clear();
    }
}
