using Example.Entities;
using Keystone4Net.CodeGeneration;
using Keystone4Net.Enums;
using Keystone4Net.Settings;

namespace Example;

public static class KeystoneBuilder
{
    public static Keystone Build()
    {
        var keystone = new Keystone
        {
            DbProvider = KeystoneDbProvider.Sqlite,
            DbUrl = "file:../database/db.sqlite",
            Session = new JsFunctionCall(
                KeystoneImportObjects.Session,
                "statelessSessions",
                new SessionSettings
                {
                    Secret = "uBqhTZVmYgUjcmG@GtVm9K#LCC7u?4:J",
                    MaxAge = 60 * 60 * 24 * 3650
                }
            ),
            Ui = new UiSettings
            {
                IsDisabled = false,
                IsAccessAllowed = new JsFunction("true")
            }
        };

        var general = new KeystoneList<General>("General")
        {
            Access = KeystoneListAccess.AllowAll,
            Ui = new ListUiOptions
            {
                HideCreate = true,
                HideDelete = true,
                Label = "General settings"
            }
        };
        general.AddField(
            "endpoint",
            KeystoneFieldType.Text,
            new TextFieldOptions
            {
                Validation = new TextValidationOptions { IsRequired = true }
            }
        );

        var model = new KeystoneList<Model>("Model")
        {
            Access = KeystoneListAccess.AllowAll,
            Ui = new ListUiOptions
            {
                ListView = new ListViewOptions { InitialColumns = ["modelName"] }
            }
        };
        model.AddField(
            "modelName",
            KeystoneFieldType.Text,
            new TextFieldOptions
            {
                Validation = new TextValidationOptions { IsRequired = true }
            }
        );
        model.AddField("overridePrompt", KeystoneFieldType.Checkbox);
        model.AddField(
            "promptText",
            KeystoneFieldType.Text,
            new TextFieldOptions
            {
                Ui = new TextUiOptions { DisplayMode = KeystoneDisplayMode.Textarea }
            }
        );

        keystone.AddList(general);
        keystone.AddList(model);
        return keystone;
    }
}
