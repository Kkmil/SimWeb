using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public readonly struct Point
{
    public readonly int X, Y;
    public Point(int x, int y)
    {
        (X, Y) = (x, y);
    }
    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public Point Next(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Point(X, Y + 1);
            case Direction.Right:
                return new Point(X + 1, Y);
            case Direction.Down:
                return new Point(X, Y - 1);
            case Direction.Left:
                return new Point(X - 1, Y);
            default:
                Console.WriteLine("Nie obsluzony kierunek");
                return this; 
        }
    }

    
    public Point NextDiagonal(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Point(X + 1, Y + 1);
            case Direction.Right:
                return new Point(X + 1, Y - 1);
            case Direction.Down:
                return new Point(X - 1, Y - 1);
            case Direction.Left:
                return new Point(X - 1, Y + 1);
            default:
                Console.WriteLine("Nie obsluzony kierunek");
                return this; 
        }
    }
}
