using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int x;
    public int y;

    private Image image;
    private InventoryUI inventory;

    private Color normalColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    private Color highlightOk = new Color(0.3f, 1f, 0.3f, 1f);
    private Color highlightBad = new Color(1f, 0.3f, 0.3f, 1f);

    private void Awake()
    {
        image = GetComponent<Image>();
        if (image != null)
            image.color = normalColor;
    }

    public void Init(InventoryUI inv, int px, int py)
    {
        inventory = inv;
        x = px;
        y = py;
    }

    public bool HasItem()
    {
        return transform.childCount > 0;
    }

    public bool CanPlaceItem(DraggableItem item)
    {
        // sprawdü granice
        if (x + item.itemWidth > inventory.width || y + item.itemHeight > inventory.height)
            return false;

        // sprawdü kaødy slot
        for (int ix = 0; ix < item.itemWidth; ix++)
        {
            for (int iy = 0; iy < item.itemHeight; iy++)
            {
                InventorySlot slot = inventory.GetSlot(x + ix, y + iy);
                if (slot.HasItem())
                    return false;
            }
        }
        return true;
    }

    public List<InventorySlot> GetSlotsForItem(DraggableItem item)
    {
        List<InventorySlot> area = new List<InventorySlot>();
        for (int ix = 0; ix < item.itemWidth; ix++)
        {
            for (int iy = 0; iy < item.itemHeight; iy++)
            {
                InventorySlot slot = inventory.GetSlot(x + ix, y + iy);
                if (slot != null)
                    area.Add(slot);
            }
        }
        return area;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableItem item = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (item != null)
            {
                if (CanPlaceItem(item))
                {
                    item.SetSlots(GetSlotsForItem(item));
                }
                else
                {
                    item.ResetToLastSlot();
                }
            }
        }
        ResetColor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image == null) return;
        if (eventData.pointerDrag == null) return;

        DraggableItem item = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (item == null) return;

        List<InventorySlot> slots = GetSlotsForItem(item);
        Color col = CanPlaceItem(item) ? highlightOk : highlightBad;

        foreach (var s in slots)
        {
            var img = s.GetComponent<Image>();
            if (img != null) img.color = col;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetColor();
    }

    private void ResetColor()
    {
        // resetuje wszystkie sloty do normalnego koloru
        foreach (Transform child in inventory.transform)
        {
            var slot = child.GetComponent<InventorySlot>();
            if (slot != null && slot.image != null)
                slot.image.color = normalColor;
        }
    }
}
