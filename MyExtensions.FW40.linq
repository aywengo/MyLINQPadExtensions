<Query Kind="Program" />

public static class MyExtensions
{		
	#region JSON
	
	public static object DumpJson(this object value, string description = null)
	   {
			  return GetJsonDumpTarget(value).Dump(description);
	   }	 

	public static object DumpJson(this object value, string description, int depth)
	   {
			  return GetJsonDumpTarget(value).Dump(description, depth);
	   }    	 

	public static object DumpJson(this object value, string description, bool toDataGrid)
	   {
			  return GetJsonDumpTarget(value).Dump(description, toDataGrid);
	   }    	 

	private static object GetJsonDumpTarget(object value)
	   {
			  object dumpTarget = value;
			  //if this is a string that contains a JSON object, do a round-trip serialization to format it:
			  var stringValue = value as string;
			  if (stringValue != null)
			  {
					 if (stringValue.Trim().StartsWith("{"))
					 {
						   var obj = JsonConvert.DeserializeObject(stringValue);
						   dumpTarget = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
					 }
					 else
					 {
						   dumpTarget = stringValue;
					 }
			  }
			  else
			  {
			  	dumpTarget = JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented);
			  }
			  return dumpTarget;
	   }
	   
	#endregion JSON   
	
	#region Crypto
	public static string Encrypt_Aes(this object value, string aesKey, string aesIV)
        {
            string plainText = value as string;
			byte[] Key = GetBytes(aesKey);
			byte[] IV = GetBytes(aesIV);
			// Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (System.Security.Cryptography.AesCryptoServiceProvider aesAlg = new System.Security.Cryptography.AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                System.Security.Cryptography.ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream csEncrypt = new System.Security.Cryptography.CryptoStream(msEncrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            
			return encrypted.GetString();
        }

    public static string Decrypt_Aes(this object value, string aesKey, string aesIV)
        {
            byte[] cipherText = GetBytes(value as string);
			byte[] Key = GetBytes(aesKey);
			byte[] IV = GetBytes(aesIV);
			// Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (System.Security.Cryptography.AesCryptoServiceProvider aesAlg = new System.Security.Cryptography.AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                System.Security.Cryptography.ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (System.Security.Cryptography.CryptoStream csDecrypt = new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
	
	public static string Encrypt_Des(this object value, string desKey, string desIV)
		{
    		string originalString = value as string;
			byte[] Key = GetBytes(desKey);
			byte[] IV = GetBytes(desIV);
			if (String.IsNullOrEmpty(originalString))
				throw new ArgumentNullException("plainText");
			if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
    		System.Security.Cryptography.DESCryptoServiceProvider cryptoProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
	    	MemoryStream memoryStream = new MemoryStream();
    		System.Security.Cryptography.CryptoStream cryptoStream 
				= new System.Security.Cryptography.CryptoStream(memoryStream,         
																cryptoProvider.CreateEncryptor(Key, IV),
																System.Security.Cryptography.CryptoStreamMode.Write);
    		StreamWriter writer = new StreamWriter(cryptoStream);
    		writer.Write(originalString);
    		writer.Flush();
    		cryptoStream.FlushFinalBlock();
    		writer.Flush();
    		return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
		}
	
	public static string Decrypt_Des(this object value, string desKey, string desIV)
		{
		    string cryptedString = value as string;
			byte[] Key = GetBytes(desKey);
			byte[] IV = GetBytes(desIV);
			// Check arguments.
            if (string.IsNullOrEmpty(cryptedString))
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
    		System.Security.Cryptography.DESCryptoServiceProvider cryptoProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
    		MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
    		System.Security.Cryptography.CryptoStream cryptoStream 
				= new System.Security.Cryptography.CryptoStream(memoryStream, 
																cryptoProvider.CreateDecryptor(Key, IV), 
																System.Security.Cryptography.CryptoStreamMode.Read);
    		StreamReader reader = new StreamReader(cryptoStream);
    		return reader.ReadToEnd();
	}
			
	#endregion Crypto
	
	#region helpers
	public static byte[] GetBytes(this string str)
		{
			byte[] bytes = new byte[str.Length];
			var chars = str.ToCharArray();
			for(int i=0; i<str.Length; i++)
			{
				bytes[i] = (byte)chars[i];
			}
    		return bytes;
		}

	public static string GetString(this byte[] bytes)
		{
			char[] chars = new char[bytes.Length];
			for(int i=0; i<bytes.Length; i++)
			{
				chars[i] = (char)bytes[i];
			}    	
    		return new string(chars);
		}
	
	public static string GetHex(this string str)	
		{
			char[] values = str.ToCharArray();
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < values.Length; i++)
			{
				sb.AppendFormat(String.Format("{0:X}", Convert.ToInt32(values[i])));
				if ((i % 4) == 3) 
					sb.AppendFormat(" ");
			}			
			return sb.ToString();
		}
	#endregion helpers
}