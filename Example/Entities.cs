namespace Example.Entities;

public class General
{
    public int Id { get; set; }
    public string Endpoint { get; set; } = string.Empty;
}

public class Model
{
    public int Id { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public bool OverridePrompt { get; set; }
    public string PromptText { get; set; } = string.Empty;
}
