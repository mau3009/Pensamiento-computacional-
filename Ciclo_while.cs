// See https://aka.ms/new-console-template for more information

Random generador = new Random();
int aleatorio = generador.Next(0,51);
int intento;
bool acertado = false;
Console.WriteLine("Adivina el número entre 0 y 50");

while (!acertado)
{
    Console.WriteLine("Introduce un número: ");
    if (int.TryParse(Console.ReadLine(), out intento))
    {
        if (intento == aleatorio)
        {
            Console.WriteLine("Haz adivinado el número");
            acertado = true;
        }
        else if (intento < aleatorio)
        {
            Console.WriteLine("El número es mayor, intentalo de nuevo");
        }
        else if (intento > aleatorio)
        {
            Console.WriteLine("El número es menor, intentalo de nuevo");
        }
    }
    else 
    {
        Console.WriteLine("No es un número, Ingresa un valor númerico entre 0 a 50.");
    }

}