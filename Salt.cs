using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SecurePasswordsOpg
{
    public class Salt
    {
        
        //makes the salt
        public static byte[] SaltMaker()
        {
            //use to determend the length of the byte array
            const int saltLength = 32;

            //makes the RNG
            //the byte array, set the length og the arrray with the int
            var RNG = RandomNumberGenerator.Create();
            byte[] rndBytes = new byte[saltLength];
            RNG.GetBytes(rndBytes);

            return rndBytes;
        }

        //uses PBKDF2
        public static byte[] PasswordToBeHashed(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            var deriveBytes = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, HashAlgorithmName.SHA256);
            return deriveBytes.GetBytes(32);
        }
    }
}