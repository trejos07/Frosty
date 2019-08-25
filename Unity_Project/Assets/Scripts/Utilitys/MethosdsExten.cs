using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MethodExtensions
{
    public static string RemoveQuotes(this string Value)
    {
        return Value.Replace("\"", "");
    }

    public static float TwoDecimals(this float Value)
    {
        return Mathf.Round(Value*1000.0f) / 1000.0f;
    }

    public static Vector3 TwoDecimals(this Vector3 Value)
    {
        Value.x = Mathf.Round(Value.x * 1000.0f) / 1000.0f;
        Value.y = Mathf.Round(Value.y * 1000.0f) / 1000.0f;
        Value.z = Mathf.Round(Value.z * 1000.0f) / 1000.0f;

        return Value;
    }

    public static float ParseFloat(this string f)
    {
        return float.Parse(f.Replace(".", ","));
    }
}
