using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class PointTests
{
    [Fact]
    public void Constructor_ShouldCreatePointAndSetCoordinates()
    {
        // Arrange
        int x = 10;
        int y = 20;
        // Act
        var p = new Point(x,y);
        // Assert
        Assert.Equal(10, p.X);
        Assert.Equal(20, p.Y);
    }

    [Theory]
    [InlineData("(0, 0)", 0, 0)]
    [InlineData("(-10, 0)", -10, 0)]
    [InlineData("(24, -67)", 24, -67)]

    public void ToString_ShouldDisplayExpectedText(string text, int x, int y)
    {
        // Arrange
        // Act
        var p = new Point(x,y);
        // Assert
        Assert.Equal(text, p.ToString());
    }

    [Theory]
    [InlineData(0, 0, Direction.Right, 1, 0)]
    [InlineData(5, 5, Direction.Up, 5, 6)]
    [InlineData(-4, 18, Direction.Left, -5, 18)]
    [InlineData(0, -1, Direction.Down, 0, -2)]

    public void Next_ShouldReturnCorrectNextPoint(int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var p1 = new Point(x,y);
        var p2 = new Point(expectedX,expectedY);
        // Act
        var movedP1 = p1.Next(direction);
        // Assert
        Assert.Equal(p2 , movedP1);
    }

    [Theory]
    [InlineData(0, 0, Direction.Right, 1, -1)]
    [InlineData(5, 5, Direction.Up, 6, 6)]
    [InlineData(-4, 18, Direction.Left, -5, 19)]
    [InlineData(0, -1, Direction.Down, -1, -2)]

    public void NextDiagonal_ShouldReturnCorrectNextPoint(int x, int y,
    Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var p1 = new Point(x, y);
        var p2 = new Point(expectedX, expectedY);
        // Act
        var movedP1 = p1.NextDiagonal(direction);
        // Assert
        Assert.Equal(p2, movedP1);
    }
}
