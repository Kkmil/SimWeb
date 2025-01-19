using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace SimConsole;

public class MapVisualizer
{
    /// <summary>
    /// Top edge of the table.
    /// </summary>
    private string _topEdge = "";

    /// <summary>
    /// Bottom edge of the table.
    /// </summary>
    private string _bottomEdge = "";

    /// <summary>
    /// Inner line/edge in table
    /// </summary>
    private string _innerEdge = "";

    /// <summary>
    /// Array of strings with data to be displayed in table.
    /// </summary>
    public string[] dataRows; // public for tests

    public Map Map { get; } // public for tests

    /// <summary>
    /// MapVisualizer constructor.
    /// </summary>
    /// <param name="map">Map to be visualized.</param>
    public MapVisualizer(Map map)
    {
        Map = map;
        dataRows = new string[Map.SizeY];
        InitTableEdges();
    }

    /// <summary>
    /// Initialization of horizontal edges for visualization table based on map size.
    /// </summary>
    private void InitTableEdges()
    {
        int width = this.Map.SizeX - 1;
        _topEdge += Box.TopLeft;
        for (int i = 0; i < width; i++)
        {
            _topEdge += Box.Horizontal;
            _topEdge += Box.TopMid;
        }
        _topEdge += Box.Horizontal;
        _topEdge += Box.TopRight;

        _bottomEdge += Box.BottomLeft;
        for (int i = 0; i < width; i++)
        {
            _bottomEdge += Box.Horizontal;
            _bottomEdge += Box.BottomMid;
        }
        _bottomEdge += Box.Horizontal;
        _bottomEdge += Box.BottomRight;

        _innerEdge += Box.MidLeft;
        for (int i = 0; i < width; i++)
        {
            _innerEdge += Box.Horizontal;
            _innerEdge += Box.Cross;
        }
        _innerEdge += Box.Horizontal;
        _innerEdge += Box.MidRight;
    }
    /// <summary>
    /// Initialization of rows with data, where creatues symbols are kept.
    /// </summary>
    private void InitDataRows()
    {
        for (int y = 0; y < Map.SizeY; y++) // ilość wierszy w tabeli do wyświettlenia taka jak rozmiar mapy w pionie
        {
            dataRows[y] = Box.Vertical.ToString(); // dodanie sybolu krawedzi kolumny -> pierwszy char inicjujący string potrzebuje metody ToString
            for (int x = 0; x < Map.SizeX; x++) // ilość kolumn taka jak rozmiar mapy w poziomie
            {
                List<IMappable> creaturesInPoint = this.Map.At(x, y);
                List<IMappable> obstaclesInPoint = this.Map.ObstaclesAt(new Point(x, y));
                if (obstaclesInPoint.Count > 0)
                {
                    dataRows[y] += obstaclesInPoint[0].MapSymbol;
                }
                else if (creaturesInPoint == null || creaturesInPoint.Count == 0) // jeżeli lista stworów w punkcie jest pusta
                {
                    dataRows[y] += " ";
                }
                else if (creaturesInPoint.Count == 1) // jeżeli na polu mapy jest dokładnie jeden stwór
                {
                    dataRows[y] += creaturesInPoint[0].MapSymbol;
                }
                else // jezeli na polu mapy jest wiecej niż jeden stwór
                {
                    dataRows[y] += 'X'; // dodane na "sztywno"
                }
                dataRows[y] += Box.Vertical; // dodanie sybolu krawedzi kolumny
            }
        }
    }

    /// <summary>
    /// Drawing visualization of map in console (table view).
    /// </summary>
    public void Draw()
    {
        InitDataRows(); // stworzenie danych na podtsawie bierzącego stanu mapy. Metoda nie wie którą turę ma narysować -> rysuje stan bierzący mapy.
        List<string> allRows = new List<string>(); // kolejne wiersze tabeli do wyświetlenia statyczne (krawdzie) i dynamiczne (dane)
        allRows.Add(_topEdge); // dodawanie od góry tabeli -> najpierw górna krawedz
        for (int j = Map.SizeY - 1; j > 0; j--) //wiersze z danymi muszą zostać podane w odwrotnej kolejności niż były stworzone -> wyświetlam mapę rysując ją "od góry"
        {
            allRows.Add(dataRows[j]);
            allRows.Add(_innerEdge); // krawedz statyczna miedzy wierszami z danymi
        }
        allRows.Add(dataRows[0]); // najniższy wiersz z danymi
        allRows.Add(_bottomEdge); // dolna krawedz tabeli
        foreach (string line in allRows) // wyswietlenie całej tabeli
        {
            Console.WriteLine(line);
        }
    }
}
