int opcion; 
    //CONFIGURACIÓN DEL MENÚ
    do 
    {
            Menu();
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: 
                    celsiusAFahrenheit();
                    break;
                case 2:
                    FahrenheitACelsius();
                    break;
                case 3:
                    informacionProgramador();
                    break;
                case 4: 
                    Console.WriteLine("Saliendo, ¡Espero haberte ayudado!.....");
                    break;
            }
            if (opcion != 4)
            {
                Console.WriteLine("\nPresione cualquier tecla si desea continuar......");
                Console.ReadKey();
                Console.Clear();
            }

    } while (opcion != 4);



    //PROCEDIMIENTOS
    void Menu ()
    {
        Console.WriteLine("******MENÚ********");
        Console.WriteLine("    1. Celsius a Fahrenheit");
        Console.WriteLine();
        Console.WriteLine("    2. Fahrenheit a Celsius");
        Console.WriteLine();
        Console.WriteLine("    3. Información del programador");
        Console.WriteLine();
        Console.WriteLine("    4. salir");
        Console.WriteLine();
        Console.WriteLine("Seleccion una de las opciones presentes: ");
    }

   
    void celsiusAFahrenheit ()
    {
        Console.WriteLine("Ingrese los grados celsius que desea pasar a Farenheit: ");
        double celsius;
        celsius = Convert.ToInt32(Console.ReadLine());
        double resultado = ConvertirCelsiusAFahrenheit(celsius);
        Console.WriteLine($"Resultado {resultado} °F");
    }


    void FahrenheitACelsius()
    {
        Console.WriteLine("Ingrese los grados Fahrenheit que desea pasar a Celsius: ");
        double Fahrenheit;
        Fahrenheit= Convert.ToInt32(Console.ReadLine());
        double resultado = convertirFahrenheitACelsius(Fahrenheit);
        Console.WriteLine($"Resultado {resultado} °C ");
    }


    void informacionProgramador()
    {
        Console.WriteLine("Nombre: Mauricio David Lima Juárez");
        Console.WriteLine("Curso: Pensamiento Computacional (práctica)");
        Console.WriteLine("Docente encargado: GÁLVEZ ARRIAZA ANDRES SEBASTIAN ");
        Console.WriteLine("Fecha: 29 de marzo de 2025");
    }



    //FUNCIONES
      double ConvertirCelsiusAFahrenheit (double celsius)
    {
        return (celsius * 9/5) + 32;
    }
      double convertirFahrenheitACelsius (double Fahrenheit)
    {
        return (Fahrenheit - 32)* 5/9;
    }

