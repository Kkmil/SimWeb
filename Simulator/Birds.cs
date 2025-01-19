using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class Birds : Animals
{
    //Pola prywatne
    private bool _canFly = true;

    //Właściwości
    public bool CanFly
    {
        get { return _canFly; }
        init { _canFly = value; }
    }

    public override char MapSymbol => CanFly ? 'B' : 'b';

    public override string Info
    {
        get { return CanFly == true ? "(fly+) " : "(fly-) "; }
    }

    public override string Go(Direction direction)
    {
        if (Map != null)
        {
            switch (this.CanFly)
            {
                case true:
                    Map.Move(this, Position, Map.Next(Position, direction));
                    Map.Move(this, Position, Map.Next(Position, direction));
                    break;
                case false:
                    Map.Move(this, Position, Map.NextDiagonal(Position, direction));
                    break;
            }
        }
        return $"{direction.ToString().ToLower()}"; //konwersja na string i ma małe litery
    }
}
