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

	void Start()
	{
		Dictionary<string, List<string>> eventDictionary = generateEventLists.GetEventDict();
		Debug.Log(eventDictionary.Count + "  " + generateEventLists.GetEventDict().Count);
		foreach (var pair in eventDictionary)
		{
			Debug.Log("Add");
			eventInspectList.Add(new StringListPair(pair.Key, pair.Value));
		}
	}
}






