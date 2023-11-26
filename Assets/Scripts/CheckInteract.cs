using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInteract : MonoBehaviour
{
	void Update()
	{
		if(DraggableUI.numOfTrendInArea >= 5)
		{
			GetComponent<Button>().interactable = true;
		}
		else
		{
			GetComponent<Button>().interactable = false;
		}
	}
}
