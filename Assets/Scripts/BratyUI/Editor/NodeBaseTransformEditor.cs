using UnityEditor;
using UnityEngine;

namespace BratyUI.Editor
{
    [CustomEditor(typeof(Transform), true)]
    public class NodeBaseTransformEditor : UnityEditor.Editor
    {
        // public override void OnInspectorGUI()
        // {
        //     serializedObject.Update();
        //
        //     if (((Transform)serializedObject.targetObject).gameObject.TryGetComponent(out NodeBase node))
        //     {
        //         return;
        //     }
        //
        //     DrawDefaultInspector();
        // }
    }
}