using System.Text;
using Azure.Security.KeyVault.Keys.Cryptography;
using Microsoft.Extensions.Azure;

namespace N_Chat.Client.Services;

public class KeyVaultService : IKeyVaultService
{
    private readonly KeyClient keyClient;
    private readonly CryptographyClient cryptoClient;
    private readonly KeyVaultKey vaultKey;
    public KeyVaultService(KeyClient keyClient, CryptographyClient cryptoClient, KeyVaultKey vaultKey)
    {
        this.keyClient = keyClient;
        this.cryptoClient = cryptoClient;
        this.vaultKey = vaultKey;
        /*keyClient = new KeyClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        vaultKey = keyClient.GetKey("[MessageKey]");
        cryptoClient = new CryptographyClient(vaultKey.Id, new DefaultAzureCredential());*/
    }
    public async Task<string> EncryptStringAsync(string input)
    {
        KeyVaultKey key = keyClient.GetKey("[MessageKey]");
        CryptographyClient cryptoClient = new CryptographyClient(key.Id, new DefaultAzureCredential());
        byte[] inputAsByteArray = Encoding.UTF8.GetBytes(input);

        EncryptResult encryptResult =  await cryptoClient.EncryptAsync(EncryptionAlgorithm.RsaOaep, inputAsByteArray);

        return Convert.ToBase64String(encryptResult.Ciphertext);
    }
    public async Task<string> DecryptStringAsync(string input)
    {
        KeyVaultKey key = keyClient.GetKey("[MessageKey]");
        CryptographyClient cryptoClient = new CryptographyClient(key.Id, new DefaultAzureCredential());
        
        byte[] inputAsByteArray = Convert.FromBase64String(input);

        DecryptResult decryptResult = await cryptoClient.DecryptAsync(EncryptionAlgorithm.RsaOaep, inputAsByteArray);

        return Encoding.Default.GetString(decryptResult.Plaintext);
    }
}

public interface IKeyVaultService
{
    Task<string> EncryptStringAsync(string input);
    Task<string> DecryptStringAsync(string input);
}