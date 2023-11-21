using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrendManager : MonoBehaviour
{
	public List<String> trendListToday;
	public static List<TrendLV> trendListAll;

	public void UpdateTrendList()
	{
		SortV2Dictionary(DraggableUI.itemSlotPositions);
	}

	private void SortV2Dictionary(Dictionary<Vector2, GameObject> v2Dict)
	{
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
			trendListToday.Add(DraggableUI.itemSlotPositions[headVector2].transform.GetComponentInChildren<Text>().text);
			//TODO:get event group 加值
			
			v2Dict.Remove(headVector2);
		}
		DraggableUI.itemSlotPositions = newV2Dict;
	}
	
	private void calculateTrendValue(TrendLV trendLV, int addValue)
	{
		trendLV.trendValue += addValue;
	}
	
}

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
