using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IntervalBossDialogue : MonoBehaviour
{
	public Text bossText;
	public Text weekText;
	public ExcelOtherReader excelOtherReader;
	public ChangeBarsValue changeBarsValue;
	public Dictionary<string, string> otherDict = new Dictionary<string, string>();
	
	private int day = 1;
	
	void Start()
	{
		OrganizeCommentDict();
	}

	private void OrganizeCommentDict()
	{
		string currentKey = null;
		foreach(string str in excelOtherReader.excelOtherText)
		{
			if(str.Contains("#"))
			{
				string key = str.Replace("#", ""); // Adjust this line

				// Check if the key already exists to avoid duplicates
				if (!otherDict.ContainsKey(key))
				{
					otherDict.Add(key,"");
					currentKey = key;
				}
				continue;
			}
			otherDict[currentKey] = str;	
		}	
	}
	
	public void UpdateBossText()
	{
		Debug.Log("Society temp: " + changeBarsValue.tempSociety);
		Debug.Log("Traffic temp: " + changeBarsValue.tempReputation);
		Debug.Log("WealthBar temp: " + changeBarsValue.tempWealth);
		
		
		bossText.text = "";
		
		day++;
		switch(day)
		{
			case 2:
			{
				bossText.text += otherDict["Tuesday"];
				weekText.text = "Tuesday";
				break;
			}
			case 3:
			{
				bossText.text += otherDict["Wednesday"];
				weekText.text = "Wednesday";
				break;
			}
			case 4:
			{
				bossText.text += otherDict["Thursday"];
				weekText.text = "Thursday";
				break;
			}
			case 5:
			{
				bossText.text += otherDict["Friday"];
				weekText.text = "Friday";
				break;
			}
			default:
			{
				Debug.Log("Out of day!");
				break;
			}	
		}
		
		switch(changeBarsValue.GetMaxChangeValue())
		{
			case 0:
			{
				if(changeBarsValue.tempSociety >= 0) bossText.text += otherDict["SocietyH"];
				else bossText.text += otherDict["SocietyL"];
				break;
			}
			case 1:
			{
				if(changeBarsValue.tempReputation >= 0) bossText.text += otherDict["ReputationH"];
				else bossText.text += otherDict["ReputationL"];
				break;
			}
			case 2:
			{
				Debug.Log("changeBarsValue.tempWealth: " + changeBarsValue.tempWealth);
				if(changeBarsValue.tempWealth > 0) bossText.text += otherDict["WealthH"];
				else bossText.text += otherDict["WealthL"];
				break;
			}
			default:
			{
				Debug.Log("Wrong value!");
				break;
			}
		}
		
		//Other text: should be adjusted or removed as the text file is changed. 
		if(day > 1)
		{
			if(GenerateEventLists.tempEventDictionary.ContainsKey("The number of death caused by COVID-2099 reached 420,200") &&
			   otherDict.ContainsKey("Covid"))
				{
					bossText.text += otherDict["Covid"];
					otherDict.Remove("Covid");
				}
			else if(GenerateEventLists.tempEventDictionary.ContainsKey("The domestic economy in the fourth quarter shows stability and improvement") && 
			   otherDict.ContainsKey("Economy"))
			   {
			   	bossText.text += otherDict["Economy"];
				otherDict.Remove("Economy");
			   }
			else if(GenerateEventLists.tempEventDictionary.ContainsKey("Singer Mr. 51 was exposed for seducing a minor") && 
			   otherDict.ContainsKey("SingerRape"))
			   {
			   	bossText.text += otherDict["SingerRape"];
				otherDict.Remove("SingerRape");
			   }
			else if(GenerateEventLists.tempEventDictionary.ContainsKey("It was reported the city mayor had cleaned up the trash, tramps, and homelesses to welcome the guest before arrival") && 
			   otherDict.ContainsKey("Mayor"))
			   {
			   	bossText.text += otherDict["Mayor"];
				otherDict.Remove("Mayor");
			   }			
		}
	}
	
	public int GetDay()
	{
		return day;
	}
}
