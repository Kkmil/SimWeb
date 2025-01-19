using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;

namespace TestSimulator;

public class ValidatorTests
{
    [Theory]
    [InlineData(4, 0, 10, 4)]
    [InlineData(0, 0, 10, 0)]
    [InlineData(10, 0, 10, 10)]
    [InlineData(-2, 0, 5, 0)]
    [InlineData(15, 1, 2, 2)]
    [InlineData(-7, -8, 0, -7)]

    public void Limiter_ShouldReturnExpectedValue(int value, int min, int max, int expected)
    {
        // Arrange
        // Act
        // Assert
        Assert.Equal(expected, Validator.Limiter(value, min, max));
    }

    [Theory]
    [InlineData("Tomek", 3, 25, '#', "Tomek")]
    [InlineData("   Shrek    ", 3, 25, '#', "Shrek")]
    [InlineData("     ", 3, 25, '#', "###")]
    [InlineData("  donkey ", 3, 25, '#', "Donkey")]
    [InlineData("Puss in Boots – a clever and brave cat.", 3, 25, '#', "Puss in Boots – a clever")]
    [InlineData("   Cats ", 3, 15, '#', "Cats")]
    [InlineData("   Cats ", 6, 15, '@', "Cats@@")]
    [InlineData("mice           are great", 3, 15, '#', "Mice")]


    public void Shortener_ShouldReturnExpectedValue(string input, int min, int max, char placeholder, string expected)
    {
        // Arrange
        // Act
        // Assert
        Assert.Equal(expected, Validator.Shortener(input, min, max, placeholder));
    }
}
