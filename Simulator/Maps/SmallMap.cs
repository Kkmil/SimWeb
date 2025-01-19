using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

public abstract class SmallMap : Map
{
    private List<IMappable>?[,] _fields;

    /// <summary>
    /// SmallMap constructor. Map size in both directions needs to be in range [5,20]. 
    /// </summary>
    public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Map size cannot be larger than 20"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }
        else if (sizeY > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Map size cannot be larger than 20"); //wyjątek -> wymiar mapy nie pasuje do założeń
        }

        _fields = new List<IMappable>?[sizeX, sizeY];
    }

    public override void Add(IMappable mappable, Point point)
    {
        if (!Exist(point)) // sprawdzam czy punkt nalezy do mapy
        {
            throw new ArgumentException("Point needs to belong to the map", nameof(point));
        }
        else // punkt należy do mapy
        {
            if (_fields[point.X, point.Y] != null && _fields[point.X, point.Y].Count != 0)
            {
                _fields[point.X, point.Y].Add(mappable); // Lista creatuerów w tym punkcie już istnieje
            }
            else
            {
                var lista = new List<IMappable> { mappable }; // Lista w tym punkcie jeszcze nie istnieje - inicjuję listę.
                _fields[point.X, point.Y] = lista;
            }
            mappable.InitMapAndPosition(this, point, true);
        }
    }

    public override void Remove(IMappable mappable, Point point)
    {
        if (_fields[point.X, point.Y].Contains(mappable)) // Jest stwór o podanej nazwie na mapie
        {
            _fields[point.X, point.Y].Remove(mappable);
            mappable.RemoveFromMap();
        }
        else // W podanym punkcie nie ma stwora o podanej nazwie
        {
            throw new ArgumentException("In the indicated point there is no creature with given name", nameof(mappable));
        }
    }


    public override List<IMappable> At(int x, int y)
    {
        var point = new Point(x, y);
        return this.At(point);
    }

    public override List<IMappable> At(Point point) 
    {
        if (this.Exist(point) == false)
        {
            return new List<IMappable> { };
        }
        else
        {
            var listOfItemsInPoint = _fields[point.X, point.Y];
            List<IMappable> listOfCreaturesInPoint = [];
            if (listOfItemsInPoint != null && listOfItemsInPoint.Count != 0)
            {
                foreach (IMappable item in listOfItemsInPoint)
                {
                    if (item is not StaticObstacle)
                    {
                        listOfCreaturesInPoint.Add((IMappable)item);
                    }
                }
                return listOfCreaturesInPoint;
            }
            else
            {
                return new List<IMappable> { };
            }
        }
    }

    public override List<IMappable> ObstaclesAt(Point point)
    {
        if (this.Exist(point) == false)
        {
            return new List<IMappable> { };
        }
        else
        {
            var listOfItemsInPoint = _fields[point.X, point.Y];
            List<IMappable> listOfObstaclesInPoint = [];
            if (listOfItemsInPoint != null && listOfItemsInPoint.Count != 0)
            {
                foreach (IMappable item in  listOfItemsInPoint)
                {
                    if (item is StaticObstacle)
                    {
                        listOfObstaclesInPoint.Add((StaticObstacle)item);
                    }
                }
                
                return listOfObstaclesInPoint;
            }
            else
            {
                return new List<IMappable> { };
            }
        }
    }

    public override string StringListAt(int x, int y) //nadmiarowa metoda -> zwykłe Add zmieniam aby zwracało List<IMappable>
    {
        var point = new Point(x, y);
        if (this.Exist(point) == false)
        {
            return $"Point {point} does not belong to the map.";
        }
        else
        {
            var listOfCreaturesInPoint = _fields[point.X, point.Y];
            if (listOfCreaturesInPoint != null && listOfCreaturesInPoint.Count != 0)
            {
                var listaStworow = new String("");
                foreach (IMappable mappable in listOfCreaturesInPoint)
                {
                    var creatureName = mappable.Name;
                    listaStworow += creatureName + ", ";
                }

                return $"The creatures in point {point} are as follows: {listaStworow.Substring(0, listaStworow.Length - 2)}";
            }
            else
            {
                return $"There is no creature in the indicated point {point}.";
            }
        }
    }
}
