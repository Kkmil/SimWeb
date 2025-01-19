using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Maps;

namespace Simulator;

public abstract class Creature : IMappable
{
    //Pola Prywatne
    private string _name = "Unknown"; //konwencja nazywania pól prywatnych _camelCase
    private int _level;

    //Właściwości + gettery/settery

    /// <summary>
    /// Creature's map assigment. Empty before placing Creature in the map.
    /// </summary>
    public Map? Map { get; set; }

    /// <summary>
    /// Creature's point in the map assigment.
    /// </summary>
    public Point Position { get; set; }

    public bool IsLost { get; set; } = false;

    /// <summary>
    /// Creature's Power identifier.
    /// </summary>
    public abstract int Power {  get; }

    /// <summary>
    /// Creature's backpack to store items.
    /// </summary>
    public abstract List<Item> Backpack { get; set; }

    /// <summary>
    /// Information about Creature.
    /// </summary>
    public abstract string Info { get; }

    /// <summary>
    /// Creature's Name (validation of given string applies).
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }
        init
        {
            _name = Validator.Shortener(value, 3, 25, '#');
        }
    }

    /// <summary>
    /// Creature's Level (validation of given int applies).
    /// </summary>
    public int Level
    {
        get
        {
            return _level;
        }
        init
        {
            _level = Validator.Limiter(value, 1, 10);
        }
    }

    /// <summary>
    /// Expected creature symbol to be displayed in SimConsole. Specific value to be added for Inheriting classes.
    /// </summary>
    public abstract char MapSymbol { get; } // Dodaję tutaj aby mieć większą pewność że bedzie doimplementowana w klasach dziedziczących

    //Konstruktor parametryczny
    /// <summary>
    /// Constructor of Creature based on provided name and level (def = 1).
    /// </summary>
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    //Konstruktor bez parametrów
    /// <summary>
    /// Constructor of Creature: non-parametric (does nothing).
    /// </summary>
    public Creature()
    {
        //Nic nie wykonuje
    }

    /// <summary>
    /// Placing Creature in the indicated map and position.
    /// </summary>
    /// <param name="map">Indicated map.</param>
    /// <param name="position">Indicated position (x,y) in the map.</param>
    /// <param name="requestFromMap">optional: Controls if the request came from the map or is initiated for creature.</param>
    /// <returns>Next point.</returns>
    public void InitMapAndPosition(Map map, Point position, bool requestFromMap = false)
    {
        Map = map;
        if (requestFromMap == false) // aby utrzymać możliwość zainicjowania stwora z jego poziomu a nie mapy (wzajemne odwołanie metody Add i InitMapAndPosition)
        {
            Map.Add(this, position);
        }
        Position = position;
    }

    /// <summary>
    /// Removing Creature from the Map it belongs to.
    /// </summary>
    /// <returns>N/A - Map is cleared for creature.</returns>
    public void RemoveFromMap()
    {
        Map = null;
    }

    /// <summary>
    /// Creature's Level upgade by 1.
    /// </summary>
    /// <returns>N/A - upgrading level only</returns>
    public void Upgrade()
    {
        if (_level < 10) //Sprawdzenie, że nie ma levelu 10 przed podniesieniem o 1
        {
            _level++;
        }
    }

    /// <summary>
    /// Moves creature in the indicated direction.
    /// Conditions of move may differ according to map.
    /// </summary>
    /// <returns>Text of move direction (lowercase).</returns>
    public string Go(Direction direction) //Metoda GO na pojedynczy ruch stwora
    {
        if (Map !=null) // Sprawdzenie czy stów nalezy do jakiejś mapy.
        {
            Map.Move(this, Position, Map.Next(Position, direction));
            return $"{direction.ToString().ToLower()}"; //konwersja na string i ma małe litery
        }
        else // Jezeli nie należy do mapy
        {
            return $"{Name} does not belong to any map."; 
        }
    }

    //public string[] Go(Direction[] directions) //Metoda GO na tablicę ruchów -> REZYGNUJEMY Z INNYCH METOD GO NA POZIOMIE  Lab-7
    //{
    //    string[] goTable = new string[directions.Length];
    //    for (int i = 0; i < directions.Length; i++)
    //    {
    //        goTable[i] = Go(directions[i]);
    //    }
    //    return goTable;
    //}

    //public string[] Go(string directionInputString) //Metoda GO parsująca string na tabelicę ruchów
    //{
    //    Direction[] directions = DirectionParser.Parse(directionInputString).ToArray();
    //    return Go(directions); //wejsciem jest tablica kierunków
    //}

    /// <summary>
    /// Creature's text presentation.
    /// </summary>
    /// <returns>Creature's information summary.</returns>
    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Name} [{Level}]{Info}";
    }

    /// <summary>
    /// Creature's Greeting. 
    /// </summary>
    /// <returns>Greeting string.</returns>
    public abstract string Greeting(); //Kod wykomentowany z powodu pojawienia sie metody abstrakcyjnej - for reference only
    //{
    //    return $"Hi, I'm {Name}, my level is {Level}.";
    //}
}
