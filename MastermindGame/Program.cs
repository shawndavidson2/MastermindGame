/*
Author: Shawn Davidson
Date: 2025-02-11
*/
using System;

namespace MastermindGame
{
    public class Program
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

                string guess = Console.ReadLine()!;
                while (!IsValidGuess(guess))
                {
                    Console.WriteLine("Invalid input. Please enter exactly 4 digits between 1 and 6.");
                    Console.Write($"Attempt {11 - attemptsLeft}: Enter your guess: ");
                    guess = Console.ReadLine()!;
                }

                string hint = GetHint(secret, guess);
                if (hint == "++++")
                {
                    Console.WriteLine("Congratulations! You've won!");
                    return;
                }
                Console.WriteLine("Hint: " + hint);

                --attemptsLeft;
            }
            Console.WriteLine($"You've lost. The correct code was: {secret}");
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

        /** 
        * Generates the hint based on the player's guess and the secret code
        *
        * @param secret The secret code as a string of digits
        * @param guess The player's guess as a string of digits
        *
        * @return A string representing the hint, consisting of '+' and '-'
        */
        public static string GetHint(string secret, string guess)
        {
            int plusCount = CalculateExactMatches(secret, guess);
            int minusCount = CalculateCorrectDigitsInWrongPositions(secret, guess);
            return new string('+', plusCount) + new string('-', minusCount);
        }

        /**
        * Calculates the number of exact matches, where the guessed digit is in the correct position
        *
        * @param secret The secret code as a string of digits
        * @param guess The player's guess as a string of digits
        *
        * @return The number of correct digits in the correct position
        */
        private static int CalculateExactMatches(string secret, string guess)
        {
            int count = 0;
            for (int i = 0; i < secret.Length; i++)
            {
                if (secret[i] == guess[i]) count++;
            }
            return count;
        }

        /**
        * Calculates the number of correct digits that are in the wrong positions
        *
        * @param secret The secret code as a string of digits
        * @param guess The player's guess as a string of digits
        *
        * @return The number of correct digits that are in the wrong positions
        */
        private static int CalculateCorrectDigitsInWrongPositions(string secret, string guess)
        {
            // Remove exact matches from consideration
            List<char> remainingSecret = new List<char>();
            List<char> remainingGuess = new List<char>();

            for (int i = 0; i < secret.Length; i++)
            {
                if (secret[i] != guess[i])
                {
                    remainingSecret.Add(secret[i]);
                    remainingGuess.Add(guess[i]);
                }
            }

            // Calculate frequency of remaining characters in secret
            Dictionary<char, int> secretFrequency = remainingSecret
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());

            int minusCount = 0;
            foreach (char c in remainingGuess)
            {
                if (secretFrequency.ContainsKey(c) && secretFrequency[c] > 0)
                {
                    minusCount++;
                    secretFrequency[c]--;
                }
            }

            return minusCount;
        }
    }
}