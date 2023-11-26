using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCommentsList : MonoBehaviour
{
	private ExcelCommentReader excelCommentReader;
	public AutoScrollComments autoScrollComments;
	public TrendManager trendManager;
	private Dictionary<string, List<string>> commentDictionary = new Dictionary<string, List<string>>();
	
	// Start is called before the first frame update
	void Awake() 
	{
		excelCommentReader = GetComponent<ExcelCommentReader>();
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
		autoScrollComments.commentsList.Clear();
		foreach(string trend in trendManager.trendListToday)
		{
			if(commentDictionary.ContainsKey(trend))
				for(int i = 1; i < commentDictionary[trend].Count; i++)
				{
					autoScrollComments.commentsList.Add(commentDictionary[trend][i]);
				}
		}
		autoScrollComments.GenerateCommentObjects();
	}
}
