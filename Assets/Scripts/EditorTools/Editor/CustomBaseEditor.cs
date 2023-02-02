using UnityEditor;
using UnityEngine;

public class CustomBaseEditor : Editor
{
    // 기본 인스펙터처럼 보이는 커스텀 인스펙터
    public void DrawNonEditableScriptReference<T>() where T : Object
    {
        GUI.enabled = false;

        if (typeof(ScriptableObject).IsAssignableFrom(typeof(T)))
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), typeof(T), false);
        else if (typeof(MonoBehaviour).IsAssignableFrom(typeof(T)))
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), typeof(T), false);

        GUI.enabled = true;
    }
}
