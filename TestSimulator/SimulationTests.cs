using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SimulationTests
{
    [Fact]
    public void Constructor_ShouldCreateSimulation()
    {
        // Arrange
        var map = new SmallSquareMap(5);
        var e = new Elf("Elandor");
        var creatures = new List<IMappable> { e, new Orc("Shrek") };
        var point = new Point(1, 1);
        var positions = new List<Simulator.Point> { point, new Point(2, 2) };
        var moves = new string("ldgrhu");
        // Act
        var sim = new Simulation(map, creatures, positions, moves);
        // Assert
        Assert.Equal(sim.Map, map);
        Assert.Equal(sim.Creatures, creatures);
        Assert.Equal(sim.Creatures[0].Position, point);
        Assert.Equal(sim.Moves, moves);
        Assert.False(sim.Finished);
        Assert.Equal(sim.CurrentCreature, e);
        Assert.Equal("left", sim.CurrentMoveName);
    }

    [Fact]
    public void Constructor_ShouldThrowExceptionWhenCreatureListIsEmpty()
    {
        // Arrange
        var map = new SmallSquareMap(5);
        var e = new Elf("Elandor");
        var creatures = new List<IMappable> { };
        var point = new Point(1, 1);
        var positions = new List<Simulator.Point> { point, new Point(2, 2) };
        var moves = new string("ldgrhu");
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() =>
        new Simulation(map, creatures, positions, moves));
    }

    [Fact]
    public void Constructor_ShouldThrowExceptionWhenCreatureListAndPositionListHaveDifferentLengths()
    {
        // Arrange
        var map = new SmallSquareMap(5);
        var e = new Elf("Elandor");
        var creatures = new List<IMappable> { e };
        var point = new Point(1, 1);
        var positions = new List<Simulator.Point> { point, new Point(2, 2) };
        var moves = new string("ldgrhu");
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() =>
        new Simulation(map, creatures, positions, moves));
    }

    [Fact]
    public void Turn_ShouldEnableOneTurnOfSimulationAndMoveCreature()
    {
        // Arrange
        var map = new SmallSquareMap(5);
        var e = new Elf("Elandor");
        var o = new Orc("Shrek");
        var creatures = new List<IMappable> { e, o };
        var point = new Point(1, 1);
        var positions = new List<Simulator.Point> { point, new Point(2, 2) };
        var moves = new string("ldgrhu");
        var sim = new Simulation(map, creatures, positions, moves);
        var endTurnPoint = new Simulator.Point(0, 1);
        // Act
        sim.Turn();
        // Assert
        Assert.Equal(sim.Map, map);
        Assert.Equal(sim.Creatures, creatures);
        Assert.Equal(sim.Creatures[0].Position, endTurnPoint);
        Assert.Equal(sim.Creatures[1].Position, new Point(2, 2));
        Assert.Equal(sim.Moves, moves);
        Assert.False(sim.Finished);
        Assert.Equal(sim.CurrentCreature, o);
        Assert.Equal("down", sim.CurrentMoveName);

    }

    [Fact]
    public void Turn_ShouldModifyFinishedFlag()
    {
        // Arrange
        var map = new SmallSquareMap(5);
        var e = new Elf("Elandor");
        //var o = new Orc("Shrek");
        var creatures = new List<IMappable> { e};
        var point = new Point(1, 1);
        var positions = new List<Simulator.Point> { point };
        var moves = new string("d");
        var sim = new Simulation(map, creatures, positions, moves);
        // Act
        sim.Turn();
        // Assert
        Assert.True(sim.Finished);
    }
}
