using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ResourcesIdAttribute))]
public class ResourcesIdAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var defs = DefsFacade.I.ResourcesRepository.ItemsForEditor;
        var ids = new List<string>();
        
        foreach (var itemDef in defs)
        {
            ids.Add(itemDef.Id);
        }

        var index = Mathf.Max(ids.IndexOf(property.stringValue), 0);
        
        index = EditorGUI.Popup(position, property.displayName, index, ids.ToArray());
        property.stringValue = ids[index];
    }
}