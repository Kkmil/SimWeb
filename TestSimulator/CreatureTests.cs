using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class CreatureTests
{

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
        var a = new Elf("Elandor");
        var point = new Simulator.Point(x, y);
        // Act
        a.InitMapAndPosition(map, point);
        // Assert
        Assert.Equal(a.Map, map);
        Assert.Equal(a.Position, point);
        Assert.Equal($"The creatures in point {point} are as follows: Elandor", map.StringListAt(point.X, point.Y));
    }


    [Fact]
    public void RemoveFromMap_ShouldClearMapForCreature()
    {
        // Arrange
        var a = new Elf("Elandor");
        var map = new SmallSquareMap(5);
        a.InitMapAndPosition(map, new Simulator.Point (1, 1));
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
        var a = new Elf("Elandor");
        var map = new SmallSquareMap(5);
        var startingPosition = new Simulator.Point (startingX, startingY);
        var endPosition = new Simulator.Point(expectedX, expectedY);
        a.InitMapAndPosition(map, startingPosition);
        // Act
        a.Go(direction);
        // Assert
        Assert.Equal(endPosition,a.Position);
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
        var a = new Elf("Elandor");
        var map = new SmallTorusMap(5,5);
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
        var a = new Elf("Elandor",7,8);
        // Act
        // Assert
        Assert.Equal("ELF: Elandor [7][8]",a.ToString());
    }
}
