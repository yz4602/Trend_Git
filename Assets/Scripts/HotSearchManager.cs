using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotSearchManager : MonoBehaviour
{
	// 假设你有一个UI元素来展示热搜标题
	public GameObject hotSearchItemPrefab;
	public Transform hotSearchListParent;

	// 存储所有可能的热搜标题
	private List<string> potentialHotSearches = new List<string>();

	// 存储当前选中的前5热搜标题
	private List<string> currentTopFiveHotSearches = new List<string>();
	
	void Start()
	{
		// 初始化一些热搜标题
		potentialHotSearches.Add("张薇勇敢揭露行业黑幕");
		potentialHotSearches.Add("行业巨头反击");
		potentialHotSearches.Add("行业巨头王喆反击");
		potentialHotSearches.Add("行业巨头培根王反击");
		potentialHotSearches.Add("张薇勇敢行业黑幕");
		potentialHotSearches.Add("张薇勇敢行业黑幕123");
		// ...添加更多标题
		// 注意：实际游戏中这些数据可能来自服务器或本地化文件

		// 创建初始的热搜列表
		UpdateHotSearchListUI();
	}

	// 这个方法用于更新UI列表
	private void UpdateHotSearchListUI()
	{
		// 清除旧的热搜条目
		foreach (Transform child in hotSearchListParent)
		{
			Destroy(child.gameObject);
		}

		// 为每个可能的热搜标题创建一个UI元素
		foreach (var hotSearch in potentialHotSearches)
		{
			var hotSearchItem = Instantiate(hotSearchItemPrefab, hotSearchListParent);
			hotSearchItem.GetComponent<Text>().text = hotSearch;
			// 添加点击事件或拖拽事件的监听，这里以按钮点击为例
			//hotSearchItem.GetComponentInChildren<Button>().onClick.AddListener(() => SelectHotSearch(hotSearch));
		}
	}

	// 玩家选择一个标题时调用的方法
	private void SelectHotSearch(string hotSearch)
	{
		if (!currentTopFiveHotSearches.Contains(hotSearch))
		{
			if (currentTopFiveHotSearches.Count < 5)
			{
				// 添加到当前热搜并更新UI
				currentTopFiveHotSearches.Add(hotSearch);
				UpdateCurrentTopFiveUI();
			}
			else
			{
				Debug.Log("热搜列表已满！");
				// 可以提示玩家需要移除一个当前的热搜
			}
		}
	}

	// 更新当前Top 5热搜的UI
	private void UpdateCurrentTopFiveUI()
	{
		// 这里应该是更新玩家已选择的前5热搜的逻辑
		// 你需要一个额外的UI元素来展示当前的Top 5
		// ...
	}

	// 允许玩家重新排序已经选择的热搜
	public void SortHotSearch(string hotSearch, int newPosition)
	{
		if (currentTopFiveHotSearches.Contains(hotSearch))
		{
			// 移除旧位置上的热搜
			currentTopFiveHotSearches.Remove(hotSearch);
			// 插入新位置
			currentTopFiveHotSearches.Insert(newPosition, hotSearch);
			UpdateCurrentTopFiveUI();
		}
	}
}
