using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Assignment_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // Variables
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;

        

        //function to encrypt 
        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        //funnction to decrypt
        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        //on click event for Button 1 AKA to encrypt
        private void Button1_Click(object sender, EventArgs e)
        {
            plaintext = ByteConverter.GetBytes(textBox1.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            textBox2.Text = ByteConverter.GetString(encryptedtext);
        }

        //on click event for Button 2 AKA to decrypt
        private void Button2_Click(object sender, EventArgs e)
        {
            byte[] decryptedtex = Decryption(encryptedtext, RSA.ExportParameters(true), false);
            textBox3.Text = ByteConverter.GetString(decryptedtex);
        }




        // salt the text and display it in textbox 3
        private void Button3_Click(object sender, EventArgs e)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";
            string password = textBox1.Text;
            byte[] bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(salt + password));
            string computedHash = BitConverter.ToString(bytes);

            //print the salted text into textBox4
            textBox4.Text = computedHash;


        }
    }
}
