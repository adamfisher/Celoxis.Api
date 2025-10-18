namespace Celoxis.Api.Models;

public class Association<T>
{
    public string? Url { get; set; }
    public T? Data { get; set; }
    
    public bool IsExpanded => Data != null;
}