using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem; // REQUIRED FOR NEW INPUT SYSTEM

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // --- Public Variables ---
    public Transform parentAfterDrag; // Set by the ItemSlot script on drop

    // --- Private Variables ---
    private Image image;
    private CanvasGroup group;

    // Reference for the New Input System's mouse position action
    private InputAction mouseMove;

    private void Start()
    {
        // Get required components
        image = GetComponent<Image>();
        group = GetComponent<CanvasGroup>();

        // Ensure both components exist before continuing
        if (image == null || group == null)
        {
            Debug.LogError("DraggableItem script requires both an Image and a CanvasGroup component on the GameObject.", this);
            enabled = false; // Disable the script if setup is wrong
            return;
        }

        // Initialize the New Input System Action
        mouseMove = InputSystem.actions.FindAction("Point");
        if (mouseMove == null)
        {
            Debug.LogError("Input Action 'Point' not found. Check your Input Action Asset.", this);
            enabled = false;
            return;
        }
        mouseMove.Enable(); // Enable the action on start
    }

    // Called when the drag operation begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 1. Store the original parent
        parentAfterDrag = transform.parent;

        // 2. Reparent to the Canvas Root to ensure it renders over EVERYTHING
        transform.SetParent(transform.root);
        transform.SetAsLastSibling(); // Ensure it's the last child of the Canvas

        // 3. Set visual state for dragging
        group.alpha = 0.6f;
        image.raycastTarget = false; // Disable raycast so the ItemSlot can receive the drop event
    }

    // Called every frame while the item is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        // FIX for New Input System: Read the position directly from the enabled action
        Vector2 pos = mouseMove.ReadValue<Vector2>();
        transform.position = pos;
    }

    // Called when the drag operation ends (mouse button released)
    public void OnEndDrag(PointerEventData eventData)
    {
        // 1. Set the item back to the stored parent (either the original parent or the ItemSlot)
        transform.SetParent(parentAfterDrag);

        // 2. CRITICAL HIERARCHY FIX: Set the item to be the last sibling of its new parent
        //    This ensures the item is drawn ON TOP of the slot, not underneath it.
        transform.SetAsLastSibling();

        // 3. Reset visual state
        group.alpha = 1f;
        image.raycastTarget = true; // Re-enable raycast for future drags
    }
}