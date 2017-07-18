using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Affine_Cipher
{
    public class AffineCipherEncryptor
    {
        // Magnitude of the shift (b)
        private const int MAGNITUDE_OF_SHIFT = 20;

        public static string Encrypt(string text, int key)
        {
            // Text after encryption
            StringBuilder encryptedText = new StringBuilder();

            // Chars in text
            char[] textChars = text.ToCharArray();

            // Encrypt each char in the text
            foreach (char currChar in textChars)
            {
                // If the character is a letter
                if (currChar >='A' && currChar <='Z')
                {
                    // Lookup value
                    int charVal = LetterConstants.LetterToNum[currChar];

                    int encryptedVal = MathMod(key * charVal + MAGNITUDE_OF_SHIFT, LetterConstants.NUM_CHARS);

                    // Because this is a captial letter, add the number of letters to our final result
                    encryptedVal += LetterConstants.NUM_CHARS;

                    // Lookup the letter for this encrypted value
                    char encryptedChar = LetterConstants.NumToLetter[encryptedVal];

                    encryptedText.Append(encryptedChar);
                }
                else if (currChar >='a' && currChar <='z')
                {
                    // Lookup value
                    int charVal = LetterConstants.LetterToNum[currChar];

                    int encryptedVal = MathMod(key * charVal + MAGNITUDE_OF_SHIFT, LetterConstants.NUM_CHARS);

                    // Lookup the letter for this encrypted value
                    char encryptedChar = LetterConstants.NumToLetter[encryptedVal];

                    encryptedText.Append(encryptedChar);
                }
                else
                {
                    // Unknown char (probably new line)
                    // Write the char back to the text
                    encryptedText.Append(currChar);
                }
            }

            return encryptedText.ToString();
        }

        public static string Decrypt(string text, int key)
        {
            int inverseOfKey = ModInverse(key, LetterConstants.NUM_CHARS);

            // Text after decryption
            StringBuilder decryptedText = new StringBuilder();

            // Chars in text
            char[] textChars = text.ToCharArray();

            // Decrypt each char in the text
            foreach (char currChar in textChars)
            {
                // If the character is a letter
                if (currChar >='A' && currChar <='Z')
                {
                    // Lookup value
                    int charVal = LetterConstants.LetterToNum[currChar];

                    int decryptedVal = MathMod(inverseOfKey * (charVal - MAGNITUDE_OF_SHIFT), LetterConstants.NUM_CHARS);

                    // Because this is a captial letter, add the number of letters to our final result
                    decryptedVal += LetterConstants.NUM_CHARS;

                    // Lookup the letter for this encrypted value
                    char decryptedChar = LetterConstants.NumToLetter[decryptedVal];

                    decryptedText.Append(decryptedChar);
                }
                else if (currChar >='a' && currChar <='z')
                {
                    // Lookup value
                    int charVal = LetterConstants.LetterToNum[currChar];

                    int decryptedVal = MathMod(inverseOfKey * (charVal - MAGNITUDE_OF_SHIFT), LetterConstants.NUM_CHARS);

                    // Lookup the letter for this encrypted value
                    char decryptedChar = LetterConstants.NumToLetter[decryptedVal];

                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    // Unknown char (probably new line)
                    // Write the char back to the text
                    decryptedText.Append(currChar);
                }
            }

            return decryptedText.ToString();
        }

        public static bool IsKeyValid(int key)
        {
            // keys are valid if they are coprime with NUM_CHARS
            return GCD(key, LetterConstants.NUM_CHARS) == 1;
        }

        private static int GCD(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }

            if (b == 0)
            {
                return a;
            }

            if (a > b)
            {
                return GCD(a % b, b);
            }
            else
            {
                return GCD(a, b % a);
            }
        }

        private static int ExtendedGCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            int x1;
            int y1;

            int gcd = ExtendedGCD(b % a, a, out x1, out y1);

            x = y1 - (b/a) * x1;
            y = x1;

            return gcd;
        }

        private static int ModInverse(int a, int mod)
        {
            int x;
            int y;
            int gcd = ExtendedGCD(a, LetterConstants.NUM_CHARS, out x, out y);
           
            int inverseOfA = MathMod(x, mod);
            
            return inverseOfA;
        }

        private static int MathMod(int a, int mod)
        {
            return (a % mod + mod) % mod;
        }
    }
}