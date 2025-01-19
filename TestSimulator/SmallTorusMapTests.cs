using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallTorusMapTests
{
    [Fact]
    public void Constructor_ValidSize_ShouldSetSize()
    {
        // Arrange
        int sizeX = 10;
        int sizeY = 9;
        // Act
        var map = new SmallTorusMap(sizeX, sizeY);
        // Assert
        Assert.Equal(sizeX, map.SizeX);
        Assert.Equal(sizeY, map.SizeY);
    }

    [Theory]
    [InlineData(4,4)]
    [InlineData(21,20)]
    public void
        Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException
        (int sizeX, int sizeY)
    {
        // Act & Assert
        // The way to check if method throws anticipated exception:
        Assert.Throws<ArgumentOutOfRangeException>(() =>
             new SmallTorusMap(sizeX, sizeY));
    }

    [Theory]
    [InlineData(3, 4, 5, 5, true)]
    [InlineData(6, 1, 5, 5, false)]
    [InlineData(19, 19, 20, 20, true)]
    [InlineData(20, 20, 20, 20, false)]
    [InlineData(0, 9, 5, 10, true)]
    [InlineData(4, 10, 5, 10, false)]
    [InlineData(4, 3, 5, 10, true)]
    [InlineData(5, 3, 5, 10, false)]
    [InlineData(-2, 3, 5, 10, false)]
    [InlineData(4, -1, 5, 10, false)]
    public void Exist_ShouldReturnCorrectValue(int x, int y,
        int sizeX, int sizeY, bool expected)
    {
        // Arrange
        var map = new SmallTorusMap(sizeX, sizeY);
        var point = new Point(x, y);
        // Act
        var result = map.Exist(point);
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 5, 11)]
    [InlineData(0, 0, Direction.Down, 0, 19)]
    [InlineData(0, 8, Direction.Left, 19, 8)]
    [InlineData(19, 10, Direction.Right, 0, 10)]
    public void Next_ShouldReturnCorrectNextPoint(int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallTorusMap(20,20);
        var point = new Point(x, y);
        // Act
        var nextPoint = map.Next(point, direction);
        // Assert
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 6, 11)]
    [InlineData(0, 0, Direction.Down, 19, 19)]
    [InlineData(0, 8, Direction.Left, 19, 9)]
    [InlineData(19, 10, Direction.Right, 0, 9)]
    public void NextDiagonal_ShouldReturnCorrectNextPoint(int x, int y,
        Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var map = new SmallTorusMap(20,20);
        var point = new Point(x, y);
        // Act
        var nextPoint = map.NextDiagonal(point, direction);
        // Assert
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }


    [Theory]
    [InlineData(5, 5, 5, 3, Direction.Up)]
    [InlineData(20, 20, 20, 0, Direction.Up)]
    [InlineData(5, 5, 5, 0, Direction.Up)]
    [InlineData(5, 5, -1, 0, Direction.Up)]
    [InlineData(5, 5, 2, 5, Direction.Up)]
    [InlineData(5, 5, 4, 5, Direction.Left)]
    [InlineData(20, 10, 20, 0, Direction.Down)]
    [InlineData(5, 10, 5, 0, Direction.Right)]
    [InlineData(5, 6, -1, 0, Direction.Down)]
    [InlineData(6, 5, 2, 5, Direction.Left)]
    public void
    Next_ShouldThrowArgumentOutOfRangeExceptionIfGivenPointIsOutsideTheMap
    (int sizeX, int sizeY, int x, int y, Direction direction)
    {
        // Act & Assert
        var map = new SmallTorusMap(sizeX,sizeY);
        var point = new Point(x, y);

        // The way to check if method throws anticipated exception:
        Assert.Throws<ArgumentOutOfRangeException>(() =>
             map.Next(point, direction));
    }

    [Theory]
    [InlineData(5, 5, 5, 3, Direction.Up)]
    [InlineData(20, 20, 20, 0, Direction.Up)]
    [InlineData(5, 5, 5, 0, Direction.Up)]
    [InlineData(5, 5, -1, 0, Direction.Up)]
    [InlineData(5, 5, 2, 5, Direction.Up)]
    [InlineData(5, 5, 4, 5, Direction.Left)]
    [InlineData(20, 10, 20, 0, Direction.Down)]
    [InlineData(5, 10, 5, 0, Direction.Right)]
    [InlineData(5, 6, -1, 0, Direction.Down)]
    [InlineData(5, 5, 2, 5, Direction.Left)]
    public void
    NextDiagonal_ShouldThrowArgumentOutOfRangeExceptionIfGivenPointIsOutsideTheMap
    (int sizeX, int sizeY, int x, int y, Direction direction)
    {
        // Act & Assert
        var map = new SmallTorusMap(sizeX, sizeY);
        var point = new Point(x, y);

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
        var map = new SmallTorusMap(size, size);
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
        var map = new SmallTorusMap(size, size);
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
        var map = new SmallTorusMap(size,size);
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
        var map = new SmallTorusMap(size,size);
        var point = new Simulator.Point(x, y);
        map.Add(creature, point);
        // Act
        map.Remove(creature, point);
        // Assert
        Assert.Null(creature.Map);
        Assert.Equal($"There is no creature in the indicated point {point}.", map.StringListAt(point.X, point.Y));
    }
}
