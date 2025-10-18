using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public abstract class CeloxisModel
{
    [JsonExtensionData]
    public Dictionary<string, object> OtherFields { get; set; } = new();
    
    public T? GetCustomField<T>(string formulaKey)
    {
        var key = $"custom_{formulaKey}";
        return OtherFields.TryGetValue(key, out var value) && value is T typedValue 
            ? typedValue 
            : default;
    }
    
    public void SetCustomField<T>(string formulaKey, T value)
    {
        var key = $"custom_{formulaKey}";
        OtherFields[key] = value!;
    }
    
    public string[] GetMultiSelectCustomField(string formulaKey)
    {
        var value = GetCustomField<string>(formulaKey);
        return string.IsNullOrEmpty(value) 
            ? Array.Empty<string>() 
            : value.Split('|');
    }
    
    public void SetMultiSelectCustomField(string formulaKey, params string[] values)
    {
        SetCustomField(formulaKey, string.Join("|", values));
    }
}
