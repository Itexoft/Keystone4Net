using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneVirtualField(string name) : KeystoneField<KeystoneVirtualFieldDbOptions, KeystoneVirtualUiOptions>(name, "virtual")
{
    public required KeystoneGraphqlField Field { get; set; }
}

public class KeystoneVirtualFieldDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneVirtualUiOptions : KeystoneFieldUiOptions
{
    public string? Query { get; set; }
}

public class KeystoneGraphqlObject(string name) : KeystoneJsObject(KeystoneImport.Graphql, name);

public class KeystoneGraphqlField() : KeystoneJsFunctionObjectCall(KeystoneImport.Graphql, "field")
{
    public required KeystoneGraphqlObject Type { get; set; }

    public Dictionary<string, KeystoneGraphqlObject>? Args { get; set; }

    public required KeystoneJsFunction Resolve { get; set; }
}