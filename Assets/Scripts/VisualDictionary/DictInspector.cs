using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StringListPair
{
	public string listKey;
	public List<string> Value;
	
	public StringListPair(string key, List<string> value)
	{
		this.listKey = key;
		this.Value = value;
	}
}

public class DictInspector : MonoBehaviour
{
	public GenerateEventLists generateEventLists;
	public List<StringListPair> eventInspectList;
	public List<string> todayEvents;

	void Start()
	{
		UpdateInspector();
	}
	
	public void UpdateInspector()
	{
		eventInspectList.Clear();
		Dictionary<string, List<string>> eventDictionary = generateEventLists.GetEventDict();
		Debug.Log("Inspector:" + eventDictionary.Count);
		foreach (var pair in eventDictionary)
		{
			eventInspectList.Add(new StringListPair(pair.Key, pair.Value));
		}
	}
	
	public void UpdateTodayEvents()
	{
		todayEvents.Clear();
		Dictionary<string, string> eventDictionary = GenerateEventLists.tempEventDictionary;
		foreach (var pair in eventDictionary)
		{
			todayEvents.Add(pair.Key);
		}
	}
}






