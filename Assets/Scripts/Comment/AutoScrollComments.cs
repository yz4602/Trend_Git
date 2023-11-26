using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AutoScrollComments : MonoBehaviour
{
	public GameObject commentPrefab;
	public float scrollSpeed = 20f;
	public List<string> commentsList = new List<string>();
	private List<GameObject> commentObjects = new List<GameObject>();
	private float commentHeight;

	void Start()
	{
		// Example comments
		// List<string> comments = new List<string> { "Comment 1", "Comment 2", "Comment 3",
		// "Comment 4", "Comment 5", "Comment 6","Comment 7", "Comment 8", "Comment 9", /* ... more comments ... */ };
		GenerateCommentObjects();
	}
	
	public void GenerateCommentObjects()
	{
		foreach (GameObject gameObject in commentObjects)
		{
			Destroy(gameObject);
		}
		commentObjects.Clear();
		
		foreach (string comment in commentsList)
		{
			GameObject newComment = Instantiate(commentPrefab, transform);
			newComment.GetComponent<Text>().text = comment;
			commentObjects.Add(newComment);
		}

		// Assuming all comments are of the same height
		if (commentObjects.Count > 0)
		{
			RectTransform rectTransform = commentObjects[0].GetComponent<RectTransform>();
			commentHeight = rectTransform.rect.height + GetComponent<VerticalLayoutGroup>().preferredHeight; //FIXME:位置不对
			Debug.Log(GetComponent<VerticalLayoutGroup>().preferredHeight);
		}
	}

	void Update()
	{
		float resetPositionY = -commentHeight * (commentObjects.Count - 1); 
		foreach (GameObject comment in commentObjects)
		{
			// Move comment upwards
			comment.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

			// Check if the comment is out of view, then reset its position
			if (comment.transform.localPosition.y > 5)
			{
				Vector3 newPos = comment.transform.localPosition;
				newPos.y = resetPositionY;
				comment.transform.localPosition = newPos;

				// Move the comment to the end of the list
				commentObjects.Remove(comment);
				commentObjects.Add(comment); 
				break; // Break the loop to avoid modifying the collection during iteration
			}
		}
	}

}
