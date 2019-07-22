using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class JimSafety : MonoBehaviour {


    private static string S_KEY = "NIt1znT76oWV2e2H8hWms29c5lOZwY27xYp0HHGArow=";
    private static string S_IV = "fzH3GG78Nw5Hkh8JEKk20g==";


    //private string A_KEY = "/NbVMvOzw/gWFm2IIO0HJfrx4gSN/8G1";
    //private string A_IV = "AoXD4uovcGs=";
    //private string sKeyB = "v1dp/3hXIVrSFVPcpSTMe2jzmuJA18RqQUHFDYFhH4Q=";
    //private string sIvB = "EVzrTdLidpGzQ8Di1Jt4LA==";

        /* 
                bKey = RijndaelManaged.Create().Key;
                bIV = RijndaelManaged.Create().IV;
                sKey = Convert.ToBase64String(bKey);
                sIV = Convert.ToBase64String(bIV);
                foreach (Byte b in bKey) { sKey += "chr(" + b + ")."; }
                foreach (Byte b in bIV) { sIV += "chr(" + b + ")."; }



        //string encryptedTextA = EncryptAES(contantBefore, sKeyA, sIvB);
        //string encryptedTextB = EncryptAES();
       
        //string contantAfter = DecryptAES(a, sKey, sIV);
        //WWWForm form = new WWWForm();
        //form.AddField("p", contantBefore);
        //form.AddField("encryptedText", a);
        //JimHtml.PostText("http://192.168.0.3/~chenjianzhao/common/JimSafety.php", form, (result, isError) => { Debug.Log(result); }, this);

*/

    public static string GetMd5Hash(string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    // Verify a hash against a string.
    public static bool VerifyMd5Hash(string input, string md5Hash)
    {
        // Hash the input.
        string hashOfInput = GetMd5Hash(input);
        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        return 0 == comparer.Compare(hashOfInput, md5Hash);
    }






    /*
    bKey = rijndaelCipher.Key;
    bIV = rijndaelCipher.IV;
    sKey = Convert.ToBase64String(rijndaelCipher.Key);
    sIV = Convert.ToBase64String(rijndaelCipher.IV);
    */
    public static string EncryptAES(string plainText)
    {
        return EncryptAES(plainText, S_KEY, S_IV);
    }


    public static string EncryptAES(string plainText, string key, string iv)
    {
        SymmetricAlgorithm sa = new RijndaelManaged();
        sa = SetUp(sa, key, iv);
        ICryptoTransform ct = sa.CreateEncryptor();
        byte[] plainData = Encoding.UTF8.GetBytes(plainText);
        byte[] encryptedData = ct.TransformFinalBlock(plainData, 0, plainData.Length);
        return Convert.ToBase64String(encryptedData);
    }

    public static string DecryptAES(string encryptedText, string key, string iv)
    {
        SymmetricAlgorithm sa = new RijndaelManaged();
        sa = SetUp(sa, key, iv);
        ICryptoTransform ct = sa.CreateDecryptor();
        byte[] encryptedData = Convert.FromBase64String(encryptedText);
        byte[] plainText = ct.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        return Encoding.UTF8.GetString(plainText);
    }






    //构造一个对称算法

    /// <summary>
    /// 字符串的加密
    /// </summary>
    /// <param name="plainText">要加密的字符串</param>
    /// <param name="key">密钥，必须32位</param>
    /// <param name="iv">向量，必须是12个字符</param>
    /// <returns>加密后的字符串</returns>
    public static string Encrypt3DES(string plainText, string key, string iv)
    {

        SymmetricAlgorithm sa = new TripleDESCryptoServiceProvider();//  Get3DES(key, iv);
        sa = SetUp(sa, key, iv);
        ICryptoTransform ct = sa.CreateEncryptor();//创建加密对象
        byte[] plainData = Encoding.UTF8.GetBytes(plainText);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
        cs.Write(plainData, 0, plainData.Length);
        cs.FlushFinalBlock();
        cs.Close();
        return Convert.ToBase64String(ms.ToArray());
    }


    /// <summary>
    /// 解密字符串
    /// </summary>
    /// <param name="encryptedText">加密后的字符串</param>
    /// <param name="key">密钥，必须32位</param>
    /// <param name="iv">向量，必须是12个字符</param>
    /// <returns>解密后的字符串</returns>
    public static string Decrypt3DES(string encryptedText, string key, string iv)
    {
        //构造一个对称算法
        SymmetricAlgorithm sa = new TripleDESCryptoServiceProvider();
        sa = SetUp(sa, key, iv);
        ICryptoTransform ct = sa.CreateDecryptor();//创建对称解密对象 加密转换运算
        byte[] enctytedData = Convert.FromBase64String(encryptedText);
        MemoryStream ms = new MemoryStream();//内存流
        CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);//数据流连接到数据加密转换的流
        cs.Write(enctytedData, 0, enctytedData.Length);
        cs.FlushFinalBlock();
        cs.Close();
        return Encoding.UTF8.GetString(ms.ToArray());
    }



    public static SymmetricAlgorithm SetUp(SymmetricAlgorithm symmetricAlgorithm, string key, string iv)
    {
        symmetricAlgorithm.Mode = CipherMode.CBC;//指定加密的运算模式
        symmetricAlgorithm.Padding = PaddingMode.PKCS7;//获取或设置加密算法的填充模式
        symmetricAlgorithm.Key = Convert.FromBase64String(key);//将密钥转换成byte
        symmetricAlgorithm.IV = Convert.FromBase64String(iv);//将3DES的向量转换成byte
        //Debug.Log(symmetricAlgorithm.KeySize);
        return symmetricAlgorithm;
    }

    public static string RandomString(int length)
    {
        System.Random random = new System.Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
