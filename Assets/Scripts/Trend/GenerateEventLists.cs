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
	public static Dictionary<string, List<string>> eventDictionary;
	public static Dictionary<string, string> tempEventDictionary;
	private static List<string> keyListAll = new List<string>();
	
	private void Awake() 
	{
		eventDictionary = new Dictionary<string, List<string>>();
		tempEventDictionary = new Dictionary<string, string>();
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
		List<string> currentList = new List<string>();
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
		ShuffleList(eventDictionary["R"]);
	}
	
	private void GenerateEvents()
	{
		ResetKeyListAll();
		for(int i = 0; i < eventNumber; i++)
		{
			GameObject eventItem = Instantiate(eventPrefab);
			eventItem.transform.SetParent(eventParent);
			eventItem.GetComponent<DraggableUI>().SetCanvas(eventParent.GetComponentInParent<Canvas>());
			RectTransform rectTransform = eventItem.GetComponent<RectTransform>();
			int randomXValue = i > 1 ? 250 : 5;
			rectTransform.anchoredPosition3D = eventPosition.anchoredPosition3D 
											   - new Vector3(Random.Range(-25, randomXValue), i*110 + Random.Range(-10, 30), 0);
											   
			rectTransform.localScale = new Vector3(1,1,1);
			if(i < keyListAll.Count)
			{	//TODO:新闻出现的逻辑；更随机的位置；优化：字典value改成队列或者是链表(性能) Random position; queue/linkedList
				if(eventDictionary[keyListAll[i]].Count > 0)
				{
					eventItem.GetComponentInChildren<Text>().text = eventDictionary[keyListAll[i]][0];
					tempEventDictionary.Add(eventDictionary[keyListAll[i]][0], keyListAll[i]);
					eventDictionary[keyListAll[i]].RemoveAt(0);
				}
				else
				{
					Debug.LogWarning(keyListAll[i] + ": No event left");
					if(eventDictionary["R"].Count > 0)
					{
						eventItem.GetComponentInChildren<Text>().text = eventDictionary["R"][0];
						tempEventDictionary.Add(eventDictionary["R"][0], "R");
						eventDictionary["R"].RemoveAt(0);
					}
					else
					{
						Debug.LogWarning("Events run out!!!");
					}
				}
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
		GetComponent<DictInspector>().UpdateTodayEvents();
	}
	
	public void ClearEvents()
	{
		for(int i = 0; i < eventParent.childCount; i++)
		{
			Destroy(eventParent.GetChild(i).gameObject);
		}
		Invoke("GenerateEvents", 0.8f);
	}
	
	private void ResetKeyListAll()
	{
		keyListAll.Clear(); // Clear the list before adding keys

		foreach(string key in eventDictionary.Keys)
		{
			keyListAll.Add(key);
		}

		// Now shuffle the keyList
		ShuffleList(keyListAll);
		ShuffleKeyList(keyListAll);
	}
	
	private void ShuffleList(List<string> list)
	{
		System.Random rng = new System.Random();
		int n = list.Count;

		while (n > 1) 
		{
			n--;
			int k = rng.Next(n + 1); //  returns a non-negative random integer that is less than the specified value
			string value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	private void ShuffleKeyList(List<string> list)
	{
		List<TrendLV> trendListAll = GetComponent<TrendManager>().trendListAll;
		int minus = 0;
		for(int i = 0; i  < trendListAll.Count; i++)
		{
			if (!eventDictionary.ContainsKey(trendListAll[i].eventGroup)) { minus++; continue; }
			string value = list[i - minus];
			int indexOfBig = list.IndexOf(trendListAll[i].eventGroup);
			list[i - minus] = list[indexOfBig];
			list[indexOfBig] = value;
		}

		string subValue = list[eventNumber - 1];
		int RIndex = list.IndexOf("R");
		list[eventNumber - 1] = list[RIndex];
		list[RIndex] = subValue;
		Debug.Log((eventNumber - 1) + " : " + list[5]);
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
