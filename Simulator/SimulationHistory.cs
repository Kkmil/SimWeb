using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Maps;

namespace Simulator;

public class SimulationHistory
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        var simulationBaseline = new SimulationTurnLog()
        {
            Mappable = "",
            Move = "Naciśnij przycisk by zacząć",
            Symbols = new Dictionary<Point, char>(),
            SimulationMessage = "Początek symulacji"
        };
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                List<IMappable> creaturesInPoint = _simulation.Map.At(x, y);
                List<IMappable> obstaclesInPoint = _simulation.Map.ObstaclesAt(new Point(x, y));
                if (obstaclesInPoint.Count > 0)
                {
                    simulationBaseline.Symbols.Add(new Point(x, y), obstaclesInPoint[0].MapSymbol);
                }
                else if (creaturesInPoint.Count == 1)
                {
                    simulationBaseline.Symbols.Add(new Point(x, y), creaturesInPoint[0].MapSymbol);
                }
                else if (creaturesInPoint.Count > 1)
                {
                    simulationBaseline.Symbols.Add(new Point(x, y), 'X');
                }
            }
        }

        TurnLogs.Add(simulationBaseline);

        while (!_simulation.Finished)
        {
            var newTurn = new SimulationTurnLog()
            {
                Mappable = _simulation.CurrentCreature.ToString(),
                Move = _simulation.CurrentMoveName.ToString(),
                Symbols = new Dictionary<Point, char>(),
                SimulationMessage = ""
            };
            _simulation.Turn();
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeX; y++)
                {
                    List<IMappable> creaturesInPoint = _simulation.Map.At(x, y);
                    List<IMappable> obstaclesInPoint = _simulation.Map.ObstaclesAt(new Point(x, y));
                    if (obstaclesInPoint.Count > 0)
                    {
                        newTurn.Symbols.Add(new Point(x, y), obstaclesInPoint[0].MapSymbol);
                    }
                    else if (creaturesInPoint.Count == 1)
                    {
                        newTurn.Symbols.Add(new Point(x, y), creaturesInPoint[0].MapSymbol);
                    }
                    else if (creaturesInPoint.Count > 1)
                    {
                        newTurn.Symbols.Add(new Point(x, y), 'X');
                    }
                }
            }
            newTurn.SimulationMessage += _simulation.endingMessage;
            TurnLogs.Add(newTurn);
        }
    }
}
