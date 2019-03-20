using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.ComponentModel;

namespace RivestShamirAdlemanRSA
{
    public class RSA
    {
        public void GeneratePublicAndPrivateKeyData(string strToEncrypt)
        {
            try
            {
                // Create a UnicodeEncoder to convert between byte array and string.
                var ByteConverter = new UnicodeEncoding();

                // Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes(strToEncrypt);
                byte[] encryptedData;
                byte[] decryptedData;

                // Create a new instance of RSACryptoServiceProvider to generate public and private key data.
                using (var RSA = new RSACryptoServiceProvider())
                {
                    // Pass the data to ENCRYPT, 
                    // the public key information (using RSACryptoServiceProvider.ExportParameters(false), and a boolean flag specifying no OAEP padding.
                    encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                    // Pass the data to DECRYPT, 
                    // the private key information (using RSACryptoServiceProvider.ExportParameters(true), and a boolean flag specifying no OAEP padding.
                    decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                    // Display the encrypted text to the console.
                    Console.WriteLine("Encrypted {0}", Convert.ToBase64String(encryptedData, 0, encryptedData.Length));

                    // Display the decrypted plaintext to the console.
                    Console.WriteLine("Decrypted plaintext {0}", ByteConverter.GetString(decryptedData));
                }
            }
            catch (ArgumentNullException)
            {
                // Catch this exception in case the encryption did not succeed.
                Console.WriteLine("Encryption failed.");                
            }
        }

        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;

                // Create new instance of RSACryptoServiceProvider.
                using (var RSA = new RSACryptoServiceProvider())
                {
                    // Import the RSA Key information. This only needs to include the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding. 
                    // OAEP padding is only available on Microsoft Windows XP or later.
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            // Catch and display a CryptographicException to the console.
            catch (CryptographicException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;

                // Create a new instance of RSACryptoServiceProvider.
                using (var RSA = new RSACryptoServiceProvider())
                {
                    // Import the RSA Key information. This needs to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    // Decrypt the passed byte array and specify OAEP padding.  
                    // OAEP padding is only available on Microsoft Windows XP or later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            // Catch and display a CryptographicException to the console.
            catch (CryptographicException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
