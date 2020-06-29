using System;
using System.Collections.Generic;
using System.Text;
using  System.Security.Cryptography;

namespace vcs
{
    public class Hash
    {
        private string _hash;

        public Hash(string content)
        {
            _hash = EncodeContent(content);
        }

        public string GetContent()
        {
            return _hash;
        }

        private string EncodeContent(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);
            var hash = string.Empty;
            foreach (var b in hashData)
            {
                hash += b.ToString("X2");
            }                

            return hash;
        }
    }
}