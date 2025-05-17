using Keystone4Net.Enums;
using Microsoft.EntityFrameworkCore;

namespace Keystone4Net.Entities;

public class KeystoneConfig : KeystoneJsFunctionPropArgCall
{
    internal KeystoneConfig(DbContext dbContext, string? baseDir) : base(KeystoneImportObjects.Core, "config", null)
    {
        this.Db = new(dbContext, baseDir);
    }

    internal KeystoneDb Db { get; }

    public KeystoneSession? Session { get; set; }

    public KeystoneUiSettings? Ui { get; set; }

    public Dictionary<string, KeystoneList> Lists { get; } = [];

    public void Add<T>(KeystoneList<T> value)
    {
        var typeName = typeof(T).Name;
        this.Lists.Add(typeName, value);
    }
}