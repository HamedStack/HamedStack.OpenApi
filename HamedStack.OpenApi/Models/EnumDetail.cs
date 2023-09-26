namespace HamedStack.OpenApi.Models;

internal class EnumDetail<T> where T : Enum
{
    public string? Description { get; set; }
    public T? Item { get; set; }
    public string? Name { get; set; }
    public int Value { get; set; }
}