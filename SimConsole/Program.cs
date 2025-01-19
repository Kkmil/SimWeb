using System.Text;
using Simulator;
using Simulator.Maps;

namespace SimConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // Wygląda na niepotrzebne?
        Console.WriteLine("SIMULATION!\n");


        //   MAPA SQUARE 5x5 
        //SmallSquareMap map = new(5);
        //List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor")];
        //List<Simulator.Point> points = [new(2, 2), new(3, 1)];
        //string moves = "dlrludl";

        //Simulation simulation = new(map, creatures, points, moves);
        //MapVisualizer mapVisualizer = new(simulation.Map);


        ////   MAPA TORUS 8x6 -> stwory i ruchy jak w projekie DEMO RAZOR PAGES
        //SmallTorusMap map = new(8, 6);
        //List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor"),
        //    new Animals() { Description = "Rabbits" , Size = 40}, new Birds() { Description = "Eagles"}, new Birds() {Description = "Ostriches", Size = 15, CanFly = false}];
        //List<Simulator.Point> points = [new(2, 2), new(3, 1), new(5, 5), new(7, 3), new(0, 4)];
        //string moves = "dlrludluddlrulr";


        //SmallTorusMap map = new(8, 6);
        //List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor"), new Animals() { Description = "Rabbits" }];
        //List<Simulator.Point> points = [new Simulator.Point(2, 1), new Simulator.Point(2, 3), new Simulator.Point(4, 4)];
        //string moves = "rdlrrruuuurrr";

        //List<IMappable> staticObstacles = [new StaticObstacle("Mountain", NaturalElement.Earth), new StaticObstacle("River", NaturalElement.Water),
        //    new StaticObstacle("River", NaturalElement.Water), new StaticObstacle("River", NaturalElement.Water),new StaticObstacle("Mist", NaturalElement.Air)];
        //List<Simulator.Point> obstaclePoints = [new(2, 2), new(3, 5), new(3, 4), new(3, 3), new(5, 4)];

        SmallTorusMap map = new(8, 6);
        List<IMappable> creatures = [new Orc("Gorbag")];
        List<Simulator.Point> points = [new Simulator.Point(2, 1)];
        string moves = "uuuuuuu";

        List<IMappable> staticObstacles = [/*new StaticObstacle("Mist", NaturalElement.Air)*/];
        List<Simulator.Point> obstaclePoints = [/*new(2, 2)*/];

        List<Item> items = Enumerable.Repeat((Item)new Coin(), 10).ToList();
        List<Point> itemPositions = [new(2, 2), new(3, 4), new(1, 0), new(7, 5), new(0, 3), new(5, 1), new(2, 4), new(4, 0), new(6, 3), new(1, 5)];

        Simulation simulation = new(map, creatures, points, moves, staticObstacles, obstaclePoints, items, itemPositions);

        //MapVisualizer mapVisualizer = new(simulation.Map);

        //Console.WriteLine("Created Creatures in map:");
        //foreach (IMappable mappable in creatures)
        //{
        //    Console.WriteLine(mappable.ToString());
        //}
        //Console.WriteLine("\nStarting Positions:");

        //mapVisualizer.Draw();
        
        //while (!simulation.Finished)
        //{
        //    Console.WriteLine("Press any key to continue...");
        //    Console.ReadKey();
        //    Console.WriteLine($"\nTurn {simulation.CurrentTurnNumber}:");
        //    Console.WriteLine($"{simulation.CurrentCreature} goes {simulation.CurrentMoveName}:");
        //    simulation.Turn();
        //    mapVisualizer.Draw();
        //}
        //Console.WriteLine("End of simulation!");

        Console.WriteLine("\nSIMULATION LOGBOOK:");
        var logbook = new SimulationHistory(simulation);
        foreach (SimulationTurnLog turn in logbook.TurnLogs)
        {
            Console.WriteLine($"\nTurn # {logbook.TurnLogs.IndexOf(turn)}:");
            Console.WriteLine($"Creature: {turn.Mappable}");
            Console.WriteLine($"Move: {turn.Move}");
            foreach (var symbol in turn.Symbols)
                Console.WriteLine($"{symbol.Key} {symbol.Value}");
        }
        Console.WriteLine();
        Console.WriteLine("\nEND OF LOGBOOK.");

        Console.WriteLine($"\nWhich Turn you would like to see? \nProvide a number between 0 and {logbook.TurnLogs.Count - 1}:");
        int selectedTurn = int.Parse(Console.ReadLine());

        Console.WriteLine($"\nTurn # {selectedTurn}:");
        Console.WriteLine($"Creature: {logbook.TurnLogs[selectedTurn].Mappable}");
        Console.WriteLine($"Move: {logbook.TurnLogs[selectedTurn].Move}");
        foreach (var symbol in logbook.TurnLogs[selectedTurn].Symbols)
            Console.WriteLine($"{symbol.Key} {symbol.Value}");

    }
}
