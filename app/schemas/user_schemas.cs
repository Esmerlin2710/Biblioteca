namespace app.libro;

public class Libro
{
    private string _titulo = "";
    private string _autor = "";
    private decimal _precio;
    private int _cantidad;

    public int Id { get; set; }
    
    public string Titulo
    {
        
        get => _titulo;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El titulo no puede estar vacio");
            }
            _titulo = value;
        }
    }

    public string Autor
    {
        get => _autor;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("El autor no puede estar vacio");
            }
            _autor = value;
        }  
    }

    public decimal Precio
    {
        get => _precio;
        set
        {
            if (value < 0)
                throw new ArgumentException("El precio no puede ser negativo");
            _precio = value;
            
        }
    }

    public int Cantidad
    {
        get => _cantidad;
        set
        {
            if (value < 0)
                throw new ArgumentException("La cantidad no puede ser negativa");
            _cantidad = value;
        }
    }


    public Categoria Categoria { get; set; } = Categoria.Otros;
    public Estado Estado { get; set; } = Estado.Disponible;
    public DateTime Fecha_registro {get; set;} = DateTime.Now;



    public decimal Valor_inventario => _precio * _cantidad;
    
}

public enum Categoria
{
    Programacion,
    Novela,
    Historia,
    Ciencia,
    Infantil,
    Otros
}

public enum Estado
{
    Disponible,
    Prestado,
    Agotado
}

public record Autor(
    int Id,
    string Nombre,
    string Nacionalidad
);

