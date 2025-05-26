// Variables

int consumoEnergia = 2;
int cantidadBotellas;
int diasSobrevividos = 0;
bool gameOver = false;
int diasDeSuperviviencia = 10;
bool accionRealizada;

//Selección de nombre de personaje
Console.WriteLine("Ingrese un nombre para su personaje: ");
String nombre = Console.ReadLine();
Console.WriteLine("El nombre de su jugador es: "+ nombre );

Console.WriteLine("Debes de sobrevivir durante 10 días.");
Console.WriteLine("Contador de días: "+ diasDeSuperviviencia);

Console.WriteLine();

//Estadistícas principales
Random random = new Random ();
int energiaAleatoria = random.Next(60, 76);
int comidaAleatoria = random.Next(25, 31);
int aguaAleatoria = random.Next(20, 31);
cantidadBotellas = 1;
int energia = energiaAleatoria;
int comida = comidaAleatoria;
int agua = aguaAleatoria;

Console.WriteLine("Tus estadísticas iniciales son las siguientes: ");
Console.WriteLine("Energía:" + energiaAleatoria);
Console.WriteLine("Comida: " + comidaAleatoria);
Console.WriteLine("Agua: " + aguaAleatoria);
Console.WriteLine("Botellas de agua: " + cantidadBotellas);


Console.WriteLine();

//Menú de acciones
Console.WriteLine("A continuación, verás el 'Menú de opciones', esto sirve para decidir qué quieres hacer.");
bool salir = false;

Console.WriteLine();

while (!salir && !gameOver && diasSobrevividos < diasDeSuperviviencia)
{
    Console.WriteLine();
    Console.WriteLine("=====Menú de acciones=====");
    Console.WriteLine("1. Buscar comida");
    Console.WriteLine("2. Buscar agua");
    Console.WriteLine("3. Explorar");
    Console.WriteLine("4. Descansar");
    Console.WriteLine("5. Acabar el día");

    string opcion = Console.ReadLine();
    switch (opcion)
    {
        case "1":
            Console.WriteLine("Fuiste a buscar comida.");
            energia = BuscarComida(energia, ref comida);
            accionRealizada = true;
            break;
        case "2":
            Console.WriteLine("Fuiste a buscar agua.");
            energia = BuscarAgua(energia, ref agua, ref cantidadBotellas);
            accionRealizada = true;
            break;
        case "3":
            Console.WriteLine("Fuiste a explorar la isla.");
            energia = ExplorarIsla(energia, ref cantidadBotellas);
            accionRealizada = true;
            break;
        case "4":
            Console.WriteLine("Decidiste descansar.");
            energia = Descansar(energia);
            accionRealizada = true;
            break;
        case "5":
            Console.WriteLine($"Has decidido terminar el día {diasSobrevividos + 1}.");

            // Avanzar día
            diasSobrevividos++;

            // Consumo de comida
            if (comida >= 20)
            {
                comida -= 20;
            }
            else
            {
                int deficitComida = 20 - comida;
                comida = 0;
                energia -= deficitComida;
                Console.WriteLine($"No tienes suficiente comida. Pierdes {deficitComida} puntos de energía.");
            }

            // Consumo de agua
            if (agua >= 15)
            {
                agua -= 15;
            }
            else
            {
                int deficitAgua = 15 - agua;
                agua = 0;
                energia -= deficitAgua;
                Console.WriteLine($"No tienes suficiente agua. Pierdes {deficitAgua} puntos de energía.");
            }

            // Energía diaria base
            energia -= consumoEnergia;

            // Estadísticas después del día
            Console.WriteLine($"Día: {diasSobrevividos}/10");
            Console.WriteLine("\nEstas son tus estadísticas después del día:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Energía: {energia}");
            Console.WriteLine();
            Console.WriteLine($"Comida: {comida}");
            Console.WriteLine();
            Console.WriteLine($"Agua: {agua}");
            Console.WriteLine();
            Console.WriteLine($"Botellas de agua: {cantidadBotellas}");

            // Verificar muerte
            if (energia <= 0)
            {
                Console.WriteLine("¡Has muerto! No pudiste sobrevivir.");
                gameOver = true;
                break;
            }

            // Evento nocturno (10% probabilidad)
            double probabilidadEvento = random.NextDouble();
            if (probabilidadEvento <= 0.10)
            {
                int eventoNocturno = random.Next(1, 4); // 1 a 3
                switch (eventoNocturno)
                {
                    case 1:
                        int aguaExtra = 10 * cantidadBotellas;
                        agua += aguaExtra;
                        Console.WriteLine($"Evento nocturno: Lluvia. Ganaste {aguaExtra} puntos de agua.");
                        break;
                    case 2:
                        comida -= 10;
                        if (comida < 0) comida = 0;
                        Console.WriteLine("Evento nocturno: Animales salvajes. Perdiste 10 puntos de comida.");
                        break;
                    case 3:
                        energia -= 10;
                        if (energia < 0) energia = 0;
                        Console.WriteLine("Evento nocturno: Clima frío. Perdiste 10 puntos de energía.");
                        break;
                }
            }

            // Verificar si sobrevivió todos los días
            if (diasSobrevividos >= diasDeSuperviviencia)
            {
                Console.WriteLine("¡Felicidades! Has sobrevivido los 10 días.");
                gameOver = true;
            }
            break;

        default:
            Console.WriteLine("No es una opción válida. ¡Vuelvelo a intentar!");
            break;
    }
}

    
//ACCIONES PARA EL MENÚ
Console.WriteLine();

//ACCIÓN DE COMIDA
int BuscarComida(int energiaActual, ref int comidaActual)
{
    int energiaGastada = random.Next(5, 16);
    Console.WriteLine();
    Console.WriteLine($"Gastaste {energiaGastada} puntos de energía buscando comida.");
    int nuevaEnergia = energiaActual - energiaGastada;
    if (nuevaEnergia < 0) return energiaActual;

    double probabilidadPez = 0.3;
    double probabilidadFruta = 0.5;
    double randomNumber = random.NextDouble();
    int comidaEncontrada = 0;
    string alimento = "";

    if (randomNumber < probabilidadPez)
    {
        comidaEncontrada = 30;
        alimento = "peces";
    }
    else if (randomNumber < probabilidadPez + probabilidadFruta)
    {
        comidaEncontrada = 25;
        alimento = "frutas";
    }
    else
    {
        comidaEncontrada = 10;
        alimento = "semillas";
    }
    comidaActual += comidaEncontrada;
    Console.WriteLine();
    Console.WriteLine($"¡Encontraste {alimento}! Obtuviste +{comidaEncontrada} de comida.");
    return nuevaEnergia;
}

//CONDICIÓN DE AGUA 
int BuscarAgua(int energiaActual, ref int aguaActual, ref int botellasActual)
{
    int energiaGastada = random.Next(10, 21);
    Console.WriteLine();
    Console.WriteLine($"Gastaste {energiaGastada} puntos de energía buscando agua.");
    int nuevaEnergia = energiaActual - energiaGastada;
    if (nuevaEnergia < 0) return energiaActual;

    double probabilidadPotable = 0.8;
    double randomNumber = random.NextDouble();
    int aguaEncontrada = 0;
    string tipo = "";
    int efecto = 0;

    if (randomNumber < probabilidadPotable)
    {
        if (botellasActual > 0)
        {
            aguaEncontrada = 20 * botellasActual;
            tipo = "potable";
            efecto = aguaEncontrada;
            aguaActual += aguaEncontrada;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Encontraste agua potable, pero no tienes botellas.");
        }
    }
    else
    {
        tipo = "contaminada";
        efecto = -10;
        nuevaEnergia -= 10;
        if (nuevaEnergia < 0) nuevaEnergia = 0;
    }
    Console.WriteLine();
    Console.WriteLine($"Encontraste agua {tipo}. Efecto: {efecto} (agua o energía).");
    return nuevaEnergia;
}

//CONDICIÓN DE DESCANSO 
int Descansar(int energiaActual)
{
    int energiaRecuperada = 20;
    int nuevaEnergia = energiaActual + energiaRecuperada;
    Console.WriteLine();
    Console.WriteLine($"Descansaste y recuperaste {energiaRecuperada} puntos de energía.");
    return nuevaEnergia;
}

//CONDICIÓN EXPLORAR LA ISLA 
int ExplorarIsla(int energiaActual, ref int botellasActual)
{
    double probabilidadAnimales = 0.3;
    double probabilidadTerrenoPeligroso = 0.2;
    double probabilidadEncontrarBotella = 0.5;
    double randomNumber = random.NextDouble();
    Console.WriteLine();
    Console.WriteLine("Explorando la isla...");
    int energiaPerdida = 0;
    string evento = "";

    if (randomNumber < probabilidadAnimales)
    {
        Console.WriteLine();
        energiaPerdida = 10;
        evento = "animales salvajes";
    }
    else if (randomNumber < probabilidadAnimales + probabilidadTerrenoPeligroso)
    {
        Console.WriteLine();
        energiaPerdida = 20;
        evento = "terreno peligroso";
    }
    else if (randomNumber < probabilidadAnimales + probabilidadTerrenoPeligroso + probabilidadEncontrarBotella)
    {
        Console.WriteLine();
        botellasActual++;
        evento = "encontrar botella";
    }
    else
    {
        Console.WriteLine();
        evento = "nada interesante";
    }
    energiaActual -= energiaPerdida;
    Console.WriteLine();
    Console.WriteLine($"Exploración: Encontraste {evento}. Energía modificada: -{energiaPerdida}.");
    return energiaActual < 0 ? 0 : energiaActual;
}
//FIN ACCIONES DE MENU

