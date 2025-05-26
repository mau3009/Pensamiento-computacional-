// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hola");
Console.WriteLine("Ingrese una hora del día");

if (int.TryParse(Console.ReadLine(), out int hora) && hora >= 0 && hora <= 23)
{
    if (hora >=0 && hora <=11)
    {
        Console.WriteLine("Buenos días");
    }
    else if (hora >= 12 && hora <= 18)
    {
        Console.WriteLine("Buenas tardes");
    }
    else if  (hora >= 19 && hora <=23)
    {
        Console.WriteLine("Buenas noches");
    }
}
