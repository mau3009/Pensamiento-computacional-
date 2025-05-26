using System.Globalization;

Console.WriteLine("Ingrese un texto: ");
string texto = Console.ReadLine();

String [] palabras = texto.Split(new char []{' '}, StringSplitOptions.RemoveEmptyEntries);
int cantidadPalabras = palabras.Length;

TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
string textoCapitalizado = textInfo.ToTitleCase(texto.ToLower());

Console.WriteLine($"\ncantidad de palabras ingresadas: {cantidadPalabras}");
Console.WriteLine();
Console.WriteLine("Texto con la primera letra de cada palabra en mayúscula: ");
Console.Write(textoCapitalizado);