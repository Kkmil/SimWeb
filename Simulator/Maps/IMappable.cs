using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public interface IMappable
{
    /// <summary>
    /// Name of mappable object.
    /// </summary>
    public string Name { get; init; } // używam w nadmiarowej metodzie At, ta co zwraca stringi a nie List<IMappable>

    /// <summary>
    /// Symbold used to display in the map.
    /// </summary>
    char MapSymbol { get; }

    /// <summary>
    /// Identifies if mappable is lost (iterfears with air/fog obstacle).
    /// </summary>
    public bool IsLost { get; set; }

    /// <summary>
    /// Map assigment.
    /// </summary>
    public Map? Map { get; set; }

    /// <summary>
    /// Position assigment.
    /// </summary>
    public Point Position { get; set; }

    /// <summary>
    /// Movement method for a mappable object.
    /// </summary>
    string Go(Direction direction);

    /// <summary>
    /// Initiation in map method for a mappable object.
    /// </summary>
    void InitMapAndPosition(Map map, Point position, bool requestFromMap = false);

    /// <summary>
    /// Removing from map method for a mappable object.
    /// </summary>
    void RemoveFromMap();
}
