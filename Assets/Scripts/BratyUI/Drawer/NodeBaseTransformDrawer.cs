using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BratyUI.Drawer
{
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    public class NodeBaseTransformDrawer : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            if (((Transform)serializedObject.targetObject).gameObject.TryGetComponent(out NodeBase node))
            {
                // Add your custom UI elements here
                // ...

                return root;
            }

            return base.CreateInspectorGUI();
            // If the condition is not met, return the default inspector
        }
    }
}