using System.Collections;
using System.Collections.Generic;

#if QUNITY_EDITOR

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(Door))]
public class DoorInspector : Editor
{
    public VisualTreeAsset UXML_Asset;

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        // Load and clone a visual tree from UXML
        VisualTreeAsset visualTree = UXML_Asset;
        visualTree.CloneTree(myInspector);

        // Return the finished inspector UI
        return myInspector;
    }
}

#endif