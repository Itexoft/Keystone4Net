using Keystone4Net.Enums;

namespace Keystone4Net.CodeGeneration;

public sealed class ListInitialSortOptions
{
    public string Field { get; init; } = string.Empty;
    public KeystoneSortDirection Direction { get; init; }
}

public sealed class ListViewOptions
{
    public KeystoneFieldMode? DefaultFieldMode { get; init; }
    public string[] InitialColumns { get; init; } = [];
    public ListInitialSortOptions? InitialSort { get; init; }
    public int? PageSize { get; init; }
}

public sealed record JsFunction(string Body, params string[] Args);
internal sealed record JsFunctionCall(KeystoneImportObjects Object, string Name, params object?[] Args);

public class ViewOptions
{
    public KeystoneFieldMode DefaultFieldMode { get; init; }
}

public sealed class ListUiOptions
{
    public string? Label { get; init; }
    public string? LabelField { get; init; }
    public string[]? SearchFields { get; init; }
    public string? Description { get; init; }
    public bool HideNavigation { get; init; }
    public bool HideCreate { get; init; }
    public bool HideDelete { get; init; }
    public ViewOptions? CreateView { get; init; }
    public ViewOptions? ItemView { get; init; }
    public ListViewOptions? ListView { get; init; }
    public string? Singular { get; init; }
    public string? Plural { get; init; }
    public string? Path { get; init; }
}

public sealed class TextFieldOptions
{
    public TextValidationOptions? Validation { get; init; }
    public TextUiOptions? Ui { get; init; }
    public KeystoneIndex IsIndexed { get; init; }
}

public class FieldUiOptions
{
    public string? Views { get; init; }
    public ViewOptions? CreateView { get; init; }
    public ItemViewOptions? ItemView { get; init; }
    public ViewOptions? ListView { get; init; }
}

public sealed class ItemViewOptions : ViewOptions
{
    public KeystoneFieldPosition FieldPosition { get; init; }
}

public sealed class TextValidationOptions
{
    public bool IsRequired { get; init; }
    public TextLengthOptions? Length { get; init; }
    public TextMatchOptions? Match { get; init; }
}

public sealed class TextLengthOptions
{
    public int Min { get; init; }
    public int? Max { get; init; }
}

public sealed class TextMatchOptions
{
    public string Regex { get; init; } = string.Empty;
    public string? Explanation { get; init; }
}

public sealed class TextUiOptions : FieldUiOptions
{
    public KeystoneDisplayMode DisplayMode { get; init; }
}
