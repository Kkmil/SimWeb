using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Simulator.Maps;

namespace Simulator;

public class Animals : IMappable
{
    //Pola Prywatne
    private string _description = "Unknown"; //konwencja nazywania pól prywatnych _camelCase

    //Właściwości + gettery/settery
    public required string Description
    {
        get
        {
            return _description;
        }
        init
        {
            _description = Validator.Shortener(value, 3, 15, '#'); 
        }
    }
    public uint Size { get; set; } = 3;

    public virtual string Info { get; }

    public bool IsLost { get; set; } = false;

    public string Name 
    {
        get
        {
            return Description;
        }
        init
        {
            Name = Description;
        }
    }

    public virtual char MapSymbol {  get; set; } = 'A'; // Domyślny symbol na mapie dla animali

    public Map? Map { get; set; }

    public Point Position { get; set; }

    public virtual string Go(Direction direction)
    {
        if (Map != null)
        {
            Map.Move(this, Position, Map.Next(Position, direction));
        }
        return $"{direction.ToString().ToLower()}"; //konwersja na string i ma małe litery
    }

    public void InitMapAndPosition(Map map, Point position, bool requestFromMap = false)
    {
        {
            Map = map;
            Position = position; 
            if (requestFromMap == false)
            {
                Map.Add(this, Position);
            }
        }
    }

    public void RemoveFromMap()
    {
        Map = null;
    }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Description} {Info}<{Size}>";
    }
}
