using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public static List<Vector2> snapPositions = new List<Vector2>(); // List of snap positions
    public static Dictionary<Vector2, GameObject> itemSlotPositions = new Dictionary<Vector2, GameObject>();

    private Vector2 tempVector;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
        tempVector = this.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        Vector2 newSnapPosition = FindClosestSnapPosition();
        GameObject imageInNewSlot;

        Vector2 originalPosition = tempVector;
        // Check if the new slot is occupied
        if (itemSlotPositions.TryGetValue(newSnapPosition, out imageInNewSlot))
        {
            // Get the original position of the current image
            

            // Move the image in the new slot to the original position
            imageInNewSlot.GetComponent<RectTransform>().anchoredPosition = originalPosition;
            Debug.Log("image in new slot: " + imageInNewSlot.name);
            // Update the dictionary
            itemSlotPositions[originalPosition] = imageInNewSlot;
        }
        else
        {
            itemSlotPositions.Add(newSnapPosition, null);
            itemSlotPositions.Remove(originalPosition);
        }

        // Snap the current image to the new position and update the dictionary
        rectTransform.anchoredPosition = newSnapPosition;
        itemSlotPositions[newSnapPosition] = gameObject;
        imageInNewSlot = null;
    }


    private Vector2 FindClosestSnapPosition()
    {
        var closest = Vector2.positiveInfinity;
        var minDistance = float.MaxValue;

        foreach (var snapPosition in snapPositions)
        {
            var distance = Vector2.Distance(rectTransform.anchoredPosition, snapPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = snapPosition;
            }
        }

        if (!closest.Equals(Vector2.positiveInfinity))
        {
            rectTransform.anchoredPosition = closest;
        }

        return closest;
    }
}
