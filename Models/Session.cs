namespace Keystone4NET.Models;

public abstract class KeystoneSession(string funcName) : KeystoneJsFunctionObjectCall(KeystoneImport.Session, funcName);

public abstract class KeystoneCookieSession(string n) : KeystoneSession(n)
{
    public required string Secret { get; set; }

    public int? MaxAge { get; set; }

    public string? CookieName { get; set; }

    public bool? Secure { get; set; }

    public string? Path { get; set; }

    public string? Domain { get; set; }

    public KeystoneJsValue<bool, KeystoneCookieSameSite>? SameSite { get; set; }
}

public class KeystoneStatelessSession() : KeystoneCookieSession("statelessSessions");

public class KeystoneStoredSession() : KeystoneCookieSession("storedSessions")
{
    public KeystoneJsFunction? Store { get; set; }
}