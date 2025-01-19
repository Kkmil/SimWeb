using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Maps;
using Simulator;
using SimConsole;

namespace TestSimulator;

public class MapVIsualizerTests
{
    [Fact]
    public void Constructor_ShouldCreateMapVisualization()
    {
        // Arrange
        var Vt = new char [ '\u2502' ];
        var map = new SmallSquareMap(5);
        var e = new Elf("Elandor");
        var o = new Orc("Shrek");
        var creatures = new List<IMappable> { e, o };
        var positions = new List<Simulator.Point> { new Point(1, 2), new Point(2, 2) };
        var moves = new string("ldgrhu");
        var sim = new Simulation(map, creatures, positions, moves);

        // Act
        var vis = new MapVisualizer(sim.Map);

        // Assert
        Assert.Equal(vis.Map, map);
    }

    [Theory]
    [InlineData(1, 1, 2, 2, 3, 5, 'E', 'O')]
    [InlineData(2, 2, 2, 2, 3, 5, ' ', 'X')]

    public void Draw_ShouldCreateRowsWithExpectedData(int x1, int y1, int x2, int y2, int x3, int x4, char A, char B)
    {
        // Arrange
        var map = new SmallSquareMap(5);
        var e = new Elf("Elandor");
        var o = new Orc("Shrek");
        var creatures = new List<IMappable> { e, o };
        var positions = new List<Simulator.Point> { new Point(x1, y1), new Point(x2, y2) };
        var moves = new string("ldgrhu");
        var sim = new Simulation(map, creatures, positions, moves);

        // Act
        var vis = new MapVisualizer(sim.Map);
        vis.Draw();

        // Assert
        Assert.Equal('│', vis.dataRows[y1][0]);
        Assert.Equal(vis.Map, map);
        Assert.Equal(' ', vis.dataRows[y1][1]);
        Assert.Equal('│', vis.dataRows[y1][2]);
        Assert.Equal(A, vis.dataRows[y1][x3]);
        Assert.Equal('│', vis.dataRows[y1][4]);
        Assert.Equal(B, vis.dataRows[y2][x4]);
        Assert.Equal('│', vis.dataRows[y1][6]);
        Assert.Equal(' ', vis.dataRows[y1][7]);
        Assert.Equal('│', vis.dataRows[y1][8]);
        Assert.Equal(' ', vis.dataRows[y1][9]);
        Assert.Equal('│', vis.dataRows[y1][10]);

    }
}
