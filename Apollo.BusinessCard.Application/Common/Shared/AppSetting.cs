namespace Apollo.BusinessCard.Application.Common.Shared;

public record AppSetting
{
    public string[] AllowedCrossOrign { get; set; }
    public APISecurity APISecurity { get; set; }
}

public record APISecurity
{
    public string Key { get; set; } = null!;
    public int Expiration { get; set; }
    public int RefreshExpiration { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Authority { get; set; } = null!;
}