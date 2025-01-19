using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class Coin : Item
    {
        public override char MapSymbol => '•';

        public Coin()
        {
            Name = "miecz";
        }
    }
}
