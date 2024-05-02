using UnityEditor;
using UnityEngine;

namespace BratyUI.Editor
{
    [CustomEditor(typeof(Transform), true)]
    public class NodeBaseTransformEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Check if the GameObject has the TryMe component attached
            //TryMe tryMeComponent = ((Transform)serializedObject.targetObject).gameObject.GetComponent<TryMe>();

            if (((Transform)serializedObject.targetObject).gameObject.TryGetComponent(out NodeBase node))
            {
                return;
            }

            DrawDefaultInspector();

            // If the GameObject does not have the TryMe component, draw all properties as usual and return early
            //if (tryMeComponent == null)
            //{
            //DrawDefaultInspector();
            //return;
            // DrawPropertiesExcluding(serializedObject, "m_Script");
            // serializedObject.ApplyModifiedProperties();
            // return;
            //}

            // SerializedProperty iterator = serializedObject.GetIterator();
            // bool enterChildren = true;
            // while (iterator.NextVisible(enterChildren))
            // {
            //     Debug.Log(iterator.name);
            //     enterChildren = false;
            //
            //     // If the GameObject has the TryMe component, skip drawing Transform component properties
            //     if (iterator.name == "m_LocalPosition" || iterator.name == "m_LocalRotation" || iterator.name == "m_LocalScale")
            //         continue;
            //
            //     EditorGUILayout.PropertyField(iterator, true);
            // }
            //
            // serializedObject.ApplyModifiedProperties();
        }
    }
}