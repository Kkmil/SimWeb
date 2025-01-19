using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class Elf : Creature
{
    //Pola prywatne
    private int _agility;
    private int _singCounter = 0; // licznik śpiewu: początkowo = 0
    private int _agilityBySingModifier = 3; // modyfikator: co ile śpiewów rośnie agility

    /// <summary>
    /// Elf symbol to be displayed in SimConsole.
    /// </summary>
    public override char MapSymbol => 'E'; // for SimConsole

    //Właściwości + gettery/settery
    public override int Power
    {
        get { return 8 * Level + 2 * Agility; }
    }

    public override List<Item> Backpack { get; set; } = new List<Item>();

    public override string Info
    {
        get 
        { 
            return $"[{Agility}]"; 
        }
    }

    public int Agility
    {
        get
        {
            return _agility;
        }
        init
        {
            _agility = Validator.Limiter(value, 0, 10);
        }
    }
    public void Sing()
    {
        _singCounter++; //licznik śpiewu +1
        if (_singCounter % _agilityBySingModifier == 0 && Agility < 10) //jeżeli ilość śpiewów podzielna przez modyfikator i agility < max
        {
            _agility++;
            _singCounter = 0; //zerowanie licznika śpiewu
        }
        else if ((_singCounter % _agilityBySingModifier == 0 && Agility >= 10) || Agility >= 10) //jeżeli ilość śpiewów podzielna przez modyfikator ale agility >= max
        {
            _singCounter = 0; //zerowanie licznika śpiewu
        }
    }

    //Konstruktor bezparametrowy
    public Elf() : base("Unknown Elf", 1)
    {
        //Brak działań
    }

    //konstruktor z parametrami
    public Elf(string name, int level = 1, int agility = 0) : base(name, level)
    {
        Agility = agility;
    }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.";
    }
}
