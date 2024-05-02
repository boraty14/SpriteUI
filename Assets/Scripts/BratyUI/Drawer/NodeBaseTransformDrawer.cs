using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BratyUI.Drawer
{
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    public class NodeBaseTransformDrawer : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            if (((Transform)serializedObject.targetObject).gameObject.TryGetComponent(out NodeBase node))
            {
                return root;
            }

            return base.CreateInspectorGUI();
        }
    }
}