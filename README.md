# Keystone4NET

Keystone4NET is a .NET helper library for generating KeystoneJS configuration from Entity Framework Core models.
It automates creation of lists and fields so you can build a Keystone application with minimal JavaScript code.

## Usage

1. Install the NuGet package `Keystone4NET`.
2. Implement `IKeystoneDbContext` in your `DbContext` and configure lists in `ConfigureKeystone`.
3. Use the `Keystone<T>` class to generate and build your Keystone project.

```csharp
public class AppDbContext : DbContext, IKeystoneDbContext
{
    public DbSet<User> Users => Set<User>();

    public void ConfigureKeystone(KeystoneConfig config)
    {
        var users = new KeystoneList<User>(KeystoneListAccess.AllowAll);
        users.Add(nameof(User.Name), new KeystoneTextField());
        config.Add(users);
    }
}
```

```csharp
var keystone = new Keystone<AppDbContext>(db, "/path/to/keystone");
await keystone.InstallAsync();
await keystone.BuildAsync();
```

Call `keystone.Start()` to run the development server.

All basic types and enums are located in the `Model` folder.
