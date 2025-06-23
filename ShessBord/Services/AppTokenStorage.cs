using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ShessBord.Interfaces;
using ShessBord.Models;

namespace ShessBord.Services;

public class AppTokenStorage : IAppTokenStorage
{
    private const int KeySize = 32;
    private const int IvSize = 16; 
    private const int SaltSize = 16;
    private const int Iterations = 100_000;

    private readonly string _storagePath;
    private readonly string _password = "*****************************";

    public AppTokenStorage()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appData, "ShessBord");
        Directory.CreateDirectory(appFolder);
        _storagePath = Path.Combine(appFolder, "tokens.dat");
    }

    public void SaveTokens(string accessToken, string refreshToken , string userId)
    {
        var data = new TokenData { AccessToken = accessToken, RefreshToken = refreshToken , UserId = userId };
        var json = JsonSerializer.Serialize(data);
        var plainBytes = Encoding.UTF8.GetBytes(json);

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        using var aes = Aes.Create();
        aes.KeySize = KeySize * 8;
        aes.GenerateIV();

        using var keyDerive = new Rfc2898DeriveBytes(_password, salt, Iterations, HashAlgorithmName.SHA256);
        aes.Key = keyDerive.GetBytes(KeySize);

        using var encryptor = aes.CreateEncryptor();
        using var ms = new MemoryStream();
        ms.Write(salt, 0, SaltSize);
        ms.Write(aes.IV, 0, IvSize);

        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            cs.Write(plainBytes, 0, plainBytes.Length);

        File.WriteAllBytes(_storagePath, ms.ToArray());
    }

    public (string AccessToken, string RefreshToken, string userId) GetTokens()
    {
        if (!File.Exists(_storagePath))
            return (null, null, null)!;
        var allBytes = File.ReadAllBytes(_storagePath);
        var salt = allBytes[..SaltSize];
        var iv = allBytes[SaltSize..(SaltSize + IvSize)];
        var cipherBytes = allBytes[(SaltSize + IvSize)..];

        using var aes = Aes.Create();
        aes.KeySize = KeySize * 8;
        aes.IV = iv;

        using var keyDerive = new Rfc2898DeriveBytes(_password, salt, Iterations, HashAlgorithmName.SHA256);
        aes.Key = keyDerive.GetBytes(KeySize);

        using var decryptor = aes.CreateDecryptor();
        using var ms = new MemoryStream(cipherBytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        var json = sr.ReadToEnd();
        var tokens = JsonSerializer.Deserialize<TokenData>(json);
        return (tokens?.AccessToken, tokens?.RefreshToken, tokens?.UserId)!;
    }

    public void ClearTokens()
    {
        if (File.Exists(_storagePath))
            File.Delete(_storagePath);
    }
}