using System;
using UnityEngine;

public class InterfaceAttribute : PropertyAttribute
{
    public Type RequiredType { get; private set; }

    public InterfaceAttribute(Type type)
    {
        RequiredType = type;
    }
}