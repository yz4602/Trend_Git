using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotSearchManager : MonoBehaviour
{
    // ��������һ��UIԪ����չʾ���ѱ���
    public GameObject hotSearchItemPrefab;
    public Transform hotSearchListParent;

    // �洢���п��ܵ����ѱ���
    private List<string> potentialHotSearches = new List<string>();

    // �洢��ǰѡ�е�ǰ5���ѱ���
    private List<string> currentTopFiveHotSearches = new List<string>();
    
    void Start()
    {
        // ��ʼ��һЩ���ѱ���
        potentialHotSearches.Add("��ޱ�¸ҽ�¶��ҵ��Ļ");
        potentialHotSearches.Add("��ҵ��ͷ����");
        potentialHotSearches.Add("��ҵ��ͷ��zhe����");
        potentialHotSearches.Add("��ҵ��ͷ���������");
        // ...��Ӹ������
        // ע�⣺ʵ����Ϸ����Щ���ݿ������Է������򱾵ػ��ļ�

        // ������ʼ�������б�
        UpdateHotSearchListUI();
    }

    // ����������ڸ���UI�б�
    private void UpdateHotSearchListUI()
    {
        // ����ɵ�������Ŀ
        foreach (Transform child in hotSearchListParent)
        {
            Destroy(child.gameObject);
        }

        // Ϊÿ�����ܵ����ѱ��ⴴ��һ��UIԪ��
        foreach (var hotSearch in potentialHotSearches)
        {
            var hotSearchItem = Instantiate(hotSearchItemPrefab, hotSearchListParent);
            hotSearchItem.GetComponent<Text>().text = hotSearch;
            // ��ӵ���¼�����ק�¼��ļ����������԰�ť���Ϊ��
            //hotSearchItem.GetComponentInChildren<Button>().onClick.AddListener(() => SelectHotSearch(hotSearch));
        }
    }

    // ���ѡ��һ������ʱ���õķ���
    private void SelectHotSearch(string hotSearch)
    {
        if (!currentTopFiveHotSearches.Contains(hotSearch))
        {
            if (currentTopFiveHotSearches.Count < 5)
            {
                // ��ӵ���ǰ���Ѳ�����UI
                currentTopFiveHotSearches.Add(hotSearch);
                UpdateCurrentTopFiveUI();
            }
            else
            {
                Debug.Log("�����б�������");
                // ������ʾ�����Ҫ�Ƴ�һ����ǰ������
            }
        }
    }

    // ���µ�ǰTop 5���ѵ�UI
    private void UpdateCurrentTopFiveUI()
    {
        // ����Ӧ���Ǹ��������ѡ���ǰ5���ѵ��߼�
        // ����Ҫһ�������UIԪ����չʾ��ǰ��Top 5
        // ...
    }

    // ����������������Ѿ�ѡ�������
    public void SortHotSearch(string hotSearch, int newPosition)
    {
        if (currentTopFiveHotSearches.Contains(hotSearch))
        {
            // �Ƴ���λ���ϵ�����
            currentTopFiveHotSearches.Remove(hotSearch);
            // ������λ��
            currentTopFiveHotSearches.Insert(newPosition, hotSearch);
            UpdateCurrentTopFiveUI();
        }
    }
}
