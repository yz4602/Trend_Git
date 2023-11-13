using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpanPositions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            DraggableUI.itemSlotPositions.Add(child.position, child.gameObject);
        }
    }
}
