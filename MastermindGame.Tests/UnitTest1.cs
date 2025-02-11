using Xunit;
using MastermindGame;
using System.Collections.Generic;
using System.Linq;

namespace MastermindGame.Tests
{
    public class MastermindTests
    {
        /// Verifies that GenerateSecretCode returns a 4-digit code and each digit is between '1' and '6'.
        [Fact]
        public void GenerateSecretCode_ProducesValidCode()
        {
            string code = Program.GenerateSecretCode();
            
            Assert.Equal(4, code.Length);
            foreach (char c in code)
            {
                Assert.InRange(c, '1', '6');
            }
        }

        /// Tests IsValidGuess with various inputs.
        [Theory]
        [InlineData("1234", true)]
        [InlineData("1562", true)]
        [InlineData("1111", true)]
        [InlineData("0000", false)]   // '0' is not allowed
        [InlineData("123", false)]    // Too short
        [InlineData("12345", false)]  // Too long
        [InlineData("12a4", false)]   // Contains a non-digit
        [InlineData("", false)]
        public void IsValidGuess_ReturnsExpectedResult(string guess, bool expected)
        {
            // Act
            bool isValid = Program.IsValidGuess(guess);
            
            // Assert
            Assert.Equal(expected, isValid);
        }

        /// Tests GetHint for a variety of secret/guess pairs.
        [Theory]
        [InlineData("1234", "1234", "++++")]   // All digits match exactly
        [InlineData("1234", "4233", "++-")]     
        [InlineData("1234", "5566", "")]        // No matches at all
        [InlineData("1122", "2211", "----")]    // All digits are correct but in the wrong positions
        [InlineData("1234", "5612", "--")]       // Two digits are correct but in the wrong positions
        public void GetHint_ReturnsCorrectHint(string secret, string guess, string expectedHint)
        {
            // Act
            string hint = Program.GetHint(secret, guess);
            
            // Assert
            Assert.Equal(expectedHint, hint);
        }

        /// Test that GetHint always returns a result with all plus signs before any minus signs.
        [Fact]
        public void GetHint_PlusSignsPrecedeMinusSigns()
        {
            string secret = "1234";
            string guess = "1325";

            string expectedHint = "+--";
            
            string hint = Program.GetHint(secret, guess);
            
            Assert.Equal(expectedHint, hint);

            int indexFirstMinus = hint.IndexOf('-');
            int indexLastPlus = hint.LastIndexOf('+');
            if (indexFirstMinus != -1 && indexLastPlus != -1)
            {
                Assert.True(indexLastPlus < indexFirstMinus, "All plus signs should precede any minus signs.");
            }
        }

        /// Test that multiple calls to GenerateSecretCode return a variety of results.
        [Fact]
        public void GenerateSecretCode_RandomnessTest()
        {
            // Arrange
            HashSet<string> codes = new HashSet<string>();
            const int iterations = 100;

            // Act
            for (int i = 0; i < iterations; i++)
            {
                codes.Add(Program.GenerateSecretCode());
            }
            
            // Very unlikely that 100 random codes would all be identical.
            Assert.True(codes.Count > 1, "Random secret codes should vary.");
        }
    }
}