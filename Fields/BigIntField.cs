using System.Numerics;
using Keystone4NET.Models;

namespace Keystone4NET.Fields;

public class KeystoneBigIntField(string name) : KeystoneField<KeystoneBigIntDbOptions, KeystoneBigIntUiOptions>(name, "bigInt")
{
    public KeystoneJsValue<BigInteger, KeystoneFieldDefaultValue>? DefaultValue { get; set; }

    public KeystoneBigIntValidation? Validation { get; set; }
}

public class KeystoneBigIntDbOptions : KeystoneFieldDbOptions
{
}

public class KeystoneBigIntValidation
{
    public bool? IsRequired { get; set; }

    public BigInteger? Min { get; set; }

    public BigInteger? Max { get; set; }
}

public class KeystoneBigIntUiOptions : KeystoneFieldUiOptions
{
}