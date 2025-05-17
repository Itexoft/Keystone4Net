using Keystone4Net.Enums;

namespace Keystone4Net.CodeGeneration;

public sealed class ListViewOptions
{
    public string[] InitialColumns { get; init; } = [];
}

public sealed record JsFunction(string Body, params string[] Args);
internal sealed record JsFunctionCall(KeystoneImportObjects Object, string Name, params object?[] Args);

public sealed class ListUiOptions
{
    public string? Label { get; init; }
    public bool HideCreate { get; init; }
    public bool HideDelete { get; init; }
    public ListViewOptions? ListView { get; init; }
}