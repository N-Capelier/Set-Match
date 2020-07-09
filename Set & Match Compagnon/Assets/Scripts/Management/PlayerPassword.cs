using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Player.Encryption
{
    public struct Password
    {
        public StringBuilder password;
        public string key;

        public Password(StringBuilder password, string key)
        {
            this.password = password;
            this.key = key;
        }
    }

    /// <summary>
    /// NCO
    /// </summary>
    public static class PlayerPassword
    {
        const string glyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789<>&/*-=+;.:/!§%^¨$£_-#~'{([|`@)]}";

        public static Password CreateEncryptedPassword(string password)
        {
            string key = GenerateRandomString();

            string firstPart = "", secondPart = "";

            for (int i = 0; i < key.Length * 0.5f; i++)
            {
                firstPart += key[i];
            }

            for (int i = (int)(key.Length * 0.5f); i < key.Length; i++)
            {
                secondPart += key[i];
            }

            password = firstPart + key + secondPart;

            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return new Password(strBuilder, key);
        }

        public static Password CreateEncryptedPassword(string password, string key)
        {
            string firstPart = "", secondPart = "";

            for (int i = 0; i < key.Length * 0.5f; i++)
            {
                firstPart += key[i];
            }

            for (int i = (int)(key.Length * 0.5f); i < key.Length; i++)
            {
                secondPart += key[i];
            }

            password = firstPart + key + secondPart;

            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return new Password(strBuilder, key);
        }

        public static string GenerateRandomString()
        {
            int charAmount = Random.Range(10, 20);

            string key = "";

            for (int i = 0; i < charAmount; i++)
            {
                key += glyphs[Random.Range(0, glyphs.Length)];
            }

            return key;
        }

    }

}