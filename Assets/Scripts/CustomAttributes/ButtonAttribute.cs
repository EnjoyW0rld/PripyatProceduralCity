using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ButtonAttribute : PropertyAttribute
{
    public string MethodName;
    public ButtonAttribute(string methodName)
    {
        MethodName = methodName;
    }
}
