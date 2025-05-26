using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

//Definir los tipos de vehiculos que permite el estacionamiento
public enum ClaseDeVehiculo
    {
        Moto,
        Sedan,
        Suv,

    }

    //Características de cada vehiculo
    public class Vehiculo 
    {
        public string Marca {get; set;}
        public string Color {get; set;}
        public string Placa {get; set;}
        public ClaseDeVehiculo Tipo {get; set;}
        public int HoraEntrada {get; set;}
        
        //Constructor el cual inicializa un vehículo con sus datos
        public Vehiculo(string marca, string color, string placa, ClaseDeVehiculo tipo, int hora_entrada)
        {
            Marca = marca;
            Color = color;
            Placa = placa;
            Tipo = tipo;
            HoraEntrada = hora_entrada;
        }
    }


    public class Estacionamiento
    {
        public Vehiculo [,] parqueo; //Matriz de los estacionamientos 
        public ClaseDeVehiculo[,] tipos; //Matriz que almacen los tipos de vehiculos
        public int pisos, espacios; //Cantidad de espacio. 

        //Constructor que inicializa el estacionamiento con la distribución de espacios 
        public  Estacionamiento (int cantidadPisos, int espaciosPorPiso, int cantidadMotos, int cantidadSuvs)
        {
            pisos = cantidadPisos;
            espacios = espaciosPorPiso;
            parqueo = new Vehiculo[pisos,espacios]; //Inicializa la matriz de parqueo
            tipos = new ClaseDeVehiculo[pisos, espacios]; //Inicializa la matriz de los tipos
            AsignarTipos(cantidadMotos, cantidadSuvs); // Asigna los tipo de espacios
        }

        //Asigna el espacio de cada tipo, especialmente para moto y suv, y lo restante es para sedan. 
        private void AsignarTipos(int motos, int suvs)
        {
            int total = pisos * espacios;

            for (int i = 0; i < pisos; i++)
            {
                for (int j = 0; j<espacios; j++)
                {
                    if (motos > 0)
                    {
                        tipos[i, j] = ClaseDeVehiculo.Moto;
                        motos--;
                    }
                    else if  (suvs > 0)
                    {
                        tipos [i, j] = ClaseDeVehiculo.Suv;
                        suvs--;
                    }
                    else 
                    {
                        tipos [i, j] = ClaseDeVehiculo.Sedan;
                    }
                }
            }
        }

        //Muestra la matriz de parqueos ya realizada y actualizada, según el ingreso de datos
        public void MostrarMapaCompletoYDisponibles()
        {
            Console.WriteLine("\nMAPA COMPLETO DEL ESTACIONAMIENTO: ");
            for (int i = 0; i < pisos; i++)
            {
                for (int j = 0; j < espacios; j++)
                {
                    if(parqueo[i, j] == null)
                        Console.Write($"{Codigo(i,j).PadLeft(4)}"); //Muestra código del espacio libre
                    else
                    {
                        Console.Write(" X "); //Muestra los espacios ocupados
                    }
                }
                Console.WriteLine();
            }
            int motosLibres = 0;//Cuenta los espacios libres que hay en cada tipo
            int SedanesLibres = 0;
            int SuvsLibres = 0;
            for (int i = 0; i < pisos; i++)
            {
                for(int j = 0; j< espacios; j++)
                {
                    if (parqueo[i, j] == null)
                    {
                        switch (tipos[i, j])
                        {
                            case ClaseDeVehiculo.Moto:
                                motosLibres++;
                                break;
                            case ClaseDeVehiculo.Sedan:
                                SedanesLibres++;
                                break;
                                case ClaseDeVehiculo.Suv: 
                                SuvsLibres++;
                                break;
                        }
                    }
                }
            }//Resultado de la cantidad de espacios disponibles para cada tipo 
            Console.WriteLine($"\nESPACIOS DISPONIBLES: \n - MOTOS: {motosLibres} \n - SEDANES {SedanesLibres}\n - SUVS: {SuvsLibres}");
        }
       
        //Ocupa el espacio si el tipo es compatible con el área libre
        public bool OcuparEspacio(string codigo, Vehiculo vehiculo)
        {
            if (ConvertirCodigo(codigo, out int fila, out int col) && tipos[fila,col] == vehiculo.Tipo && parqueo[fila,col] == null)
            {
            parqueo[fila, col] = vehiculo;
                return true;
            }
            return false;
        }


        //Busca el vehiculo por su placa y muestra el código de estacionamiento
        public string BuscarVehiculo(string placa)
        {
            for (int i = 0; i < pisos; i++)
            {
                for (int j = 0; j < espacios; j++)
                {
                    if (parqueo[i, j] != null & parqueo[i, j].Placa == placa)
                        return Codigo(i, j);
                }
            }
            return null; //Si no encuentra la placa 
        }

        //Retira un vehiculo y lo retorna 
        public Vehiculo RetirarVehiculo(string codigo)
        {
            if (ConvertirCodigo(codigo, out int fila, out int col) & parqueo[fila, col] != null)
            {
                Vehiculo carro = parqueo[fila,col];
                parqueo[fila,col] = null;
                return carro;
            }
            return null;
        }

        //Genera el código único para un espacio dado 
        private string Codigo( int fila, int col) => $"{(char)('A'+ fila )}{col+ 1}";

        //El código lo convierte a coordenadas de la matriz 
        private bool ConvertirCodigo(string codigo, out int fila, out int col)
        {
            fila = codigo[0] - 'A';
            col = Convert.ToInt32(codigo.Substring(1)) - 1;
            return fila >= 0 && fila < pisos && col >= 0 & col < espacios;
        } 
    }



    //PROGRAMACIÓN DEL MENÚ
    public class ProgramaInicial
    {
        static Random random = new Random(); //Para generar números aleatorios en los métodos
        static Estacionamiento parqueo; //El objeto principal de parqueo

        static void Main()
        {
            //datos principales - configuración 
            Console.WriteLine("Ingrese la cantidad de pisos:");
            int pisos = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("----------------------");
            Console.WriteLine("Ingresa la cantidad de estacionamientos por piso: ");
            int espacios = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("----------------------");
            Console.WriteLine("Ingresa la cantidad de espacios hay para motos: ");
            int motos = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("----------------------");
            Console.WriteLine("Ingresa la cantidad de espacios para Suv: ");
            int suv = Convert.ToInt32(Console.ReadLine());

            //Inicialización de parqueo
            parqueo = new Estacionamiento(pisos, espacios, motos, suv);

            //El menú de opciones
            int opciones;
            do
            {
                Console.WriteLine("1. Ingresar un vehiculo.");
                Console.WriteLine("2. Ingresar lote.");
                Console.WriteLine("3. Buscar vehículo.");
                Console.WriteLine("4. Retirar vehículo.");
                Console.WriteLine("5. Salir. ");
                opciones = Convert.ToInt32(Console.ReadLine());

                //Configuración de según el número seleccionado.
                switch (opciones)
                {
                    case 1:
                        IngresoManual();
                        break;
                    case 2:
                        IngresoLote();
                        break;
                    case 3:
                        BuscarVehiculo();
                        break;
                    case 4:
                        RetirarVehiculo();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo......");
                        Console.WriteLine("¡Esperamos haberte ayudadoo, cuidate!");
                        break;

                    default:
                        Console.WriteLine("Esta opción no forma parte de las opciones presentes. Intentalo de nuevo: ");
                        break;
                }
            } while (opciones != 5);
        }

    //MÉTODOS PARA MENÚ 
        static void IngresoManual()
        {
            Console.WriteLine("Marca: ");
            string marca = Console.ReadLine();
            Console.WriteLine("Color: ");
            string color = Console.ReadLine();
            Console.WriteLine("Ingresa la placa (deb de ser de 6 letras/números) : ");
            string placa = Console.ReadLine();
            Console.WriteLine("Tipo de vehiculo (Moto, Sedán o Suv): ");
            //Para intentar convertir la entrada del usuario a un valor del enum ClaseDeVEhiculo
            if (Enum.TryParse<ClaseDeVehiculo>(Console.ReadLine(), true, out ClaseDeVehiculo tipo))
            {

                Console.WriteLine("Hora de entrada (6 a 20): ");
                int HoraEntrada;
                bool hora = int.TryParse(Console.ReadLine(), out HoraEntrada);
                    //Validación del rango permitido de la hora 
                    if (!hora || HoraEntrada < 6 || HoraEntrada > 20)
                    {
                        Console.WriteLine("La hora es inválida. Debe de ser un número entre 6 y 20.");
                        return;
                    }
                    //Crea una nueva instancia de vehículo con los datos que se proporcionen 
                    Vehiculo vehiculo = new Vehiculo(marca, color, placa, tipo, HoraEntrada);
                    bool AsignarTipos = false; 
                    //Recorre el arreglo de parqueo para encontrar espacio disponible para el tipo 
                    for (int i = 0; i<parqueo.pisos && !AsignarTipos; i++)
                    {
                        for (int j = 0; j < parqueo.espacios && !AsignarTipos; j++)
                        {
                            //Genera el código del espacio 
                            string codigo = $"{(char)('A' + i)}{j+1}";
                            //Ocupa el espacio 
                            if (parqueo.OcuparEspacio(codigo, vehiculo))
                            {
                                Console.WriteLine($"Vehiculo exitosamente ingresado y asignado en estacionaminento:  ");
                                AsignarTipos = true;
                            }
                        }
                    }

                Console.WriteLine("Vehiculo exitosamente ingresado. ");
                Console.WriteLine("..................");
                Console.WriteLine($"Marca: {marca}");
                Console.WriteLine("..................");
                Console.WriteLine($"Color: {color} ");
                Console.WriteLine("..................");
                Console.WriteLine($"Placa: {placa}");
                Console.WriteLine("..................");
                Console.WriteLine($"Tipo: {tipo}");
                Console.WriteLine("..................");
                Console.WriteLine($"Hora de entrada: {HoraEntrada}");
                Console.WriteLine("..................");

            }
            else
            {
                Console.WriteLine("El tipo de vehículo es inválido. Debe de ser Moto, Sedan o Suv.");
            }
            parqueo.MostrarMapaCompletoYDisponibles();//muestra actualizado la matriz de parqueo

        }

        //Para ingresar varios vehiculos al azar 
        static void IngresoLote()
        {
            string[] marcas = { "Honda", "Mazda", "Hyundai", "Toyota", "Suzuki" };
            string[] Colores = { "Rojo", "Azul", "Negro", "Gris", "Blanco" };
            ClaseDeVehiculo[] tipos = { ClaseDeVehiculo.Moto, ClaseDeVehiculo.Sedan, ClaseDeVehiculo.Suv };
            Random random = new Random();

            //Se genera un valor entre 2 y 6 vehículos aleatorios, se coloca hasta 7 debido a que el primer valor inicia en 0.
            int cantidad = random.Next(2, 7);// En este rango se coloca así porque no utiliza el limite superiro de este rango, el cual es el 7, así que solo genera números dentro de 2 y 6. 
            for (int i = 0; i < cantidad; i++)
            {
                //se genera una placa aleatoria.
                string placa = MostrarPlaca(random);
                //selecciona un vehiculo aleatorio con las características ya definidas. 
                Vehiculo vehiculo = new Vehiculo(marcas[random.Next(marcas.Length)], Colores[random.Next(Colores.Length)], placa, tipos[random.Next(tipos.Length)], random.Next(6, 20));//en esta parte una hora aleatoria de su ingreso. 

                bool AsignarTipos = false;

                //Busca espacio disponible del tipo de vehiculo
                for (int f = 0; f < parqueo.pisos && !AsignarTipos; f++)
                {
                    for (int c = 0; c < parqueo.espacios && !AsignarTipos; c++)
                    {
                        //Solo asigna si esta vacío y si coincide con el tipo
                        if (parqueo.parqueo[f, c]== null && parqueo.tipos[f, c] == vehiculo.Tipo )
                        {
                            string codigo = $"{(char)('A' + f)}{c + 1}";
                            AsignarTipos = true; 
                        
                            //Intentará ocupar el espacio 
                            if (parqueo.OcuparEspacio(codigo, vehiculo))
                            {
                                Console.WriteLine($"Vehículo {placa} de tipo {vehiculo.Tipo} ingresado en el espacio {codigo}.");
                                AsignarTipos = true; 
                            }   
                        }
                    }
                }
                if (!AsignarTipos)
                {
                    //si no encuentra espacio 
                    Console.WriteLine($"No se encontró espacio para el vehículo {placa}");
                }
            }
            parqueo.MostrarMapaCompletoYDisponibles();
        }

        //Método para generar 6 caracteres de forma aleatoria 
        static string MostrarPlaca(Random rand)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] placa = new char[6];
            for (int i = 0; i < 6; i++)
            {
                placa[i] = chars[random.Next(chars.Length)];
            }
            return new string(placa);
        }
        //Para buscar un vehículo que ya fue registrado
        static void BuscarVehiculo()
        {
            Console.WriteLine("Ingrese la placa de su vehículo: ");
            string placa = Console.ReadLine();
            //Para validar si la placa no esta vacia o es nula
            if (string.IsNullOrWhiteSpace(placa))
            {
                Console.WriteLine("Placa inválida. Intentelo nuevamente.");
                return; 
            }
            //Busca la ubicación dentro de la matriz de parqueo
            string ubicación = parqueo.BuscarVehiculo(placa);

            if (ubicación !=null)
            {
                Console.WriteLine($"Vehículo con placa {placa} está ubicado en {ubicación}.");
                //Busca en el arreglo para dar la información sobre el vehiculo 
                for (int i = 0; i < parqueo.pisos; i++)
                {
                    for (int j = 0; j < parqueo.espacios; j++)
                    {
                        Vehiculo vehiculo = parqueo.parqueo[i,j];
                        if (vehiculo != null && vehiculo.Placa == placa)
                        {
                            Console.WriteLine($"Información del vehículo: ");
                            Console.WriteLine($"Marca: {vehiculo.Marca}");
                            Console.WriteLine($"Color: {vehiculo.Color}");
                            Console.WriteLine($"Tipo de vehículo: {vehiculo.Tipo}");
                            Console.WriteLine($"Hora de entrada: {vehiculo.HoraEntrada}");
                            return; 
                        }
                    }
                }
            }
            else 
            {
                //Si no encuentra nada 
                Console.WriteLine("No se ha encontrado el vehículo");
            }
            parqueo.MostrarMapaCompletoYDisponibles();
        }
        
        //Método para retirar un vehiculo anteriormente registrado
        static void RetirarVehiculo()
        {
            Console.WriteLine("Ingrese el código del parqueo: ");
            string codigo = Console.ReadLine();
            //Intenta retirar el vehiculo según el codigo dado 
            Vehiculo vehiculo = parqueo.RetirarVehiculo(codigo);
            if (vehiculo != null)
            {
                Random rand = new Random();
                //calcula las horas que permanecio en el parqueo inicia desde 0 horas hasta las 24 horas 
                int horas = rand.Next(0, 24 - vehiculo.HoraEntrada + 1); //+1 para poder permitir incluir el limite superior 
                //llama al metodo CalcularPago y calcula segúna las horas que permaneció 
                double pago = CalcularPago(horas);
                Console.WriteLine($"Horas: {horas}, Total a pagar: Q {pago}");
                Console.WriteLine("¿Cuál de las siguientes opciones será tu método de pago (efectivo/tarjeta/sticker): ");
                string metodo = Console.ReadLine();

                switch (metodo)
                {
                    case "efectivo":
                        Console.WriteLine("Ingrese monto entregado: ");
                        double pagado = Convert.ToDouble(Console.ReadLine());
                        if (pagado >= pago)
                        {
                            double vuelto = pagado - pago;
                            Console.WriteLine($"Su vuelto es: {vuelto}");
                        }
                        else
                        {
                            Console.WriteLine("El monto es insuficiente para efectuar el pago.");
                        }
                        break;

                    case "tarjeta":
                        Console.WriteLine("Procesando su pago con tarjeta.....");
                        Console.WriteLine("Pago realizado, ¡Ten un buen día!");
                        break;
                    case "sticker":
                        Console.WriteLine("Realizando su pago con sticker recargable.");
                        Console.WriteLine("Pago realizado, ¡Ten un buen día!");
                        break;
                } 
            }
            
        }

        static double CalcularPago(int horas)
        {
            if (horas <= 1) //cuando es 0 o 1 
            {
                return 0;
            }
            else if (horas <= 4) //cuando el valor es mayor a 1 y menor o igual a 4
            {
                return 15;
            }
            else if (horas <= 7)// cuando el valor es mayor a 4 y menor o igual a 7
            {
                return 45;
            }
            else if (horas <= 12) //valor mayor a 7 y menor o igual a 12 
            {
                return 60; 
            }
            else if (horas > 12 && horas <= 24) //Valor mayor a 12 y menor o igual a 24
            {
                return 150;
            }
            else 
            {
                return 0; 
            }
        }
    }

