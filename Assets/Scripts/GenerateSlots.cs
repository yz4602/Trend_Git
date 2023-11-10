using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSlots : MonoBehaviour
{
    public int rawNum;
    public GameObject slotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < rawNum; i++)
        {
            GameObject go = GameObject.Instantiate(slotPrefab);
            go.transform.SetParent(transform,false);
            go.transform.localPosition = new Vector2(0, (i - rawNum / 2) * 100f);
            go.transform.localScale = slotPrefab.transform.localScale;
            DraggableUI.snapPositions.Add(go.transform.localPosition);
        }
    }
}
