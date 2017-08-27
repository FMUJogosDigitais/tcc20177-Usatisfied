using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumParse {

    public static bool TryParseEnum<TEnum>(string aName, out TEnum aValue) where TEnum : struct
    {
        try
        {
            aValue = (TEnum)System.Enum.Parse(typeof(TEnum), aName);
            return true;
        }
        catch
        {
            aValue = default(TEnum);
            return false;
        }
    }
}
