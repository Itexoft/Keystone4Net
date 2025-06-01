namespace Keystone4Net.Entities;

public abstract class KeystoneHooks
{
    public KeystoneJsFunction? ResolveInput { get; set; }

    public KeystoneJsFunction? ResolveInputCreate { get; set; }

    public KeystoneJsFunction? ResolveInputUpdate { get; set; }

    public KeystoneJsFunction? Validate { get; set; }

    public KeystoneJsFunction? ValidateCreate { get; set; }

    public KeystoneJsFunction? ValidateUpdate { get; set; }

    public KeystoneJsFunction? ValidateDelete { get; set; }

    public KeystoneJsFunction? BeforeOperation { get; set; }

    public KeystoneJsFunction? BeforeOperationCreate { get; set; }

    public KeystoneJsFunction? BeforeOperationUpdate { get; set; }

    public KeystoneJsFunction? BeforeOperationDelete { get; set; }

    public KeystoneJsFunction? AfterOperation { get; set; }

    public KeystoneJsFunction? AfterOperationCreate { get; set; }

    public KeystoneJsFunction? AfterOperationUpdate { get; set; }

    public KeystoneJsFunction? AfterOperationDelete { get; set; }
}

public class KeystoneListHooks : KeystoneHooks
{
}

public class KeystoneFieldHooks : KeystoneHooks
{
}