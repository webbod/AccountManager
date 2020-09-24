using AccountManager.Domain.Models;
using AccountManager.Interfaces.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountManager.Data.Helpers
{
    /// <summary>
    /// A simple alternative to the md5 hash that transforms
    /// an input string and a salt string into deterministic garbage
    /// not all of the data in either the salt or the input is used
    /// all the time, the data that isn't used influences the obfuscation process
    /// you need to bruteforce it, different combinations of input and salt
    /// might generate the same output, but that won't help you recover a valid password
    /// </summary>
    internal class HashingService
    {
        private int _SaltMultiplier;
        private int _InputMultiplier;
        private int _EndPoint;
        private string _Salt;
        private string _Input;

        public HashingService(IAccountCredentials credentials)
        {
            AccountCredentials.TryValidate(credentials);
            _Salt = credentials.EmailAddress;
            _Input = credentials.PlainTextPassword;

            SetParameters();
        }

        /// <summary>
        /// Intercalates the input with the salt, rotates characters and shuffles the result,
        /// the intention is not to leak too much information about how the process works,
        /// the output is heavily influenced by the input and small changes can radically 
        /// alter the output
        /// </summary>
        /// <returns>deterministic garbage that looks a bit like an Md5 hash</returns>
        public string Execute()
        {
            var output = new List<char>();
            var counter = 0;
                                  
            while (counter < _EndPoint)
            {
                output.Add(EncodeCharacter(counter++));
            }

            return ExtractHashFragment(output);
        }

        /// <returns>this should be 32 characters worth of garbage</returns>
        private string ExtractHashFragment(List<char> hashedChars)
        {
            hashedChars.Reverse();
            return string.Concat(hashedChars.Skip(_SaltMultiplier + _Input.Length).Take(32));
        }

        private int GetOffsetChar(string source, int offset)
        {
            return (int)source[offset % source.Length];
        }

        /// <returns>the unicode charcter for the 'offset' position in the hash</returns>
        private char EncodeCharacter(int offset)
        {
            var inputCharCode = GetOffsetChar(_Input, offset);
            var saltCharCode = GetOffsetChar(_Input, offset); 
            
            var charCode = offset % 2 == 0 ? 
                inputCharCode + saltCharCode % _InputMultiplier : 
                saltCharCode + inputCharCode % _SaltMultiplier;

            try
            {
                return Convert.ToChar(charCode + offset);
            }
            catch (Exception)
            {
                return Convert.ToChar(inputCharCode);
            }
        }

        private void SetParameters()
        {
            SetSaltMultiplier();
            SetInputMultiplier();
            SetEndPoint();
        }

        private void SetSaltMultiplier()
        {
            _SaltMultiplier = _Salt.Length % 2 == 0 ?  17 : 29;
        }

        private void SetInputMultiplier()
        {
            _InputMultiplier = ((int)_Input[0] * (int)_Input.Last()) % 2 == 0 ?
                    ((int)_Input[2]) % 11 + 1:
                    ((int)_Input.Reverse().Skip(1).First()) % 37 + 1;
        }

        private void SetEndPoint()
        {
            var end = _SaltMultiplier * _Input.Length + _InputMultiplier * _Salt.Length + _Salt.Length + _Input.Length;
            _EndPoint = end > 349 ? 349 : end;
        }

        /// <param name="credentials"></param>
        /// <returns>a salted hash of the password</returns>
        public static string HashPassword(IAccountCredentials credentials)
        {
            return new HashingService(credentials).Execute();
        }
    }
}
