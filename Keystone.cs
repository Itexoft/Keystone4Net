using System.Text;
using Keystone4Net.CodeGeneration;
using Keystone4Net.Common;
using Keystone4Net.Enums;
using Microsoft.EntityFrameworkCore;

namespace Keystone4Net;

public class Keystone(DbContext db)
{
    public string GenerateKeystone()
    {
        var json = new KeystoneOptionsSerializer().Serialize(db);
        var sb = new StringBuilder();
        foreach (var value in  Enum.GetValues<KeystoneImportObjects>())
        {
            var importFrom = value == KeystoneImportObjects.Core ? "" : "/" + value;
            sb.Append($"import * as {value} from '@keystone-6/core{importFrom}';");
        }
        
        sb.AppendLine();
        sb.AppendLine($"export default core.config({json})");

        return sb.ToString();
    }
}