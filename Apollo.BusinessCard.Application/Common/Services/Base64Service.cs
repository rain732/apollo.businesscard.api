using Apollo.BusinessCard.Application.Common.Interfaces;

namespace Apollo.BusinessCard.Application.Common.Services;

public class Base64Service : IBase64Service
{
    public string Encrypt(string base64) => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(base64));
    public string Decrypt(string encryptedBase64) => System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encryptedBase64));
}
