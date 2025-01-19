using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class RectangleTests
{
    [Theory]
    [InlineData( 0, 0, 10, 10)]
    [InlineData(-10, 0, 0, 16)]
    [InlineData(10, 10, 0, 5)]

    public void ConstructorForIntegers_ValidCoordinates_ShouldCreateRectangleAndAssignCoordinates(int x1, int y1, int x2, int y2)
    {
        // Arrange - implementuje warunki inaczej niż w klasie, żeby test był niezależny logicznie
        // Act
        int expX1, expY1, expX2, expY2;
        if (x1 <= x2)
        {
            expX1 = x1;
            expX2 = x2;
        }
        else
        {
            expX1 = x2;
            expX2 = x1;
        }
        if (y1 <= y2)
        {
            expY1 = y1;
            expY2 = y2;
        }
        else
        {
            expY1 = y2;
            expY2 = y1;
        }
        var rect = new Rectangle(x1, y1, x2, y2);
        // Assert
        Assert.Equal(expX1, rect.X1);
        Assert.Equal(expY1, rect.Y1);
        Assert.Equal(expX2, rect.X2);
        Assert.Equal(expY2, rect.Y2);
    }


    [Theory]
    [InlineData(1, 1, 11, 11)]
    [InlineData(0, 2, -3, 8)]
    [InlineData(6, 13, 13, 6)]
    public void ConstructorForPoints_ValidCoordinates_ShouldCreateRectangleAndAssignCoordinates(int x1, int y1, int x2, int y2)
    {
        // Arrange - implementuje warunki inaczej niż w klasie, żeby test był niezależny logicznie
        // Act
        int expX1, expY1, expX2, expY2;
        if (x1 <= x2)
        {
            expX1 = x1;
            expX2 = x2;
        }
        else
        {
            expX1 = x2;
            expX2 = x1;
        }
        if (y1 <= y2)
        {
            expY1 = y1;
            expY2 = y2;
        }
        else
        {
            expY1 = y2;
            expY2 = y1;
        }
        var p1 = new Point(expX1, expY1);
        var p2 = new Point(expX2, expY2);
        var rect = new Rectangle(p1, p2);
        // Assert
        Assert.Equal(expX1, rect.X1);
        Assert.Equal(expY1, rect.Y1);
        Assert.Equal(expX2, rect.X2);
        Assert.Equal(expY2, rect.Y2);
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(1, 3, 1, 5)]
    [InlineData(-2, -3, 5, -3)]
    [InlineData(8, 0, 5, 0)]
    public void
    Constructor_InvalidCoordinates_ShouldThrowArgumentException
    (int x1, int y1, int x2, int y2)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() =>
             new Rectangle(x1, y1, x2, y2));
    }

    [Theory]
    [InlineData(0, 0, 10, 10, 5, 5, true)]
    [InlineData(0, 0, 10, 10, -1, 5, false)]
    [InlineData(0, 0, 10, 10, 10, 10, true)]
    [InlineData(0, 0, 10, 10, 0, 10, true)]
    [InlineData(10, 0, 0, 10, 0, 10, true)]
    [InlineData(0, 0, 10, 10, 0, 11, false)]
    [InlineData(-5, -5, 5, 5, 1, 1, true)]
    [InlineData(5, 5, -5, -5, -5, 0, true)]

    public void Contains_ShouldCreateRectangleAndAssignCoordinates(int x1, int y1, int x2, int y2, int pX, int pY, bool expected)
    {
        // Arrange
        var rect = new Rectangle(x1, y1, x2, y2);
        var point = new Point(pX, pY);
        // Act & Assert
        Assert.Equal(expected, rect.Contains(point));
    }


    [Theory]
    [InlineData("(0, 0):(10, 10)", 0, 0, 10, 10)]
    [InlineData("(-10, 0):(0, 16)", -10, 0, 0, 16)]
    [InlineData("(0, 5):(10, 10)", 10, 10, 0, 5)]

    public void ToString_ShouldDisplayExpectedText(string text, int x1, int y1, int x2, int y2)
    {
        // Arrange
        // Act
        var rect = new Rectangle(x1, y1, x2, y2); 
        // Assert
        Assert.Equal(text, rect.ToString());
    }
}
