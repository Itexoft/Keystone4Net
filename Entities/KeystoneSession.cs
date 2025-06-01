using Keystone4Net.Enums;

namespace Keystone4Net.Entities;

public abstract class KeystoneSession(string name) : KeystoneJsFunctionPropArgCall(KeystoneImportObjects.Session, name, null)
{
}

public abstract class KeystoneCookieSession(string name) : KeystoneSession(name)
{
    public string Secret { get; set; } = string.Empty;

    public KeystoneIronOptions? IronOptions { get; set; }

    public int MaxAge { get; set; }

    public string? CookieName { get; set; }

    public bool? Secure { get; set; }

    public string? Path { get; set; }

    public string? Domain { get; set; }

    public KeystoneCookieSameSite? SameSite { get; set; }
}

public class KeystoneStatelessSession() : KeystoneCookieSession("statelessSessions")
{
}

public class KeystoneStoredSession() : KeystoneCookieSession("storedSessions")
{
    public KeystoneJsFunction? Store { get; set; }
}


