using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class GenerateEventLists : MonoBehaviour
{
	public GameObject eventPrefab;
	public Transform eventParent;
	public RectTransform eventPosition;
	public int eventNumber;
	private ExcelReader excelReader;
	public static Dictionary<string, List<string>> eventDictionary = new Dictionary<string, List<string>>();
	public static Dictionary<string, string> tempEventDictionary = new Dictionary<string, string>();
	private List<string> currentList;
	private List<string> keyList = new List<string>();
	
	private void Awake() 
	{
		excelReader = GetComponent<ExcelReader>();
	}
	
	// Start is called before the first frame update
	void Start()
	{
		OrganizeEventDict();
		GenerateEvents();
	}
	
	private void OrganizeEventDict()
	{
		foreach(string str in excelReader.excelContentList)
		{
			if(str.Contains("#"))
			{
				string key = str.Replace("#", ""); // Adjust this line

				// Check if the key already exists to avoid duplicates
				if (!eventDictionary.ContainsKey(key))
				{
					eventDictionary.Add(key, new List<string>());
					currentList = eventDictionary[key];
				}
				continue;
			}
			currentList.Add(str);		
		}
	}
	
	private void GenerateEvents()
	{
		ResetKeyList();
		for(int i = 0; i < eventNumber; i++)
		{
			GameObject eventItem = Instantiate(eventPrefab);
			eventItem.transform.SetParent(eventParent);
			eventItem.GetComponent<DraggableUI>().SetCanvas(eventParent.GetComponentInParent<Canvas>());
			RectTransform rectTransform = eventItem.GetComponent<RectTransform>();
			rectTransform.anchoredPosition3D = eventPosition.anchoredPosition3D 
											   - new Vector3(Random.Range(-350, 200), i*100 + Random.Range(-20, 20), 0);
			rectTransform.localScale = new Vector3(1,1,1);
			if(i < keyList.Count)
			{	//TODO:新闻出现的逻辑；更随机的位置；优化：字典value改成队列或者是链表(性能) Random position; queue/linkedList
				eventItem.GetComponentInChildren<Text>().text = eventDictionary[keyList[i]][0];
				tempEventDictionary.Add(eventDictionary[keyList[i]][0], keyList[i]);
				eventDictionary[keyList[i]].RemoveAt(0);
			}
			else if(eventDictionary["R"].Count >= 1)
			{
				eventItem.GetComponentInChildren<Text>().text = eventDictionary["R"][0];
				tempEventDictionary.Add(eventDictionary["R"][0], "R");
				eventDictionary["R"].RemoveAt(0);
			}
			else
			{
				Debug.LogWarning("Event items not enough!");
			}
		}
	}
	
	private void ResetKeyList()
	{
		keyList.Clear(); // Clear the list before adding keys

		foreach(string key in eventDictionary.Keys)
		{
			keyList.Add(key);
		}

		// Now shuffle the keyList
		System.Random rng = new System.Random();
		int n = keyList.Count;

		while (n > 1) 
		{
			n--;
			int k = rng.Next(n + 1); //  returns a non-negative random integer that is less than the specified value
			string value = keyList[k];
			keyList[k] = keyList[n];
			keyList[n] = value;
		}
	}
	
	public Dictionary<string, List<string>> GetEventDict()
	{
		return eventDictionary;
	}
	
	public static string FindKeyByString(string str)
	{
		foreach(KeyValuePair<string, List<string>> pair in eventDictionary)
		{
			if(pair.Value.Contains(str)) return pair.Key;
		}
		return null;
	}
}
