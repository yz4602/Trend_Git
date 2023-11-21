using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//[CustomEditor(typeof(DictInspector))]
public class DictInspectorEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     DictInspector dictInspector = (DictInspector)target;

    //     // Create a custom GUI for the dictionary
    //     foreach (var pair in dictInspector.eventInspectList)
    //     {
    //         EditorGUILayout.LabelField(pair.Key);
    //         // Display list or other controls for the value
    //     }

    //     // More custom GUI here if necessary

    //     if (GUI.changed)
    //     {
    //         EditorUtility.SetDirty(dictInspector);
    //     }
    // }
}
