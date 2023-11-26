using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateSlots : MonoBehaviour
{
	public int rawNum;
	public GameObject slotPrefab;
	public RectTransform slotBackgroundTrans;

	// Start is called before the first frame update
	void Start()
	{
		GenerateSlot();
	}
	
	public void GenerateSlot()
	{
		if (slotBackgroundTrans) slotBackgroundTrans.anchoredPosition = 
		new Vector2(slotBackgroundTrans.anchoredPosition.x, rawNum % 2 == 1 ? 0 : -50);

		for(int i = 0; i < rawNum; i++)
		{
			GameObject go = Instantiate(slotPrefab);
			go.transform.SetParent(transform,false);
			go.transform.localPosition = new Vector2(0, (i - rawNum / 2) * 100f);
			go.transform.localScale = slotPrefab.transform.localScale;
			
			if(slotBackgroundTrans)
			{
				slotBackgroundTrans.sizeDelta = new Vector2(slotBackgroundTrans.sizeDelta.x, 
															slotBackgroundTrans.sizeDelta.y + 100f);
			}
			DraggableUI.snapPositions.Add(go.GetComponent<RectTransform>().anchoredPosition + 
										  this.GetComponent<RectTransform>().anchoredPosition);
		}

		DraggableUI.snapArea.width = slotBackgroundTrans.sizeDelta.x;
		DraggableUI.snapArea.height = slotBackgroundTrans.sizeDelta.y;
		DraggableUI.snapArea.x = GetComponent<RectTransform>().anchoredPosition.x - DraggableUI.snapArea.width/2;
		DraggableUI.snapArea.y = GetComponent<RectTransform>().anchoredPosition.y - DraggableUI.snapArea.height/2;
	}
}
