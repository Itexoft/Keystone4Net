using Keystone4Net.CodeGeneration;
using Keystone4Net.Enums;

namespace Keystone4Net.Settings;

public sealed class Keystone
{
    public KeystoneDbProvider DbProvider { get; set; }
    public string DbUrl { get; set; } = string.Empty;
    public JsFunctionCall? Session { get; set; }
    public UiSettings? Ui { get; set; }
    public List<KeystoneList> Lists { get; } = [];

    public Keystone AddList(KeystoneList list)
    {
        Lists.Add(list);
        return this;
    }
}

public sealed class UiSettings
{
    public bool IsDisabled { get; set; }
    public JsFunction? IsAccessAllowed { get; set; }
}

public sealed class SessionSettings
{
    public string Secret { get; set; } = string.Empty;
    public int MaxAge { get; set; }
}