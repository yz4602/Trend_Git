using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBarsValue : MonoBehaviour
{
	public Scrollbar SocietyBar;
	public Scrollbar ReputationBar;
	public Scrollbar WealthBar;
	public Image SocietyImage;
	public Image ReputationImage;
	public Image WealthImage;
	public float tempSociety;
	public float tempReputation;
	public float tempWealth;
	
	// Start is called before the first frame update
	void Start()
	{
		SocietyBar.size = 0.5f;
		ReputationBar.size = 0.5f;
		WealthBar.size = 0;
	}

	// Update is called once per frame
	// void Update()
	// {
		
	// }
	
	public void ChangeBarValue() //TODO:改时间
	{
		StartCoroutine(ChangeValue());
	}
	
	private IEnumerator ChangeValue()
	{
		for(int i = 0; i < 200; i++)
		{
			SocietyBar.size += tempSociety/20000f;
			ReputationBar.size += tempReputation/20000f;
			WealthBar.size += tempWealth/20000f;
			ChangeBarImages();						
			yield return new WaitForSecondsRealtime(0.01f);
		}
		yield return null;
	}

	private void ChangeBarImages()
	{
		if(SocietyBar.size > 0.66f) SocietyImage.sprite = Resources.Load<Sprite>("BarImages/SocietyHigh");
		else if(SocietyBar.size > 0.33f) SocietyImage.sprite = Resources.Load<Sprite>("BarImages/SocietyMid");
		else if(SocietyBar.size > 0) SocietyImage.sprite = Resources.Load<Sprite>("BarImages/SocietyLow");
		
		if(ReputationBar.size > 0.66f) ReputationImage.sprite = Resources.Load<Sprite>("BarImages/ReputationHigh");
		else if(ReputationBar.size > 0.33f) ReputationImage.sprite = Resources.Load<Sprite>("BarImages/ReputationMid");
		else if(ReputationBar.size > 0) ReputationImage.sprite = Resources.Load<Sprite>("BarImages/ReputationLow");
		
		if(WealthBar.size > 0.66f) WealthImage.sprite = Resources.Load<Sprite>("BarImages/WealthHigh");
		else if(WealthBar.size > 0.33f) WealthImage.sprite = Resources.Load<Sprite>("BarImages/WealthMid");
		else if(WealthBar.size > 0) WealthImage.sprite = Resources.Load<Sprite>("BarImages/WealthLow");
	}

	public int GetMaxChangeValue()
	{
		float absSociety = Mathf.Abs(tempSociety);
		float absReputation = Mathf.Abs(tempReputation);
		float absWealth = Mathf.Abs(tempWealth);
		
		if(absSociety >= absReputation && absSociety > absWealth) return 0;
		else if(absReputation >= absSociety && absReputation >= absWealth) return 1;
		else if(absWealth >= absSociety && absWealth >= absReputation) return 2;
		else return -1;
	}
}
