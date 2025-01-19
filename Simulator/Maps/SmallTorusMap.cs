using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public class SmallTorusMap : SmallMap
{
    /// <summary>
    /// SmallTorusMap constructor. Map size needs to be in range [5,20] for each direction. Can be rectangular.
    /// Torus behavior - objects are transfering to oposite edge when exiting map while move.
    /// </summary>
    public SmallTorusMap(int sizeX, int sizeY) : base (sizeX, sizeY) 
    {
    }

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point after move. Mind that in Torus exiting the map on one edge causes returining on th eoposite edge!</returns>
    public override Point Next(Point p, Direction d)
    {
        // Check if point is inside the map
        if (Exist(p)) // Sprawdzenie tego warunku powtarza sie w 2 metodach w 2 klasach - mozna by wyłączyć?
        {
            Point pointAfterMove = p.Next(d);
            if (Exist(pointAfterMove)) // W zasadzie mozna tą metodę zmienić tak aby przypominała NextDiagonal - Refactoring?
            {
                return pointAfterMove;
            }
            else
            {
                switch (d)
                {
                    case (Direction.Up):
                        return new Point(p.X, 0);
                    case (Direction.Right):
                        return new Point(0, p.Y);
                    case (Direction.Down):
                        return new Point(p.X, SizeY - 1);
                    case (Direction.Left):
                        return new Point(SizeY - 1, p.Y);
                    default:
                        return p;
                }
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(p), "Provided point is outside of the map.");
        }
    }

    /// <summary>
    /// Next diagonal position to the point in a given direction 
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point after move. Mind that in Torus exiting the map on one edge causes returining on th eoposite edge!</returns>
    public override Point NextDiagonal(Point p, Direction d)
    {
        // Check if point is inside the map
        if (Exist(p)) // Sprawdzenie tego warunku powtarza sie w 2 metodach w 2 klasach - mozna by wyłączyć?
        {
            Point pointAfterMove = p.NextDiagonal(d); // W zasadzie to co w else wystarcza na każdy ruch - Refactoring?
            if (Exist(pointAfterMove))
            {
                return pointAfterMove;
            }
            else
            {
                switch (d)
                {
                    case (Direction.Up):
                        return new Point((p.X + 1) % SizeY, (p.Y + 1) % SizeY); // Modulo przeniesie punkt na początek krawedzi (reszta z dzielenia)
                    case (Direction.Right):
                        return new Point((p.X + 1) % SizeX, (p.Y - 1 + SizeX) % SizeX); // + Size kiedy odejmuję 1 aby uniknąć liczb ujemnych, w połączeniu z Modulo nie wpływa na wynik
                    case (Direction.Down):
                        return new Point((p.X - 1 + SizeY) % SizeY, (p.Y - 1 + SizeY) % SizeY);
                    case (Direction.Left):
                        return new Point((p.X - 1 + SizeX) % SizeX, (p.Y + 1) % SizeX);
                    default:
                        return p;
                }
            }    
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(p), "Provided point is outside of the map.");
        }
    }
}
