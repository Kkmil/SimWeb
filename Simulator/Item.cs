using Simulator.Maps;
using System.Xml.Linq;

namespace Simulator
{
    public abstract class Item : IMappable
    {
        private string _name = "Unknown item";

        public virtual char MapSymbol => '-';

        public bool IsLost { get; set; } = false;
        public Map? Map { get; set; }
        public Point Position { get; set; }

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

        public void InitMapAndPosition(Map map, Point position, bool requestFromMap = false)
        {
            Map = map;
            if (requestFromMap == false) 
            {
                Map.Add(this, position);
            }
            Position = position;
        }

        public void RemoveFromMap()
        {
            Map = null;
            
        }

        public string Go(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
