using System.Reflection.Metadata;

Console.WriteLine("-----Notas de los estudiantes------");
String[] alumnos = {"Juan", "Pedro" , "Luisa", "Adriana", "Sofia"};
int[] notas = {80,76,86,77,88};
int promedioDeNotas = 0;

for (int i = 0; i < alumnos.Length; i++)
{
    Console.WriteLine($"El estudiante: {alumnos [i]} --- y su nota es de: {notas [i]}");
    promedioDeNotas += notas[i];
}

float promedio = (float)promedioDeNotas / alumnos.Length;
Console.WriteLine("Promedio de los estudiantes: ");
Console.WriteLine();
Console.WriteLine($"Promedio de notas: {promedio:F2}" );
