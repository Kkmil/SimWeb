using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class BirdsTests
{
    [Theory]
    [InlineData("Hens", 7, false, 'b')]
    [InlineData("Eagles", 44, true,'B')]
    [InlineData("Eagles", 33, false, 'b')]

    public void Constructor_ShouldCreateCorrectBirds(string name, uint size, bool fly, char C)
    {
        // Arrange & Act
        var a = new Birds() { Description = name, Size = size, CanFly = fly };
        // Assert
        Assert.Null(a.Map);
        Assert.Equal(C, a.MapSymbol);
        Assert.Equal(a.Size, size);
        Assert.Equal(a.Name, name);
        Assert.Equal(a.Description, name);
    }

    [Theory]
    [InlineData(Direction.Up, 2, 2, 2, 4)]
    [InlineData(Direction.Right, 2, 2, 4, 2)]
    [InlineData(Direction.Down, 2, 2, 2, 0)]
    [InlineData(Direction.Left, 2, 2, 0, 2)]
    [InlineData(Direction.Up, 4, 4, 4, 4)]
    [InlineData(Direction.Right, 4, 1, 4, 1)]
    [InlineData(Direction.Down, 2, 0, 2, 0)]
    [InlineData(Direction.Left, 0, 2, 0, 2)]
    [InlineData(Direction.Up, 4, 3, 4, 4)]
    [InlineData(Direction.Right, 3, 1, 4, 1)]
    [InlineData(Direction.Down, 2, 1, 2, 0)]
    [InlineData(Direction.Left, 1, 2, 0, 2)]

    public void
    Go_FlyingBirdsShouldGoCorrectlyInSmallSquareMap
    (Direction direction, int startingX, int startingY, int expectedX, int expectedY)
    {
        // Arrange
        var a = new Birds() { Description = "Eagles" , CanFly = true};
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
    [InlineData(Direction.Up, 2, 2, 3, 3)]
    [InlineData(Direction.Right, 2, 2, 3, 1)]
    [InlineData(Direction.Down, 2, 2, 1, 1)]
    [InlineData(Direction.Left, 2, 2, 1, 3)]
    [InlineData(Direction.Up, 3, 4, 3, 4)]
    [InlineData(Direction.Right, 4, 1, 4, 1)]
    [InlineData(Direction.Down, 0, 1, 0, 1)]
    [InlineData(Direction.Left, 0, 2, 0, 2)]


    public void
    Go_NonFlyingBirdsShouldGoCorrectlyInSmallSquareMap
    (Direction direction, int startingX, int startingY, int expectedX, int expectedY)
    {
        // Arrange
        var a = new Birds() { Description = "Hens", CanFly = false };
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
    [InlineData(Direction.Up, 2, 2, 2, 4)]
    [InlineData(Direction.Right, 2, 2, 4, 2)]
    [InlineData(Direction.Down, 2, 2, 2, 0)]
    [InlineData(Direction.Left, 2, 2, 0, 2)]
    [InlineData(Direction.Up, 4, 4, 4, 1)]
    [InlineData(Direction.Right, 4, 1, 1, 1)]
    [InlineData(Direction.Down, 2, 0, 2, 3)]
    [InlineData(Direction.Left, 0, 2, 3, 2)]
    [InlineData(Direction.Up, 4, 3, 4, 0)]
    [InlineData(Direction.Right, 3, 1, 0, 1)]
    [InlineData(Direction.Down, 2, 1, 2, 4)]
    [InlineData(Direction.Left, 1, 2, 4, 2)]

    public void
    Go_FlyingBirdsShouldGoCorrectlyInSmallTorusMap
    (Direction direction, int startingX, int startingY, int expectedX, int expectedY)
    {
        // Arrange
        var a = new Birds() { Description = "Eagles", CanFly = true };
        var map = new SmallTorusMap(5,5);
        var startingPosition = new Simulator.Point(startingX, startingY);
        var endPosition = new Simulator.Point(expectedX, expectedY);
        a.InitMapAndPosition(map, startingPosition);
        // Act
        a.Go(direction);
        // Assert
        Assert.Equal(endPosition, a.Position);
    }

    [Theory]
    [InlineData(Direction.Up, 2, 2, 3, 3)]
    [InlineData(Direction.Right, 2, 2, 3, 1)]
    [InlineData(Direction.Down, 2, 2, 1, 1)]
    [InlineData(Direction.Left, 2, 2, 1, 3)]
    [InlineData(Direction.Up, 3, 4, 4, 0)]
    [InlineData(Direction.Right, 4, 1, 0, 0)]
    [InlineData(Direction.Down, 0, 1, 4, 0)]
    [InlineData(Direction.Left, 0, 2, 4, 3)]


    public void
    Go_NonFlyingBirdsShouldGoCorrectlyInSmallTorusMap
    (Direction direction, int startingX, int startingY, int expectedX, int expectedY)
    {
        // Arrange
        var a = new Birds() { Description = "Hens", CanFly = false };
        var map = new SmallTorusMap(5,5);
        var startingPosition = new Simulator.Point(startingX, startingY);
        var endPosition = new Simulator.Point(expectedX, expectedY);
        a.InitMapAndPosition(map, startingPosition);
        // Act
        a.Go(direction);
        // Assert
        Assert.Equal(endPosition, a.Position);
    }
}
