using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
using System.Diagnostics;

namespace SecurePasswordsOpg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //password
            string pw = "MyLittelSecret";

            //PBKDF2 config
            int numberOfRounds = 10000;

            var saltmakerfromsalt = Salt.SaltMaker();
            var pwtobehashedfromsalt = Salt.PasswordToBeHashed(Encoding.UTF8.GetBytes(pw), saltmakerfromsalt, numberOfRounds);

            var saltmakerfromhash = Hash.SaltMaker();
            var pwtobecombinedfromhash = Hash.CombiningArrays(Encoding.UTF8.GetBytes(pw), saltmakerfromhash);
            var combinedpwandsalt = Hash.HPassWSalt(pwtobecombinedfromhash, saltmakerfromhash);

            try
            {
                if (File.Exists("C:\\Users\\ABP\\source\\repos\\SecurePasswordsOpg\\pretentDB.txt"))
                {
                    using (FileStream fs = File.OpenWrite("C:\\Users\\ABP\\source\\repos\\SecurePasswordsOpg\\pretentDB.txt"))
                    {
                        AddText(fs, "Password from the Class: \r\n");
                        AddText(fs, "Salt = " + Convert.ToBase64String(pwtobehashedfromsalt));
                        AddText(fs, "\r\n");
                        AddText(fs, "Hash = " + Convert.ToBase64String(combinedpwandsalt));
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e); 
                throw;
            }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] text = new UTF8Encoding(true).GetBytes(value);
            fs.Write(text, 0, text.Length);
        }
    }
}