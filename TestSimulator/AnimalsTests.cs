using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Maps;
using Simulator;

namespace TestSimulator;

public class AnimalsTests
{

    [Theory]
    [InlineData("Eagles", 7)]
    [InlineData("Dogs", 9)]
    [InlineData("Something", 33)]
    [InlineData("Cats", 145)]
    [InlineData("Yaks", 1000)]
    public void Constructor_ShouldCreateCorrectAnimals(string name, uint size)
    {
        // Arrange & Act
        var a = new Animals() { Description = name, Size = size };
        // Assert
        Assert.Null(a.Map);
        Assert.Equal('A', a.MapSymbol);
        Assert.Equal(a.Size, size);
        Assert.Equal(a.Name, name);
        Assert.Equal(a.Description, name);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(4, 4)]
    [InlineData(0, 0)]
    [InlineData(4, 0)]
    [InlineData(0, 4)]
    public void InitMapAndPosition_ShouldAssignCreatureToMap(int x, int y)
    {
        // Arrange
        var map = new SmallSquareMap(5);
        var a = new Animals() { Description = "Rabbits" };
        var point = new Simulator.Point(x, y);
        // Act
        a.InitMapAndPosition(map, point);
        // Assert
        Assert.Equal(a.Map, map);
        Assert.Equal(a.Position, point);
        Assert.Equal('A', a.MapSymbol);
        Assert.Equal($"The creatures in point {point} are as follows: Rabbits", map.StringListAt(point.X, point.Y));
    }


    [Fact]
    public void RemoveFromMap_ShouldClearMapForCreature()
    {
        // Arrange
        var a = new Animals() { Description = "Eagles"};
        var map = new SmallSquareMap(5);
        a.InitMapAndPosition(map, new Simulator.Point(1, 1));
        // Act
        a.RemoveFromMap();
        // Assert
        Assert.Null(a.Map);
    }

    [Theory]
    [InlineData(Direction.Up, 2, 2, 2, 3)]
    [InlineData(Direction.Right, 2, 2, 3, 2)]
    [InlineData(Direction.Down, 2, 2, 2, 1)]
    [InlineData(Direction.Left, 2, 2, 1, 2)]
    [InlineData(Direction.Up, 4, 4, 4, 4)]
    [InlineData(Direction.Right, 4, 1, 4, 1)]
    [InlineData(Direction.Down, 2, 0, 2, 0)]
    [InlineData(Direction.Left, 0, 2, 0, 2)]

    public void
    Go_CreatureShouldGoCorrectlyInSmallSquareMap
    (Direction direction, int startingX, int startingY, int expectedX, int expectedY)
    {
        // Arrange
        var a = new Animals() { Description = "Dogs" };
        var map = new SmallSquareMap(5);
        var startingPosition = new Simulator.Point(startingX, startingY);
        var endPosition = new Simulator.Point(expectedX, expectedY);
        a.InitMapAndPosition(map, startingPosition);
        // Act
        a.Go(direction);
        // Assert
        Assert.Equal(endPosition, a.Position);
    }

    [Theory]
    [InlineData(Direction.Up, 2, 2, 2, 3)]
    [InlineData(Direction.Right, 2, 2, 3, 2)]
    [InlineData(Direction.Down, 2, 2, 2, 1)]
    [InlineData(Direction.Left, 2, 2, 1, 2)]
    [InlineData(Direction.Up, 4, 4, 4, 0)]
    [InlineData(Direction.Right, 4, 1, 0, 1)]
    [InlineData(Direction.Down, 2, 0, 2, 4)]
    [InlineData(Direction.Left, 0, 2, 4, 2)]

    public void
    Go_CreatureShouldGoCorrectlyInSmallTorusMap
    (Direction direction, int startingX, int startingY, int expectedX, int expectedY)
    {
        // Arrange
        var a = new Animals() { Description = "Cats" };
        var map = new SmallTorusMap(5, 5);
        var startingPosition = new Simulator.Point(startingX, startingY);
        var endPosition = new Simulator.Point(expectedX, expectedY);
        a.InitMapAndPosition(map, startingPosition);
        // Act
        a.Go(direction);
        // Assert
        Assert.Equal(endPosition, a.Position);
    }

    [Fact]
    public void ToString_ShouldReturnExpectedString()
    {
        // Arrange
        var a = new Animals() { Description = "Eagles", Size = 27};
        // Act
        // Assert
        Assert.Equal("ANIMALS: Eagles <27>", a.ToString());
    }

    [Fact]
    public void ToString_ShouldReturnDefaultSize()
    {
        // Arrange
        var a = new Animals() { Description = "Eagles"};
        // Act
        // Assert
        Assert.Equal("ANIMALS: Eagles <3>", a.ToString());
    }
}
