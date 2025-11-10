using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Only allow a drop if the slot is empty
        if (transform.childCount == 0)
        {
            // Set the parentAfterDrag variable in the DraggableItem script
            // This tells the DraggableItem where to snap when OnEndDrag is called.
            eventData.pointerDrag.GetComponent<DraggableItem>().parentAfterDrag = transform;
        }
    }
}