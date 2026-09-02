using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InterfaceAttribute))]
public class InterfacePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.serializedObject.isEditingMultipleObjects) return;
        if (property.propertyType != SerializedPropertyType.ObjectReference) return;
        var interfaceAttribute = this.attribute as InterfaceAttribute;
        var requiredType = interfaceAttribute.RequiredType;
        EditorGUI.BeginProperty(position, label, property);

        var reference = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(UnityEngine.Object), true);
        if (reference is GameObject go)
        {
            reference = go.GetComponent(requiredType);
        }

        property.objectReferenceValue = reference;

        EditorGUI.EndProperty();
    }
}