using Simulator;
using Simulator.Maps;

namespace Runner;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");

        //var a = new Elf("Elandor", 7, 8);
        //Console.WriteLine(a.Go(Direction.Right));

        //var TestowaMapa = new SmallSquareMap(5);
        //var TestowyPunkt = new Point(1, 1);
        //a.InitMapAndPosition(TestowaMapa, TestowyPunkt);
        //Console.WriteLine(TestowaMapa.At(TestowyPunkt));
        //Console.WriteLine(TestowaMapa.At(6, 6));

        //var InnyTestowyPunkt = new Point(2, 2);
        //var o = new Orc("Shrek", 7, 8);
        //var g = new Orc("Goblin", 1, 1);
        //TestowaMapa.Add(o, InnyTestowyPunkt);
        //TestowaMapa.Add(g, InnyTestowyPunkt);
        //Console.WriteLine(TestowaMapa.At(2, 2));


        //TestowaMapa.Remove(a, TestowyPunkt);
        //Console.WriteLine(TestowaMapa.At(TestowyPunkt));


        //o.Go(Direction.Right);
        //var InnyTestowyPunkt2 = new Point(3, 2);
        //Console.WriteLine(TestowaMapa.At(2, 2));
        //Console.WriteLine(TestowaMapa.At(InnyTestowyPunkt2));


        SmallSquareMap map = new(5);
        List<IMappable> creatures = [new Orc("Gorbag"), new Elf("Elandor")];
        List<Point> points = [new(2, 2), new(3, 1)];
        string moves = "dlrludl";

        Simulation simulation = new(map, creatures, points, moves);
        Console.WriteLine(map.At(2, 2));
        Console.WriteLine(map.At(3, 1));
        simulation.Turn();
        Console.WriteLine(map.At(2, 2));
        Console.WriteLine(map.At(2, 1));
        Console.WriteLine(map.At(3, 1));
        simulation.Turn();
        Console.WriteLine(map.At(2, 1));
        Console.WriteLine(map.At(3, 1));


    }
}
