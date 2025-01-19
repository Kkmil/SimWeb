using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallSquareMapTests
{
    [Fact]
    public void Constructor_ValidSize_ShouldSetSize()
    {
        // Arrange
        int size = 10;
        // Act
        var map = new SmallSquareMap(size);
        // Assert
        Assert.Equal(size, map.Size);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(20)]
    public void Constructor_ValidSize_ShouldSetMaxSize(int size)
    {
        // Arrange
        // Act
        var map = new SmallSquareMap(size);
        // Assert
        Assert.Equal(size, map.Size);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(21)]
    public void
        Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException
        (int size)
    {
        // Act & Assert
        // The way to check if method throws anticipated exception:
        Assert.Throws<ArgumentOutOfRangeException>(() =>
             new SmallSquareMap(size));
    }

    [Theory]
    [InlineData(-1, 0, 5, false)]
    [InlineData(0, 0, 5, true)]
    [InlineData(3, 4, 5, true)]
    [InlineData(6, 1, 5, false)]
    [InlineData(19, 19, 20, true)]
    [InlineData(20, 20, 20, false)]
    [InlineData(0, 7, 8, true)]
    public void Exist_ShouldReturnCorrectValue(int x, int y,
        int size, bool expected)
    {
        // Arrange
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        var result = map.Exist(point);
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(20, 5, 10, Direction.Up, 5, 11)]
    [InlineData(8, 7, 7, Direction.Up, 7, 7)]
    [InlineData(20, 0, 0, Direction.Down, 0, 0)]
    [InlineData(7, 6, 5, Direction.Down, 6, 4)]
    [InlineData(20, 0, 8, Direction.Left, 0, 8)]
    [InlineData(20, 19, 10, Direction.Right, 19, 10)]
    [InlineData(20, 18, 10, Direction.Right, 19, 10)]
    public void Next_ShouldReturnCorrectNextPoint(int size, int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        var nextPoint = map.Next(point, direction);
        // Assert
        Assert.Equal(new Simulator.Point(expectedX, expectedY), nextPoint);
    }

    [Theory]
    [InlineData(20, 5, 10, Direction.Up, 6, 11)]
    [InlineData(8, 6, 7, Direction.Up, 6, 7)]
    [InlineData(20, 0, 0, Direction.Down, 0, 0)]
    [InlineData(20, 1, 0, Direction.Down, 1, 0)]
    [InlineData(10, 5, 5, Direction.Down, 4, 4)]
    [InlineData(20, 0, 8, Direction.Left, 0, 8)]
    [InlineData(10, 5, 7, Direction.Left, 4, 8)]
    [InlineData(20, 19, 10, Direction.Right, 19, 10)]
    [InlineData(15, 10, 10, Direction.Right, 11, 9)]
    public void NextDiagonal_ShouldReturnCorrectNextPoint(int size, int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        var nextPoint = map.NextDiagonal(point, direction);
        // Assert
        Assert.Equal(new Simulator.Point(expectedX, expectedY), nextPoint);
    }

    [Theory]
    [InlineData(5, 5, 3, Direction.Up)]
    [InlineData(20, 20, 0, Direction.Up)]
    [InlineData(5, 5, 0, Direction.Up)]
    [InlineData(5, -1, 0, Direction.Up)]
    [InlineData(5, 2, 5, Direction.Up)]
    [InlineData(5, 5, 3, Direction.Left)]
    [InlineData(20, 20, 0, Direction.Down)]
    [InlineData(5, 5, 0, Direction.Right)]
    [InlineData(5, -1, 0, Direction.Down)]
    [InlineData(5, 2, 5, Direction.Left)]
    public void
    Next_ShouldThrowArgumentOutOfRangeExceptionIfGivenPointIsOutsideTheMap
    (int size, int x, int y, Direction direction)
    {
        // Act & Assert
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);

        // The way to check if method throws anticipated exception:
        Assert.Throws<ArgumentOutOfRangeException>(() =>
             map.Next(point, direction));
    }

    [Theory]
    [InlineData(5, 5, 3, Direction.Up)]
    [InlineData(20, 20, 0, Direction.Up)]
    [InlineData(5, 5, 0, Direction.Up)]
    [InlineData(5, -1, 0, Direction.Up)]
    [InlineData(5, 2, 5, Direction.Up)]
    [InlineData(5, 5, 3, Direction.Left)]
    [InlineData(20, 20, 0, Direction.Down)]
    [InlineData(5, 5, 0, Direction.Right)]
    [InlineData(5, -1, 0, Direction.Down)]
    [InlineData(5, 2, 5, Direction.Left)]
    public void
    NextDiagonal_ShouldThrowArgumentOutOfRangeExceptionIfGivenPointIsOutsideTheMap
    (int size, int x, int y, Direction direction)
    {
        // Act & Assert
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);

        // The way to check if method throws anticipated exception:
        Assert.Throws<ArgumentOutOfRangeException>(() =>
             map.NextDiagonal(point, direction));
    }

    [Theory]
    [InlineData(5, 2, 2)]
    [InlineData(20, 7, 7)]
    public void
    Add_ShouldInitiateCreatureInTheMapAndAssignCreatureToMap
    (int size, int x, int y)
    {
        // Arrange
        var creature = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        map.Add(creature, point);
        // Assert
        Assert.Equal(creature.Map, map);
        Assert.Equal(creature.Position, point);
        Assert.Equal($"The creatures in point {point} are as follows: Elandor", map.StringListAt(point.X, point.Y));
    }

    [Theory]
    [InlineData(5, 2, 2)]
    [InlineData(20, 7, 7)]
    public void
    Add_ShouldBeAbleToAddMoreThanOneCreatureInTheSamePoint
    (int size, int x, int y)
    {
        // Arrange
        var creature1 = new Elf("Elandor");
        var creature2 = new Orc("Ictorn");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        map.Add(creature1, point);
        map.Add(creature2, point);
        // Assert
        Assert.Equal(creature1.Map, map);
        Assert.Equal(creature1.Position, point);
        Assert.Equal(creature2.Map, map);
        Assert.Equal(creature2.Position, point);
        Assert.Equal($"The creatures in point {point} are as follows: Elandor, Ictorn", map.StringListAt(point.X, point.Y));
    }

    [Theory]
    [InlineData(5, 5, 5)]
    [InlineData(20, 20, 10)]
    [InlineData(7, -2, 1)]
    [InlineData(5, 3, 5)]
    public void
    Add_CannotAddCreatureOutsideOftheMap
    (int size, int x, int y)
    {
        // Arrange
        var creature1 = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() =>
             map.Add(creature1, point));
    }

    [Theory]
    [InlineData(5, 2, 2)]
    [InlineData(20, 7, 7)]
    public void
    Remove_ShouldRemoveCreaturesFromMap
    (int size, int x, int y)
    {
        // Arrange
        var creature = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        map.Add(creature, point);
        // Act
        map.Remove(creature, point);
        // Assert
        Assert.Null(creature.Map);
        Assert.Equal($"There is no creature in the indicated point {point}.", map.StringListAt(point.X, point.Y));
    }

    [Theory]
    [InlineData(5, 2, 2, 2, 3)]
    [InlineData(20, 7, 7, 10, 10)]
    public void
    Move_ShouldMoveCreatureAsExpectedInMap
    (int size, int x, int y, int x2, int y2)
    {
        // Arrange
        var creature = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var startingPoint = new Simulator.Point(x, y);
        var endPoint = new Simulator.Point(x2, y2);
        map.Add(creature, startingPoint);
        // Act
        map.Move(creature, startingPoint, endPoint);
        // Assert
        Assert.Equal($"There is no creature in the indicated point {startingPoint}.", map.StringListAt(startingPoint.X, startingPoint.Y));
        Assert.Equal($"The creatures in point {endPoint} are as follows: Elandor", map.StringListAt(endPoint.X, endPoint.Y));
        Assert.Equal(endPoint, creature.Position);
        Assert.Equal(map, creature.Map);
    }

    [Theory]
    [InlineData(5, 2, 2, 5, 5)]
    [InlineData(20, 7, 7, 7, 20)]
    [InlineData(7, 0, 0, -1, 3)]
    public void
    AtPoint_ShouldReturnExpectedStringWhenPointIsNotInTheMap
    (int size, int x, int y, int x2, int y2)
    {
        // Arrange
        var creature = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        var point2 = new Simulator.Point(x2, y2);
        map.Add(creature, point);
        // Act
        // Assert
        Assert.Equal($"Point {point2} does not belong to the map.", map.StringListAt(point2.X, point2.Y));
    }

    [Theory]
    [InlineData(5, 2, 2, 2, 1)]
    [InlineData(20, 7, 7, 0, 0)]
    [InlineData(7, 0, 0, 1, 0)]
    public void
    AtPoint_ShouldReturnExpectedStringWhenThereAIsNoCreatureInPoint
    (int size, int x, int y, int x2, int y2)
    {
        // Arrange
        var creature = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        var point2 = new Simulator.Point(x2, y2);
        map.Add(creature, point);
        // Act
        // Assert
        Assert.Equal($"There is no creature in the indicated point {point2}.", map.StringListAt(point2.X, point2.Y));
    }

    [Theory]
    [InlineData(5, 2, 2)]
    [InlineData(20, 7, 7)]
    public void
    AtPoint_ShouldReturnExpectedStringWhenThereAreCreaturesInPoint
    (int size, int x, int y)
    {
        // Arrange
        var creature = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        map.Add(creature, point);
        // Assert
        Assert.Equal($"The creatures in point {point} are as follows: Elandor", map.StringListAt(point.X, point.Y));
    }

    [Theory]
    [InlineData(5, 2, 2)]
    [InlineData(20, 7, 7)]
    public void
    AtCoordinates_ShouldReturnExpectedStringWhenThereAreCreaturesInPoint
    (int size, int x, int y)
    {
        // Arrange
        var creature = new Elf("Elandor");
        var map = new SmallSquareMap(size);
        var point = new Simulator.Point(x, y);
        // Act
        map.Add(creature, point);
        // Assert
        Assert.Equal($"The creatures in point {point} are as follows: Elandor", map.StringListAt(point.X, point.Y));
    }
}
