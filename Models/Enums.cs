using System.Text.Json.Serialization;

namespace Keystone4NET.Models;

public enum KeystoneCookieSameSite
{
    Lax,
    Strict,
    None
}

public enum KeystoneDbProvider
{
    Sqlite,
    Postgresql,
    Mysql
}

public enum KeystoneRelationshipDisplayMode
{
    Select,
    Cards,
    Count
}

public enum KeystoneTextDisplayMode
{
    Input,
    Textarea
}

public enum KeystoneFieldMode
{
    Edit,
    Read,
    Hidden
}

public enum KeystoneFieldPosition
{
    Form,
    Sidebar
}

public enum KeystoneSortDirection
{
    [JsonStringEnumMemberName("ASC")] Asc,
    [JsonStringEnumMemberName("DESC")] Desc
}

public enum KeystoneSelectValueType
{
    String,
    Enum,
    Integer
}

public enum KeystoneRemoveMode
{
    Disconnect,
    None
}

public enum KeystoneStorageKind
{
    Local,
    S3
}

public enum KeystoneStorageType
{
    File,
    Image
}

public enum KeystoneSelectUiMode
{
    Select,

    [JsonStringEnumMemberName("segmented-control")]
    SegmentedControl,
    Radio
}

public enum KeystoneDocumentRelationshipKind
{
    Inline,
    Block,
    Prop
}

public enum KeystoneGraphqlPlaygroundType
{
    Apollo
}

public enum KeystoneGraphqlCacheScope
{
    [JsonStringEnumMemberName("PUBLIC")] Public,
    [JsonStringEnumMemberName("PRIVATE")] Private
}

public enum KeystoneFieldDefaultValueKind
{
    Autoincrement,
    Now
}

public enum KeystoneIdFieldKind
{
    Cuid,
    Uuid,
    Autoincrement
}

public enum KeystoneIndexMode
{
    Unique
}

public enum KeystoneIdFieldType
{
    BigInt
}