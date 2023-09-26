using HamedStack.OpenApi.Enums;

namespace HamedStack.OpenApi.Settings;

public class TypeScriptGeneratorSettings
{
    public Func<string, string, string>? CustomTypeReplacer { get; set; }
    public string InterfaceOrTypeName { get; set; } = "Root";
    public TypeScriptTypeStyle TypeStyle { get; set; } = TypeScriptTypeStyle.Interface;
    public bool UseAny { get; set; } = false;
    public bool UseDate { get; set; } = true;
}