using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurePasswordsOpg
{
    public class Hash
    {
        //makes the salt
        public static byte[] SaltMaker()
        {
            //use to determend the length of the byte array
            const int saltLength = 32;

            //makes the RNG
            //makes the byte array, set the length og the arrray with the int
            var RNG = RandomNumberGenerator.Create();
            byte[] rndBytes = new byte[saltLength];
            RNG.GetBytes(rndBytes);

            return rndBytes;
        }

        //adding the password and salt arrays togeter
        public static byte[] CombiningArrays(byte[] first, byte[] second)
        {
            var combined = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, combined, 0, first.Length);
            Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);

            return combined;
        }

        //hashing the combined password and salt array
        public static byte[] HPassWSalt(byte[] toBeHashed, byte[] salt)
        {
            var sha256 = SHA256.Create();            
            return sha256.ComputeHash(CombiningArrays(toBeHashed, salt));            
        }
    }
}
