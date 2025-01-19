using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

/// <summary>
/// Map of points.
/// </summary>
public abstract class Map
{
    /// <summary>
    /// Horizontal map size.
    /// </summary>
    public int SizeX { get; init; }

    /// <summary>
    /// Vertical map size.
    /// </summary>
    public int SizeY { get; init; }

    /// <summary>
    /// Rectangle created out of map dimensions. Always start in (0,0) and ends in (SizeX-1, SizeY-1).
    /// </summary>
    private readonly Rectangle _map;

    /// <summary>
    /// Map constructor. Map cannot be smaller than 5 units each direction.
    /// </summary>
    public Map(int sizeX, int sizeY)
    {
        if (sizeX < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Map size cannot be smaller than 5"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }
        else if (sizeY < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Map size cannot be smaller than 5"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }
        SizeX = sizeX;
        SizeY = sizeY;
        _map = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
     }

    /// <summary>
    /// Check if given point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public bool Exist(Point p)
    {
        return _map.Contains(p);
    }

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction 
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);

    /// <summary>
    /// Adds indicated creature to the map in the given point.
    /// </summary>
    /// <param name="creature">Creature to add to map.</param>
    /// <param name="point">Starting point of creature in the map.</param>
    /// <returns>N/A - Creature added to the map.</returns>
    public abstract void Add(IMappable mappable, Point point);

    /// <summary>
    /// Removes creature from indicated point from the map.
    /// </summary>
    /// <param name="creature">Creature to remove from the map.</param>
    /// <param name="point">Point from where creature should be removed from.</param>
    /// <returns>N/A - Creature added to the map.</returns>
    public abstract void Remove(IMappable mappable, Point point);

    /// <summary>
    /// Moving Creature in the map.
    /// </summary>
    /// <param name="creature">Creature to move.</param>
    /// <param name="startPoint">Starting position of creature.</param>
    /// <param name="endPoint">Target position of creature.</param>
    /// <returns>N/A - Creature moved between indicated points.</returns>
    public void Move(IMappable mappable, Point startPoint, Point endPoint)
    {
        if (!mappable.IsLost)
        {
            Remove(mappable, startPoint);
            Add(mappable, endPoint);
        } else
        {
            Remove(mappable, startPoint);
            Random random = new Random();
            Array allMoves = Enum.GetValues(typeof(Direction));
            Direction newDirection = (Direction)allMoves.GetValue(random.Next(allMoves.Length));
            mappable.IsLost = false;
            Add(mappable, Next(startPoint, newDirection));
        }
    }

    /// <summary>
    /// Checks what creatures are in the indicated point of map (x, y).
    /// </summary>
    /// <param name="x">X coordinate of point in map.</param>
    /// <param name="y">Y coordinate of point in map.</param>
    /// <returns>List<IMappable> of creatures in indicated point.</returns>
    public abstract List<IMappable> At(int x, int y);

    /// <summary>
    /// Checks what creatures are in the indicated point of map (Point).
    /// </summary>
    /// <param name="point">Point where to look for list of creatures in the map.</param>
    /// <returns>List<IMappable> of creatures in indicated point.</returns>
    public abstract List<IMappable> At(Point point);

    /// <summary>
    /// Checks what obstacles are in the indicated point of map (Point).
    /// </summary>
    /// <param name="point">Point where to look for list of obstacles in the map.</param>
    /// <returns>List<IMappable> of obstacles in indicated point.</returns>
    public abstract List<IMappable> ObstaclesAt(Point point);

    /// <summary>
    /// Checks what creatures are in the indicated point of map (Point).
    /// </summary>
    /// <param name="point">Point where to look for list of creatures in the map.</param>
    /// <returns>List(string) in indicated point.</returns>
    public abstract string StringListAt(int x, int y);
}
