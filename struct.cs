Medicamento[] medicamentos = new Medicamento[6];

medicamentos[0] = new Medicamento{codigo = "000", nombre = "ASPIRINA", inventario = 50, precio = 2};

medicamentos[1] = new Medicamento{codigo= "001", nombre= "DICLOFENACO", inventario = 45, precio = 5};
medicamentos[2] = new Medicamento{codigo = "002", nombre = "PASIFLORA", inventario = 12, precio = 3};
medicamentos[3] = new Medicamento{codigo = "003", nombre = "PARACETAMOL", inventario = 20, precio= 7};
medicamentos[4] = new Medicamento{codigo = "004", nombre = "SUKROL", inventario = 10, precio = 12};
medicamentos[5] = new Medicamento{codigo = "005", nombre = "GRIPETIN", inventario = 8, precio = 12};

mostrar();

void mostrar ()
{
    for( int i = 0; i < medicamentos.Length; i++)
    {
        Console.WriteLine ($"Código: {medicamentos [i].codigo}, Nombre: {medicamentos[i].nombre}, Inventario: {medicamentos[i].inventario}, Precio: {medicamentos[i].precio} ");
    }
}


struct Medicamento
{
    public string codigo;
    public string nombre;
    public int inventario;
    public int precio;
}

