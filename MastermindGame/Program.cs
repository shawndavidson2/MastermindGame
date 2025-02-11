/*
Author: Shawn Davidson
Date: 2025-02-11
*/
using System;

namespace MastermindGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string secret = GenerateSecretCode();
            Console.WriteLine(secret);
        }

        /**
        * Generates a random 4-digit secret code with digits between 1 and 6
        * 
        * @return A string of exactly 4 characters, each being a digit from '1'-'6'
        */
        public static string GenerateSecretCode() 
        {
            Random random = new Random();
            char[] code = new char[4];

            for (int i = 0; i < 4; i++)
            {
                code[i] = (char)('1' + random.Next(0, 6));
            }

            return new string(code);
        }
    }
}