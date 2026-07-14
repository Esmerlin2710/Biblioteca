namespace app.models;
using app.libro;

public static class LibroFactory
{
    private static int _nextId = 1;

    public static Libro Crear(
        string titulo,
        string autor,
        decimal precio,
        int cantidad,
        Categoria categoria = Categoria.Otros)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Debe poner un titulo.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentException("Debe poner un autor.", nameof(autor));
        if (precio < 0)
            throw new ArgumentException("El precio no puede ser negativo.", nameof(precio));
        if (cantidad < 0) 
            throw new ArgumentException("La cantidad no puede ser negativa.", nameof(cantidad));

        return new Libro
        {
            Id = _nextId++,
            Titulo = titulo,
            Autor = autor,
            Precio = precio,
            Cantidad = cantidad,
            Categoria = categoria,
            Estado = Estado.Disponible,

        };
    }   
}