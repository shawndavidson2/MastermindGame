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
            int attemptsLeft = 10;

            Console.WriteLine("Welcome to Mastermind!");
            Console.WriteLine("Guess the 4-digit code (digits 1-6). You have 10 attempts.");

            while (attemptsLeft > 0) 
            {
                Console.Write($"Attempt {11 - attemptsLeft}: Enter your guess: ");

                string guess = Console.ReadLine();
                while (!IsValidGuess(guess))
                {
                    Console.WriteLine("Invalid input. Please enter exactly 4 digits between 1 and 6.");
                    Console.Write($"Attempt {11 - attemptsLeft}: Enter your guess: ");
                    guess = Console.ReadLine();
                }

                --attemptsLeft;
            }
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

        /**
        * Validates player's guess to make sure it is exactly 4 digits and each digit is between 1 and 6
        *
        * @param guess A string representing the player's guess
        *
        * @return 'true' if the guess is exactly 4 characters long and consists only of digits '1'-'6', otherwise 'false'
        */
        public static bool IsValidGuess(string guess)
        {
            return guess.Length == 4 && guess.All(c => c >= '1' && c <= '6');
        }
    }
}