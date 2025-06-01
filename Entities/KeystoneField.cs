using Keystone4Net.Common;
using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public abstract class KeystoneField<TUiOptions> : KeystoneJsFunctionPropArgCall where TUiOptions : KeystoneFieldUiOptions
{
    protected KeystoneField(string name) : base(KeystoneImportObjects.Fields, name, null)
    {
    }

    public KeystoneFieldAccess? Access { get; set; }
    public KeystoneFieldHooks? Hooks { get; set; }
    public string? Label { get; set; }
    public bool? IsFilterable { get; set; }
    public bool? IsOrderable { get; set; }
    public TUiOptions? Ui { get; set; }
    public KeystoneIndex? IsIndexed { get; set; }
    public KeystoneFieldGraphqlOptions? Graphql { get; set; }
}

public abstract class KeystoneField : KeystoneField<KeystoneFieldUiOptions>
{
    protected KeystoneField(string name) : base(name)
    {
    }
}
