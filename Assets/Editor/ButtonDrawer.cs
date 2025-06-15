using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class ButtonDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        
        // Get the target object
        Object targetObj = target;
        
        // Get all methods in the target script
        MethodInfo[] methods = targetObj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | 
                                                             BindingFlags.Public | BindingFlags.NonPublic);
        
        // Check each method for our custom attribute
        foreach (MethodInfo method in methods)
        {
            ButtonAttribute buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            
            // Skip if method doesn't have our attribute
            if (buttonAttribute == null) continue;
            
            // Get button name - use method name if custom name is not provided
            string buttonName = string.IsNullOrEmpty(buttonAttribute.Name) 
                ? ObjectNames.NicifyVariableName(method.Name) 
                : buttonAttribute.Name;
            
            // Create GUI content with tooltip
            GUIContent content = new GUIContent(buttonName, buttonAttribute.Tooltip);
            
            // Draw the button
            if (GUILayout.Button(content))
            {
                // Invoke the method when button is pressed
                method.Invoke(targetObj, null);
            }
            
            // Add some space after the button
            EditorGUILayout.Space();
        }
    }
}


[CustomEditor(typeof(ScriptableObject), true)]
[CanEditMultipleObjects]
public class ScriptableObjectButtonDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        
        // Get the target object
        Object targetObj = target;
        
        // Get all methods in the target script
        MethodInfo[] methods = targetObj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | 
                                                             BindingFlags.Public | BindingFlags.NonPublic);
        
        // Check each method for our custom attribute
        foreach (MethodInfo method in methods)
        {
            ButtonAttribute buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            
            // Skip if method doesn't have our attribute
            if (buttonAttribute == null) continue;
            
            // Get button name - use method name if custom name is not provided
            string buttonName = string.IsNullOrEmpty(buttonAttribute.Name) 
                ? ObjectNames.NicifyVariableName(method.Name) 
                : buttonAttribute.Name;
            
            // Create GUI content with tooltip
            GUIContent content = new GUIContent(buttonName, buttonAttribute.Tooltip);
            
            // Draw the button
            if (GUILayout.Button(content))
            {
                // Invoke the method when button is pressed
                method.Invoke(targetObj, null);
            }
            
            // Add some space after the button
            EditorGUILayout.Space();
        }
    }
} 