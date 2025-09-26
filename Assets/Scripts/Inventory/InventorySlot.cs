using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int x;
    public int y;

    private Image image;

    private Color normalColor = new Color(0.7f, 0.7f, 0.7f, 1f);  // szary
    private Color highlightOk = new Color(0.3f, 1f, 0.3f, 1f);    // zielony
    private Color highlightBad = new Color(1f, 0.3f, 0.3f, 1f);   // czerwony

    private void Awake()
    {
        image = GetComponent<Image>();
        if (image != null)
            image.color = normalColor;
    }

    public bool HasItem()
    {
        return transform.childCount > 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableItem item = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (item != null)
            {
                if (!HasItem())
                {
                    item.SetSlot(this);
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

        if (eventData.pointerDrag != null)
        {
            if (!HasItem())
                image.color = highlightOk;   // zielony
            else
                image.color = highlightBad;  // czerwony
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetColor();
    }

    private void ResetColor()
    {
        if (image != null)
            image.color = normalColor;
    }
}
