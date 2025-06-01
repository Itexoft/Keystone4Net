namespace Keystone4Net.Entities;

public class KeystoneFieldOptions
{
    public KeystoneFieldAccess? Access { get; set; }
    public KeystoneFieldHooks? Hooks { get; set; }
    public string? Label { get; set; }
    public bool? IsFilterable { get; set; }
    public bool? IsOrderable { get; set; }
    public KeystoneFieldUiOptions? Ui { get; set; }
    public KeystoneIndex? IsIndexed { get; set; }
    public KeystoneFieldGraphqlOptions? Graphql { get; set; }
}
