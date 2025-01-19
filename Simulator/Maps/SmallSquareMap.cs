using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps
{
    public class SmallSquareMap : SmallMap
    {
        /// <summary>
        /// Size of the map - the same in both directions.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// SmallSquareMap constructor. Map size needs to be in range [5,20]. Map has the same size in both directions.
        /// Objects cannot exit the map when moving.
        /// </summary>
        public SmallSquareMap(int size) : base (size, size)
        {
            Size = size;
        }

        /// <summary>
        /// Next position to the point in a given direction.
        /// </summary>
        /// <param name="p">Starting point.</param>
        /// <param name="d">Direction.</param>
        /// <returns>Next point after move or input point if given move takes it outside the map.</returns>
        public override Point Next(Point p, Direction d)
        {
            // Check if point is inside the map
            if (Exist(p)) // Sprawdzenie tego warunku powtarza sie w 2 metodach w 2 klasach - mozna by wyłączyć?
            {
                Point pointAfterMove = p.Next(d);
                if (Exist(pointAfterMove))
                {
                    return pointAfterMove;
                }
                else
                {
                    return p;
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
        /// <returns>Next point after move or input point if given move takes it outside the map.</returns>
        public override Point NextDiagonal(Point p, Direction d)
        {
            // Check if point is inside the map
            if (Exist(p)) // Sprawdzenie tego warunku powtarza sie w 2 metodach w 2 klasach - mozna by wyłączyć?
            {
                Point pointAfterMove = p.NextDiagonal(d);
                if (Exist(pointAfterMove))
                {
                    return pointAfterMove;
                }
                else
                {
                    return p;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(p), "Provided point is outside of the map.");
            }
        }
    }
}
