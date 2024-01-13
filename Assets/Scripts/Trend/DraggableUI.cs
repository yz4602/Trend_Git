using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private RectTransform rectTransform;
	private Canvas canvas;
	private CanvasGroup canvasGroup;
	public RectTransform restrictedArea;

	public static Rect snapArea;

	public static List<Vector2> snapPositions = new List<Vector2>(); // List of snap positions
	public static Dictionary<Vector2, GameObject> itemSlotPositions = new Dictionary<Vector2, GameObject>();
	public static int numOfTrendInArea;

	private Vector2 tempVector;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		canvasGroup = gameObject.AddComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		SoundMgr.Instance.PlaySound("select");
		canvasGroup.blocksRaycasts = false;
		transform.SetAsLastSibling();
		tempVector = this.rectTransform.anchoredPosition;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 proposedPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

		if (IsPositionValid(proposedPosition))
		{
			rectTransform.anchoredPosition = proposedPosition;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = true;

		// Calculate the rectangle bounds of the RectTransform
		Rect rectTransformBounds = GetRectTransformBounds(rectTransform);

		if (snapArea.Overlaps(rectTransformBounds))
		{
			SoundMgr.Instance.PlaySound("snap");
			Vector2 newSnapPosition = FindClosestSnapPosition();
			GameObject imageInNewSlot;

			Vector2 originalPosition = tempVector;

			if (itemSlotPositions.TryGetValue(newSnapPosition, out imageInNewSlot))
			{
				if (imageInNewSlot != null)
				{
					Debug.Log("nn " + imageInNewSlot.name);
					imageInNewSlot.GetComponent<RectTransform>().anchoredPosition = originalPosition;
					itemSlotPositions[originalPosition] = imageInNewSlot;
					//itemSlotPositions.Remove(originalPosition);
				}
				else
				{
					numOfTrendInArea++;
					//Debug.Log("numOfTrendInArea: " + numOfTrendInArea);
					itemSlotPositions.Remove(originalPosition);
				}
			}
			else
			{
				numOfTrendInArea++;//FIXME: numOfTrendInArea
				Debug.Log("numOfTrendInArea: " + numOfTrendInArea);
				itemSlotPositions.Remove(originalPosition);
			}

			rectTransform.anchoredPosition = newSnapPosition;
			itemSlotPositions[newSnapPosition] = gameObject;
		}
		else
		{
			// Optional: Reset to the original position or take another action
			// rectTransform.anchoredPosition = tempVector;
			if(snapArea.Contains(tempVector))
			{
				numOfTrendInArea--;
				Debug.Log("numOfTrendInArea: " + numOfTrendInArea);
			}
			itemSlotPositions.Remove(tempVector);
		}
	}

	private bool IsPositionValid(Vector2 position)
	{
		Rect elementRect = GetRectTransformBounds(rectTransform);	
		
		if (IsMovingAwayFromRestrictedArea(position))
		{
			return true;
		}
		// Check if the elementRect overlaps with the restrictedArea
		return !elementRect.Overlaps(GetRectTransformBounds(restrictedArea));
	}
	
	private bool IsMovingAwayFromRestrictedArea(Vector2 position)
	{
		return position.y <= rectTransform.anchoredPosition.y;
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

		return closest;
	}

	Rect GetRectTransformBounds(RectTransform rectTransform)
	{
		Vector2 size = rectTransform.sizeDelta;
		Vector2 position = rectTransform.anchoredPosition;

		// Calculate the min and max positions
		float xMin = position.x - size.x * rectTransform.pivot.x;
		float xMax = position.x + size.x * (1 - rectTransform.pivot.x);
		float yMin = position.y - size.y * rectTransform.pivot.y;
		float yMax = position.y + size.y * (1 - rectTransform.pivot.y);

		return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
	}
	
	public void SetCanvas(Canvas canvas)
	{
		this.canvas = canvas;
	}

	// void OnDrawGizmos()
	// {
	// 	if (canvas == null)
	// 		return;

	// 	// Set the color of the Gizmo
	// 	Gizmos.color = Color.green;

	// 	// Convert the snapArea from Rect to world space
	// 	Vector3[] worldCorners = new Vector3[4];
	// 	Vector3 size = new Vector3(snapArea.width, snapArea.height, 0);
	// 	Vector3 center = new Vector3(snapArea.x + snapArea.width / 2, snapArea.y + snapArea.height / 2, 0);

	// 	// Transform center from local to world space
	// 	center = canvas.transform.TransformPoint(center);

	// 	// Get the world corners of the rectangle
	// 	worldCorners[0] = canvas.transform.TransformPoint(new Vector3(snapArea.xMin, snapArea.yMin, 0));
	// 	worldCorners[1] = canvas.transform.TransformPoint(new Vector3(snapArea.xMax, snapArea.yMin, 0));
	// 	worldCorners[2] = canvas.transform.TransformPoint(new Vector3(snapArea.xMax, snapArea.yMax, 0));
	// 	worldCorners[3] = canvas.transform.TransformPoint(new Vector3(snapArea.xMin, snapArea.yMax, 0));

	// 	// Draw the rectangle
	// 	Gizmos.DrawLine(worldCorners[0], worldCorners[1]);
	// 	Gizmos.DrawLine(worldCorners[1], worldCorners[2]);
	// 	Gizmos.DrawLine(worldCorners[2], worldCorners[3]);
	// 	Gizmos.DrawLine(worldCorners[3], worldCorners[0]);
	// }
	
	void OnDrawGizmos()
	{
		if (canvas == null)
			return;
		
		Rect resArea = GetRectTransformBounds(restrictedArea);

		// Set the color of the Gizmo
		Gizmos.color = Color.green;

		// Convert the snapArea from Rect to world space
		Vector3[] worldCorners = new Vector3[4];
		Vector3 size = new Vector3(resArea.width, resArea.height, 0);
		Vector3 center = new Vector3(resArea.x + resArea.width / 2, resArea.y + resArea.height / 2, 0);

		// Transform center from local to world space
		center = canvas.transform.TransformPoint(center);

		// Get the world corners of the rectangle
		worldCorners[0] = canvas.transform.TransformPoint(new Vector3(resArea.xMin, resArea.yMin, 0));
		worldCorners[1] = canvas.transform.TransformPoint(new Vector3(resArea.xMax, resArea.yMin, 0));
		worldCorners[2] = canvas.transform.TransformPoint(new Vector3(resArea.xMax, resArea.yMax, 0));
		worldCorners[3] = canvas.transform.TransformPoint(new Vector3(resArea.xMin, resArea.yMax, 0));

		// Draw the rectangle
		Gizmos.DrawLine(worldCorners[0], worldCorners[1]);
		Gizmos.DrawLine(worldCorners[1], worldCorners[2]);
		Gizmos.DrawLine(worldCorners[2], worldCorners[3]);
		Gizmos.DrawLine(worldCorners[3], worldCorners[0]);
	}

}
