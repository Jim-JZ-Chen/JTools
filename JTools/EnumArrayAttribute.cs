using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public EnumArrayAttribute(System.Type names_enum_type) { this.names = System.Enum.GetNames(names_enum_type); }
}
