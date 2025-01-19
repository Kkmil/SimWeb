using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simulator;

public static class Validator
{
    public static int Limiter(int value, int min, int max) 
    {
        if (value <= min)
        {
            value = min;
        }
        else if (value >= max) 
        {
            value = max;
        }
        return value;
    }

    public static string Shortener(string value, int min, int max, char placeholder)
    {
        string validatedName = value.Trim(); //Ucina białe znaki na początku i na końcu
        if (validatedName == "" || validatedName == null) //Wyłapanie sytuacji w której z nazwy nic nie zostało po Trim()
        {
            return "###"; // zmiana z "Unknown"
        }
        else
        {
            if (validatedName.Length < min)
            {
                validatedName = validatedName.PadRight(min, placeholder); //Uzyskanie nazwy na minimalną ilość znaków
            }
            else if (validatedName.Length > max)
            {
                validatedName = validatedName.Substring(0, max).TrimEnd(); //ucięcie stringa do maxymalną ilość znaków + ucięcie białych znaków z końca
                if (validatedName.Length < min)
                {
                    validatedName = validatedName.PadRight(min, placeholder); //Uzyskanie nazwy na minimalną ilość znaków
                }
            }
            if (char.IsLower(validatedName[0]))
            {
                validatedName = char.ToUpper(validatedName[0]) + validatedName.Substring(1); //Ustawienie pierwszej litery na wielka
            }
            return validatedName;
        }
    }
}


