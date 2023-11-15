using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrendManager : MonoBehaviour
{
    public List<String> trendList;

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
            trendList.Add(DraggableUI.itemSlotPositions[headVector2].transform.GetComponentInChildren<Text>().text);
            v2Dict.Remove(headVector2);
        }
        DraggableUI.itemSlotPositions = newV2Dict;
        Debug.Log(DraggableUI.itemSlotPositions.Count);
    }
}
