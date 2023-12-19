using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GenerateCommentsList : MonoBehaviour
{
	private ExcelCommentReader excelCommentReader;
	public AutoScrollComments autoScrollComments;
	public TrendManager trendManager;
	public ChangeBarsValue changeBarsValue;
	public PlayableDirector playableDirector;
	public GameObject ContentForWebGL;
	private Dictionary<string, List<string>> commentDictionary = new Dictionary<string, List<string>>();
	
	// Start is called before the first frame update
	void Awake() 
	{
		#if UNITY_WEBGL && !UNITY_EDITOR
			excelCommentReader = ContentForWebGL.GetComponent<ExcelCommentReader>();
		#else
			excelCommentReader = GetComponent<ExcelCommentReader>();
		#endif
	}
	
	void Start()
	{
		OrganizeCommentDict();
	}

	private void OrganizeCommentDict()
	{
		List<string> currentList = null;
		foreach(string str in excelCommentReader.excelCommentList)
		{
			if(str.Contains("#"))
			{
				string key = str.Replace("#", ""); // Adjust this line

				// Check if the key already exists to avoid duplicates
				if (!commentDictionary.ContainsKey(key))
				{
					commentDictionary.Add(key, new List<string>()); Debug.Log("Add key: " + key);
					currentList = commentDictionary[key];
				}
				continue;
			}
			currentList.Add(str);		
		}	
	}
	
	public void UpdateCommentList()
	{
		Invoke("_UpdateCommentList", 0.7f);
	}
	
	private void _UpdateCommentList()
	{
		autoScrollComments.commentsList.Clear();
		changeBarsValue.tempSociety = 0;
		changeBarsValue.tempReputation = 0;
		changeBarsValue.tempWealth = 0;
		float multiplier = 1f;
		
		foreach(string trend in trendManager.trendListToday)
		{
			if(commentDictionary.ContainsKey(trend))
			{
				string[] valuesStr = commentDictionary[trend][0].Split(",");
				changeBarsValue.tempSociety += int.Parse(valuesStr[0]) * multiplier; //Debug.Log("String: " + valuesStr[0] + "  Value: " + int.Parse(valuesStr[0]));
				changeBarsValue.tempReputation += int.Parse(valuesStr[1]) * multiplier; 
				changeBarsValue.tempWealth += int.Parse(valuesStr[2]) * multiplier; 
				multiplier -= 0.05f;
				
				for(int i = 1; i < commentDictionary[trend].Count; i++)
				{
					autoScrollComments.commentsList.Add(commentDictionary[trend][i]);
				}
			}				
		}
		//changeBarsValue.ChangeBarValue();
		autoScrollComments.GenerateCommentObjects();
	}
}
