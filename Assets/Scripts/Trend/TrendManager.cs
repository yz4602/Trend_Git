using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrendManager : MonoBehaviour
{
	public List<String> trendListToday;
	public List<TrendLV> trendListAll = new List<TrendLV>(); //static

	public void UpdateTrendList()
	{
		DeleteSpareV2(DraggableUI.itemSlotPositions);
		SortV2Dictionary(DraggableUI.itemSlotPositions);
		RemoveTheKeyNotSelected();
		DraggableUI.numOfTrendInArea = 0;
		GetComponent<DictInspector>().UpdateInspector();
	}

	//Remove the original position left in itemSlotPositions which will cause error
	private void DeleteSpareV2(Dictionary<Vector2, GameObject> v2Dict)
	{
		List<Vector2> keysToRemove = new List<Vector2>();
		foreach (Vector2 v2 in v2Dict.Keys)
		{
			if (!DraggableUI.snapPositions.Contains(v2))
			{
				keysToRemove.Add(v2);
			}
		}
		
		foreach (Vector2 key in keysToRemove)
		{
			v2Dict.Remove(key);
		}
	}

	private void SortV2Dictionary(Dictionary<Vector2, GameObject> v2Dict)
	{
		trendListToday.Clear();
		Vector2 headVector2;
		Dictionary<Vector2, GameObject> newV2Dict = new Dictionary<Vector2, GameObject>();
		int listCount = v2Dict.Count;
		for(int i = 0; i < listCount; i++)
		{
			headVector2 = new Vector2(0, float.MinValue);
			foreach (Vector2 v2 in v2Dict.Keys)
			{
				if (v2.y > headVector2.y)
				{
					headVector2 = v2;
				}
			}
			newV2Dict.Add(headVector2, v2Dict[headVector2]);
			//Debug.Log("listCount " + listCount + "  " + (DraggableUI.itemSlotPositions[headVector2] == null).ToString());
			string eventContent = DraggableUI.itemSlotPositions[headVector2].transform.GetComponentInChildren<Text>().text;
			trendListToday.Add(eventContent);
			
			v2Dict.Remove(headVector2);
			
			//FIXME:可能错误
			// string eventGroupName = null;
			// if(GenerateEventLists.tempEventDictionary.ContainsKey(eventContent)) 
			// 	eventGroupName = GenerateEventLists.tempEventDictionary[eventContent];
			string eventGroupName = GenerateEventLists.tempEventDictionary[eventContent];
			GenerateEventLists.tempEventDictionary.Remove(eventContent);
			// if(eventGroupName != null) GenerateEventLists.eventDictionary[eventGroupName].RemoveAt(0);//不是error 游戏逻辑里不会重复出现同样的event
			
			if(trendListAll.Count == 0)
			{
				trendListAll.Add(new TrendLV(eventGroupName, listCount - i));
			}
			else
			{
				TrendLV targetTrend = new TrendLV("null",-1);
				foreach(TrendLV trendLV in trendListAll)
				{
					if(trendLV.eventGroup == eventGroupName)
					{
						targetTrend = trendLV;
					}
				}
				if(targetTrend.eventGroup != "null")
				{
					targetTrend.trendValue += listCount - i;
				}
				else
				{
					trendListAll.Add(new TrendLV(eventGroupName,listCount - i));
				}				
			}	
		}
		
		foreach(TrendLV trendLV1 in trendListAll)
		{
			if(trendLV1.eventGroup == "R")
			{
				trendListAll.Remove(trendLV1);	
				break;
			}
		}
		
		DraggableUI.itemSlotPositions = newV2Dict;
	}
	
	private void RemoveTheKeyNotSelected()
	{
		foreach(string groupName in GenerateEventLists.tempEventDictionary.Values)
		{
			Debug.Log("Delete Group: " + groupName);
			if(groupName != "R")
			{
				GenerateEventLists.eventDictionary.Remove(groupName);
			}
			//TODO:增加未完成导致的event到R组
		}
	}
}

[Serializable]
public class TrendLV : IComparable<TrendLV>
{
	public string eventGroup;
	public int trendValue;
	
	public TrendLV(string eventGroup, int trendValue)
	{
		this.eventGroup = eventGroup;
		this.trendValue = trendValue;
	}

	public int CompareTo(TrendLV other)
	{
		if(trendValue > other.trendValue)
		{
			return 1;
		}
		else
		{
			return -1;
		}
	}
}
