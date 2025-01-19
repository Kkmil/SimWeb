using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    private int _currentTurnIndex; // index kolejki (zaczynając od 0 w konstruktorze
    private int _currentCreatureIndex; 
    private int _numberOfTurns; // dla czytelności kodu -> ilość kolejek na podstawie długości sparsowanego stringu Moves
    private bool _creatureKilled = false;
    public bool simulationMessage = false;
    public string endingMessage;

    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<IMappable> Creatures { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves, 
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }


    /// <summary>
    /// Obstacles on the map.
    /// </summary>
    public List<IMappable> StaticObstacles { get; }

    /// <summary>
    /// Positions of obstacles.
    /// </summary>
    public List<Point> ObstaclePositions { get; }

    /// <summary>
    /// Items on the map.
    /// </summary>
    public List<Item> Items { get; }

    /// <summary>
    /// Positions of items.
    /// </summary>
    public List<Point> ItemPositions { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public IMappable CurrentCreature 
    {
        get 
        {
            if (_currentCreatureIndex >= Creatures.Count) _currentCreatureIndex = 0;
            return Creatures[_currentCreatureIndex]; 
        }
    }

    /// <summary>
    /// Number of current turn (starting from 1). Used in SimConsole display.
    /// </summary>
    public int CurrentTurnNumber
    {
        get
        {
            return _currentTurnIndex + 1;
        }
    }

    /// <summary>
    /// List of moves parsed to a table of directions.
    /// </summary>
    private List<Direction> _directionListForSimulation // dla cztelności kodu - tablica sparsowana kierunków ruchów w kolejnych kolejkach => ilość kolejek 
    {
        get
        {
            return DirectionParser.Parse(Moves);
        }
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName
    {
        get
        {
            return $"{_directionListForSimulation[_currentTurnIndex].ToString().ToLower()}";
        }
        set { }
    }


    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<IMappable> creatures,
        List<Point> positions, string moves, List<IMappable> staticObstacles, List<Point> obstaclePositions, 
        List<Item> items, List<Point> itemPositions)

    {
        Map = map;
        if (creatures.Count == 0) // Walidacja dla pustego stringu stworów
        {
            throw new ArgumentException("List of Creatures cannot be empty", nameof(creatures));
        }
        else if (creatures.Count != positions.Count) // Walidacja dla różnej ilosci stworów i pozycji (to załatwia też pustą listę pozycji)
        {
            throw new ArgumentException("Number of creatures and their starting positions must match", nameof(positions));
        }
        else
        {
            Creatures = creatures;
            Positions = positions;
            StaticObstacles = staticObstacles;
            ObstaclePositions = obstaclePositions;
            Items = items;
            ItemPositions = itemPositions;
            Moves = moves;
            _currentTurnIndex = 0;
            _currentCreatureIndex = 0;
            _numberOfTurns = _directionListForSimulation.Count;

            // Inicjowanie stworów na mapie
            int i = 0; // Positions iterator
            foreach (IMappable mappable in creatures)
            {
                mappable.InitMapAndPosition(Map, Positions[i]);
                i++;
            }

            if (staticObstacles != null)
            {
                int j = 0;
                foreach (IMappable mappable in StaticObstacles)
                {
                    mappable.InitMapAndPosition(Map, ObstaclePositions[j]);
                    j++;
                }
            }

            if (items != null)
            {
                int j = 0;
                foreach (Item mappable in Items)
                {
                    mappable.InitMapAndPosition(Map, ItemPositions[j]);
                    j++;
                }
            }
        }
    }

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        simulationMessage = false;
        endingMessage = "";
        CurrentCreature.Go(_directionListForSimulation[_currentTurnIndex]);

        if (ObstaclePositions != null && ObstaclePositions.Contains(CurrentCreature.Position))
        {
            int obstacleIndex = ObstaclePositions.IndexOf(CurrentCreature.Position);
            StaticObstacle currentObstacle = (StaticObstacle)StaticObstacles[obstacleIndex];

            switch (currentObstacle.NaturalElement)
            {
                case NaturalElement.Lava:
                    endingMessage = $"{CurrentCreature} ulegl samospaleniu";
                    Drown();
                    break;
                case NaturalElement.Earth:
                    endingMessage = $"{CurrentCreature} nie jest w stanie przejść";
                    RevertMove();
                    break;
                case NaturalElement.Fog:
                    endingMessage = $"{CurrentCreature} zniknal we mgle";
                    CurrentCreature.IsLost = true;
                    break;
            }
            simulationMessage = true;

        }

        if (ItemPositions != null && ItemPositions.Contains(CurrentCreature.Position))
        {
            if (CurrentCreature is Creature creature) PickUpItem(creature);
        }

        if (Creatures.Count == 0)
        {
            this.Finished = true;
            endingMessage = "Wszystkie stwory nie żyją";
            simulationMessage = true;
        }



        _currentTurnIndex++;
        _currentCreatureIndex++;
        if (_creatureKilled)
        {
            _currentCreatureIndex--;
            _creatureKilled = false;
        }

        if (_currentTurnIndex == _numberOfTurns)
        {
            this.Finished = true;
            endingMessage = "Symulacja zakończona oto wyniki:";
            foreach (IMappable mappable in this.Creatures) {
                if (mappable.Map != null)// || mappable is Creature creature)
                {
                    if (mappable is Creature creature)
                    {
                        if(creature.Backpack != null)
                            endingMessage += $"\n{creature.Name} podniósł {creature.Backpack.Count} {(creature.Backpack.Count !=1 ? "mieczy" : "miecz")}!";
                    }
                }

            }

            simulationMessage = true;
        }


    }

    public void Drown()
    {
        Map.Remove(CurrentCreature, CurrentCreature.Position);
        Creatures.Remove(CurrentCreature);
        _creatureKilled = true;
    }

    public void RevertMove()
    {
        switch (CurrentMoveName)
        {
            case "up":
                CurrentCreature.Go(Direction.Down);
                break;
            case "right":
                CurrentCreature.Go(Direction.Left);
                break;
            case "down":
                CurrentCreature.Go(Direction.Up);
                break;
            case "left":
                CurrentCreature.Go(Direction.Right);
                break;
        }
    }

    public void PickUpItem(Creature creature)
    {
        Point itPosisiton = creature.Position;

        foreach (IMappable mappable in Map.At(itPosisiton))
        {
            if (mappable is Item it)
            {
                if (creature.Backpack != null)
                {
                    Map.Remove(it, itPosisiton);
                    creature.Backpack.Add(it);
                    Items.Remove(it);
                    ItemPositions.Remove(itPosisiton);
                    endingMessage = $"{creature} podniósł {it.Name}!\n";
                }
            }
        }
    }
}
