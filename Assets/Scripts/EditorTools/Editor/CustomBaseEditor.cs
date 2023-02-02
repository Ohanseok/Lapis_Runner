using UnityEditor;
using UnityEngine;

public class CustomBaseEditor : Editor
{
    // �⺻ �ν�����ó�� ���̴� Ŀ���� �ν�����
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
