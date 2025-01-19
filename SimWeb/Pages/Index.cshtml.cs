using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;


namespace SimWeb.Pages;

public class IndexModel : PageModel
{
    //private readonly ILogger<IndexModel> _logger;

    public Simulation Simulation { get; }

    public SimulationHistory History { get; }

    public string CreaturesInMap { get; } = "";
    private string GenerateRandomMoves(int length)
    {
        char[] moves = { 'r', 'd', 'l', 'u' };
        Random random = new Random();
        char[] randomMoves = new char[length];

        for (int i = 0; i < length; i++)
        {
            randomMoves[i] = moves[random.Next(moves.Length)];
        }

        return new string(randomMoves);
    }

    public IndexModel()
    {
        //Map map = new SmallTorusMap(8, 6);
        //List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor"), new Animals() { Description = "Rabbits" , Size = 40},
        //        new Birds() { Description = "Eagles"}, new Birds() {Description = "Ostriches", Size = 15, CanFly = false}];
        //List<Simulator.Point> creaturePoints = [new(2, 2), new(3, 1), new(5, 5), new(7, 3), new(0, 4)];
        //string simulationMoves = "dlrludluddlrulr";
        //List<IMappable> staticObstacles = [new StaticObstacle("Mountain", NaturalElement.Earth)];
        //List<Simulator.Point> obstaclePoints = [new(0, 0)];

        //Simulation = new Simulation(map, creatures, creaturePoints, simulationMoves, staticObstacles, obstaclePoints);


        Map map = new SmallTorusMap(8, 6);
        List<IMappable> creatures = [new Orc("Dis"), new Elf("Legolas"), new Animals() { Description = "Ent" }, new Orc("Ur-Shak")];
        List<Simulator.Point> creaturePoints = [new Simulator.Point(2, 1), new Simulator.Point(2, 3), new Simulator.Point(4, 4), new(4,2)];
        string simulationMoves = GenerateRandomMoves(10);

        List<IMappable> staticObstacles = [new StaticObstacle("Mountain", NaturalElement.Earth), new StaticObstacle("River", NaturalElement.Water),
            new StaticObstacle("Lava", NaturalElement.Lava), new StaticObstacle("River", NaturalElement.Water),new StaticObstacle("Mist", NaturalElement.Fog)];
        List<Simulator.Point> obstaclePoints = [new(2, 2), new(3, 5), new(2, 5), new(3, 3), new(5, 4)];

        List<Item> items = Enumerable.Repeat((Item)new Coin(), 6).ToList();
        List<Point> itemPositions = [new(1, 0), new(0, 1), new(5, 0), new(7, 5), new(6, 2), new(4, 1)];
        
        //List<IMappable> creatures = [new Orc("Gorbag")];
        //List<Simulator.Point> creaturePoints = [new Simulator.Point(2, 1)];
        //string simulationMoves = "uuuuuuu";

        //List<IMappable> staticObstacles = [new StaticObstacle("Mist", NaturalElement.Air)];
        //List<Simulator.Point> obstaclePoints = [new(2, 2)];

        Simulation = new Simulation(map, creatures, creaturePoints, simulationMoves, staticObstacles, obstaclePoints, items, itemPositions);

        foreach (IMappable mappable in creatures)
        {
            CreaturesInMap += mappable.ToString() + " ; ";
        };

        History = new SimulationHistory(Simulation);
    }

    public void OnGet()
    {
        HttpContext.Session.SetInt32("MaxTurn", History.TurnLogs.Count - 1);
        HttpContext.Session.SetInt32("SizeX", History.SizeX);
        HttpContext.Session.SetInt32("SizeY", History.SizeY);
        for (int i = 0; i < History.TurnLogs.Count; i++)
        {
            HttpContext.Session.SetString($"Turn{i}.Mappable", History.TurnLogs[i].Mappable);
            HttpContext.Session.SetString($"Turn{i}.Move", History.TurnLogs[i].Move);
            HttpContext.Session.SetString($"Turn{i}.Symbols", History.TurnLogs[i].StrigifySymbols());
            HttpContext.Session.SetString($"Turn{i}.Message", History.TurnLogs[i].SimulationMessage);
        }
    }
}
