using UnityEditor;
using UnityEngine;

public class QuickCreate
{
    [MenuItem("Custom/Create Empty Child GameObject %#e")] // Cmd+Shift+E
    private static void CreateEmptyGOWithParents()
    {
        GameObject selected = Selection.activeGameObject;

        GameObject go;

        // Проверяем: если у родителя есть RectTransform, создаём UI-объект
        var isUI = selected != null && selected.GetComponent<RectTransform>() != null;
    
        if (isUI)
        {
            // Создаём объект с RectTransform
            go = new GameObject("New UI Child", typeof(RectTransform));
        
            // Устанавливаем слой "UI", если он существует
            int uiLayer = LayerMask.NameToLayer("UI");
            if (uiLayer != -1)
                go.layer = uiLayer;
            else
                Debug.LogWarning("Слой 'UI' не найден.");
        
            Undo.RegisterCreatedObjectUndo(go, "Create UI Child GameObject");
        }
        else
        {
            // Обычный объект
            go = new GameObject("New ChildGameObject");
            Undo.RegisterCreatedObjectUndo(go, "Create Child GameObject");
        }
    
        // Если есть выделенный объект — делаем его родителем
        if (selected != null)
        {
            Undo.SetTransformParent(go.transform, selected.transform, "Set Parent");
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
        }

        Undo.RegisterCreatedObjectUndo(go, "Create New GameObject");
        Selection.activeGameObject = go;
    }

    [MenuItem("Custom/Create Empty GameObject %#w")] // Cmd+Shift+W
    private static void CreateEmptyGO()
    {
        GameObject go = new GameObject("New GameObject");
        Undo.RegisterCreatedObjectUndo(go, "Create New GameObject");
        Selection.activeGameObject = go;
    }
}
