    public class Cilindro
    {
        // Propiedades 
        public decimal Radio { get; set; }
        public decimal Altura { get; set; }

        // Constructor
        public Cilindro(decimal radio, decimal altura)
        {
            Radio = radio;
            Altura = altura;
        }

        // Método para calcular el volumen
        public decimal CalcularVolumen()
        {
            // Usar la constante PI con mayor precisión para decimal
            decimal pi = 3.1415926535897932384626433832795m;
            return pi * Radio * Radio * Altura;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LABORATORIO - CÁLCULO DE VOLUMEN DE CILINDRO (USANDO DECIMAL)");
            Console.WriteLine("------------------------------------------------------------");

            try
            {
                // Solicitar datos al usuario
                Console.Write("Ingrese el radio del cilindro: ");
                decimal radio = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Ingrese la altura del cilindro: ");
                decimal altura = Convert.ToDecimal(Console.ReadLine());

                // Verificar que los valores que ingrese el usuario sean positivos
                if (radio <= 0 || altura <= 0)
                {
                    Console.WriteLine("Error: El radio y la altura deben ser valores positivos.");
                    return;
                }

                // instancia del cilindro
                Cilindro miCilindro = new Cilindro(radio, altura);

                // Calcular y mostrar el volumen
                decimal volumen = miCilindro.CalcularVolumen();
                Console.WriteLine($"\nEl volumen del cilindro es: {volumen:F2}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Por favor ingrese valores numéricos válidos.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: El valor ingresado es demasiado grande o pequeño.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }