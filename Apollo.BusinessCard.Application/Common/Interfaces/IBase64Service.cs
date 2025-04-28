namespace Apollo.BusinessCard.Application.Common.Interfaces;

public interface IBase64Service
{
    string Encrypt(string base64);
    string Decrypt(string encryptedBase64);
}
