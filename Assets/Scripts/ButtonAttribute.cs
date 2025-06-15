using UnityEngine;
using System;

/// <summary>
/// Attribute that makes a method appear as a button in the inspector.
/// Usage: [Button] void MyMethod() { ... }
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ButtonAttribute : PropertyAttribute
{
    public string Name { get; private set; }
    public string Tooltip { get; private set; }

    /// <summary>
    /// Makes a method appear as a button in the inspector with custom name and tooltip.
    /// </summary>
    /// <param name="name">Custom name for the button. If empty, the method name will be used.</param>
    /// <param name="tooltip">Tooltip text that appears when hovering over the button.</param>
    public ButtonAttribute(string name = "", string tooltip = "")
    {
        Name = name;
        Tooltip = tooltip;
    }
} 