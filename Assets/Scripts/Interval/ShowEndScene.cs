using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEndScene : MonoBehaviour
{
	public ChangeBarsValue changeBarsValue;
	public GameObject EndPanel;
	private Image[] images = new Image[3];
	private bool[] imagesShow;
	
	private void OnEnable() 
	{
		images[0] = transform.GetChild(1).GetComponent<Image>();
		images[1] = transform.GetChild(2).GetComponent<Image>();
		images[2] = transform.GetChild(3).GetComponent<Image>();
		imagesShow = new bool[3]{false, false, false};	
		
		float societyValue = changeBarsValue.SocietyBar.size * 100f;
		float ReputationValue = changeBarsValue.ReputationBar.size * 100f;
		float WealthValue = changeBarsValue.WealthBar.size * 100f;
		
		if(societyValue > 66)
		{
			images[0].sprite = Resources.Load<Sprite>("EndImages/SH");
			images[0].gameObject.GetComponentInChildren<Text>().text = "Happy Society";
			imagesShow[0] = true;
		}
		else if(societyValue < 33)
		{
			images[0].sprite = Resources.Load<Sprite>("EndImages/SL");
			images[0].gameObject.GetComponentInChildren<Text>().text = "Blame Culture Society";
			imagesShow[0] = true;
		}
		
		if(ReputationValue > 66)
		{
			images[1].sprite = Resources.Load<Sprite>("EndImages/TH");
			images[1].gameObject.GetComponentInChildren<Text>().text = "Center of Attention";
			imagesShow[1] = true;
		}
		else if(ReputationValue < 33)
		{
			images[1].sprite = Resources.Load<Sprite>("EndImages/TL");
			images[1].gameObject.GetComponentInChildren<Text>().text = "No Attention";
			imagesShow[1] = true;
		}
		
		if(WealthValue > 66)
		{
			images[2].sprite = Resources.Load<Sprite>("EndImages/WH");
			images[2].gameObject.GetComponentInChildren<Text>().text = "Trend & Trade";
			imagesShow[2] = true;
		}
		
		if(CanShowOrdinary())
		{
			Invoke("ShowOrdinaryScene", 3f);
			Invoke("ShowEndPanel", 7f);
		}	
		else
			StartCoroutine(ShowEnd());
		
	}
	
	IEnumerator ShowEnd()
	{
		yield return new WaitForSecondsRealtime(3);
		for(int i = 0; i < imagesShow.Length; i++)
		{
			if(imagesShow[i])
			{
				images[i].gameObject.SetActive(true);
				yield return new WaitForSecondsRealtime(3);
			}
		}
		Invoke("ShowEndPanel", 2f);		
		yield return null;
	}
	
	bool CanShowOrdinary()
	{
		foreach(bool i in imagesShow)
		{
			if(i == true) return false;
		}
		return true;
	}
	
	void ShowOrdinaryScene()
	{
		images[0].gameObject.SetActive(true);
	}
	
	void ShowEndPanel()
	{
		EndPanel.SetActive(true);
	}
}
