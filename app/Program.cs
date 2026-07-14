using System;
using app.libro;
using app.models;

var libros = new List<Libro>();

Console.WriteLine("-------------------------------------------------------");
Console.WriteLine("         Sistema de Gestión de Biblioteca");
Console.WriteLine("-------------------------------------------------------");

Menu();


void Menu()
{
    int opcion;
    bool bucle = true;

    while (bucle)
    {
        

        Console.WriteLine("\nBienvenidos al sistema de gestion de biblioteca\n");
        Console.WriteLine("Comandos:");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Agregar");
        Console.WriteLine("3. Buscar");
        Console.WriteLine("4. Prestar");
        Console.WriteLine("5. Salir\n");

        try
        {
            opcion = Convert.ToInt32(Console.ReadLine());
        } 
        catch
        {
            Console.WriteLine("\nElija una opcion numerica\n");
            continue;
        }

        switch (opcion)
        {
            case 1: Listar(); break;
            case 2:Agregar(); break;
            case 3:Buscar(); break;
            case 4:Prestar(); break;
            case 5: Console.WriteLine("\nSaliendo del sistema...\n"); bucle = false; break;
            default: Console.WriteLine("\nSeleccione una opcion valida.\n"); break;
        }
    }
}

void Listar()
{
    if(libros.Count == 0)
    {
        Console.WriteLine("\nNo hay libros aun.\n");
        return;
    }

    foreach (var l in libros)
    {
        Console.WriteLine($"\nTitulo: {l.Titulo} | Autor: {l.Autor} | Precio: ${l.Precio:F2} | Cantidad: {l.Cantidad} | Total: {l.Valor_inventario:F2}");

    }
    Console.WriteLine($"\nTotal: {libros.Count} libros(s)\n");
}

void Agregar()
{
    Console.WriteLine("--------AGREGAR LIBRO---------\n");

    Console.Write("Titulo: ");
    string titulo = Console.ReadLine() ?? "";
    

    Console.Write("Autor: ");
    string autor = Console.ReadLine() ?? "";
    

    Console.Write("Precio: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
    {
        Console.WriteLine("\nPrecio invalido.\n");
        return;
    }

    Console.Write("Cantidad: ");
    if (!int.TryParse(Console.ReadLine(), out int cantidad))
    {
        Console.WriteLine("\nCantidad invalida");
        return;
    }

    Console.WriteLine("\nCategorias: Programacion, Novela, Historia, Ciencia, Infantil, Otros");
    Console.Write("Categoria: ");
    string cate = Console.ReadLine() ?? "Otros";

    if(!Enum.TryParse<Categoria>(cate, true, out var categoria))
    {
        categoria = Categoria.Otros;
    }

    try
    {
        var libro = LibroFactory.Crear(titulo, autor, precio, cantidad, categoria);
        libros.Add(libro);
        Console.WriteLine($"\nEl libro `{libro.Titulo}` ha sido agregado con el ID `{libro.Id}`\n");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {ex}");
    }
}

void Buscar()
{
    Console.WriteLine("-------BUSQUEDA POR TITULO------\n");
    Console.Write("Ingrese el texto: ");
    string Name = Console.ReadLine() ?? "";
    Console.WriteLine("");

    var encontrado = libros.Where(l => l.Titulo.Contains(Name, StringComparison.OrdinalIgnoreCase)).ToList();

    if (encontrado.Count == 0)
    {
        Console.WriteLine("\nNo se encontraron.");
        return;
    }

    foreach (var l in encontrado)
    {
        Console.WriteLine($"Titulo: {l.Titulo} | Autor: {l.Autor} | Precio: ${l.Precio:F2} | Cantidad: {l.Cantidad}");
        Console.WriteLine($"Libros encontrados: {encontrado.Count()}");
    }
}

void Prestar()
{
    Console.WriteLine("-----PRESTAR LIBROS-----\n");
    Console.Write("Que libro desea prestar? (ID): ");
    if(!int.TryParse(Console.ReadLine(), out int Prestado))
    {
        Console.WriteLine("ID invalida");
        return;
    }
    Console.WriteLine("");

    var Encontrado_prestar = libros.Where(p => p.Id == Prestado).ToList();
    
    if (Encontrado_prestar.Count() == 0)
    {
        Console.WriteLine("ID no encontrada");
        return;
    }

    Console.Write("Ingrese la cantidad a prestar: ");
    if(!int.TryParse(Console.ReadLine(), out int Cant_pres))
    {
        Console.WriteLine("Ingrese un numero.");
        return;
    }
    Console.WriteLine("");


    int temp = 0;
    foreach (var l in Encontrado_prestar)
    {
        if (Cant_pres > l.Cantidad)
        {
            Console.WriteLine("Error: Cantidad insuficiente");
        }

        l.Cantidad -= Cant_pres;

        if (temp == 0)
        {
            Console.WriteLine($"Se prestaron `{Cant_pres}` del libro `{l.Titulo}`");
            Console.WriteLine($"Quedan: {l.Cantidad}");
        }
        temp++;
    }
}


