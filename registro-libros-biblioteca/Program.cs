using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BibliotecaLibros
{
    /// <summary>
    /// Clase que representa un libro de la biblioteca
    /// </summary>
    public class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public int Anio { get; set; }
        public int EjemplaresDisponibles { get; set; }

        public override string ToString()
        {
            return $"ISBN: {ISBN} | Título: {Titulo} | Autor: {Autor} | Género: {Genero} | Año: {Anio} | Disponibles: {EjemplaresDisponibles}";
        }
    }

    class Program
    {
        // ==================== ESTRUCTURAS DE DATOS ====================

        // Diccionario (Mapa): ISBN -> Libro
        static Dictionary<string, Libro> librosPorISBN = new Dictionary<string, Libro>();

        // Conjuntos (Sets): garantizan unicidad
        static HashSet<string> autoresUnicos = new HashSet<string>();
        static HashSet<string> generosUnicos = new HashSet<string>();

        // Diccionario auxiliar: Género -> cantidad de libros
        static Dictionary<string, int> contadorPorGenero = new Dictionary<string, int>();

        // Diccionario: Autor -> lista de libros
        static Dictionary<string, List<Libro>> librosPorAutor = new Dictionary<string, List<Libro>>();

        static void Main(string[] args)
        {
            // Cargar algunos libros de ejemplo
            CargarDatosEjemplo();

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("══════════════════════════════════════════════════════");
                Console.WriteLine("     SISTEMA DE REGISTRO DE LIBROS - BIBLIOTECA");
                Console.WriteLine("══════════════════════════════════════════════════════");
                Console.WriteLine("1. Registrar nuevo libro");
                Console.WriteLine("2. Buscar libro por ISBN");
                Console.WriteLine("3. Listar todos los libros");
                Console.WriteLine("4. Listar libros por género");
                Console.WriteLine("5. Mostrar autores únicos");
                Console.WriteLine("6. Mostrar géneros únicos");
                Console.WriteLine("7. Contar libros por género");
                Console.WriteLine("8. Buscar libros por autor");
                Console.WriteLine("9. Análisis de tiempo de ejecución");
                Console.WriteLine("0. Salir");
                Console.WriteLine("══════════════════════════════════════════════════════");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarLibro();
                        break;
                    case "2":
                        BuscarPorISBN();
                        break;
                    case "3":
                        ListarTodos();
                        break;
                    case "4":
                        ListarPorGenero();
                        break;
                    case "5":
                        MostrarAutoresUnicos();
                        break;
                    case "6":
                        MostrarGenerosUnicos();
                        break;
                    case "7":
                        ContarPorGenero();
                        break;
                    case "8":
                        BuscarPorAutor();
                        break;
                    case "9":
                        AnalizarTiempoEjecucion();
                        break;
                    case "0":
                        salir = true;
                        Console.WriteLine("\n¡Gracias por usar el sistema!");
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Intente nuevamente.");
                        Pausar();
                        break;
                }
            }
        }

        /// <summary>
        /// Carga libros de ejemplo para pruebas
        /// </summary>
        static void CargarDatosEjemplo()
        {
            RegistrarLibroInterno("978-0132350884", "Clean Code", "Robert C. Martin", "Programación", 2008, 5);
            RegistrarLibroInterno("978-0201633610", "Design Patterns", "Erich Gamma", "Programación", 1994, 3);
            RegistrarLibroInterno("978-0134685991", "Effective Java", "Joshua Bloch", "Programación", 2018, 4);
            RegistrarLibroInterno("978-1492056812", "Fluent Python", "Luciano Ramalho", "Programación", 2022, 2);
            RegistrarLibroInterno("978-0062316097", "Sapiens", "Yuval Noah Harari", "Historia", 2015, 6);
            RegistrarLibroInterno("978-0307887894", "The Lean Startup", "Eric Ries", "Negocios", 2011, 3);
            RegistrarLibroInterno("978-0143127550", "The Alchemist", "Paulo Coelho", "Ficción", 1988, 8);
        }

        /// <summary>
        /// Permite al usuario registrar un nuevo libro
        /// </summary>
        static void RegistrarLibro()
        {
            Console.WriteLine("\n--- REGISTRAR NUEVO LIBRO ---");

            Console.Write("ISBN: ");
            string isbn = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("El ISBN no puede estar vacío.");
                Pausar();
                return;
            }

            if (librosPorISBN.ContainsKey(isbn))
            {
                Console.WriteLine("Ya existe un libro registrado con ese ISBN.");
                Pausar();
                return;
            }

            Console.Write("Título: ");
            string titulo = Console.ReadLine()?.Trim();

            Console.Write("Autor: ");
            string autor = Console.ReadLine()?.Trim();

            Console.Write("Género: ");
            string genero = Console.ReadLine()?.Trim();

            Console.Write("Año de publicación: ");
            int.TryParse(Console.ReadLine(), out int anio);

            Console.Write("Ejemplares disponibles: ");
            int.TryParse(Console.ReadLine(), out int ejemplares);

            RegistrarLibroInterno(isbn, titulo, autor, genero, anio, ejemplares);

            Console.WriteLine("\n✓ Libro registrado exitosamente.");
            Pausar();
        }

        /// <summary>
        /// Método interno que registra el libro en todas las estructuras de datos
        /// </summary>
        static void RegistrarLibroInterno(string isbn, string titulo, string autor, string genero, int anio, int ejemplares)
        {
            var libro = new Libro
            {
                ISBN = isbn,
                Titulo = titulo,
                Autor = autor,
                Genero = genero,
                Anio = anio,
                EjemplaresDisponibles = ejemplares
            };

            // 1. Diccionario principal (Mapa)
            librosPorISBN[isbn] = libro;

            // 2. Conjuntos (unicidad)
            autoresUnicos.Add(autor);
            generosUnicos.Add(genero);

            // 3. Contador por género
            if (contadorPorGenero.ContainsKey(genero))
                contadorPorGenero[genero]++;
            else
                contadorPorGenero[genero] = 1;

            // 4. Agrupación por autor
            if (!librosPorAutor.ContainsKey(autor))
                librosPorAutor[autor] = new List<Libro>();

            librosPorAutor[autor].Add(libro);
        }

        static void BuscarPorISBN()
        {
            Console.Write("\nIngrese el ISBN a buscar: ");
            string isbn = Console.ReadLine()?.Trim();

            if (librosPorISBN.TryGetValue(isbn, out Libro libro))
            {
                Console.WriteLine("\nLibro encontrado:");
                Console.WriteLine(libro);
            }
            else
            {
                Console.WriteLine("\nNo se encontró ningún libro con ese ISBN.");
            }

            Pausar();
        }

        static void ListarTodos()
        {
            Console.WriteLine("\n--- LISTADO DE TODOS LOS LIBROS ---");

            if (librosPorISBN.Count == 0)
            {
                Console.WriteLine("No hay libros registrados.");
            }
            else
            {
                foreach (var libro in librosPorISBN.Values)
                {
                    Console.WriteLine(libro);
                }
                Console.WriteLine($"\nTotal de libros registrados: {librosPorISBN.Count}");
            }

            Pausar();
        }

        static void ListarPorGenero()
        {
            Console.Write("\nIngrese el género a buscar: ");
            string genero = Console.ReadLine()?.Trim();

            var librosFiltrados = librosPorISBN.Values
                .Where(l => l.Genero.Equals(genero, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (librosFiltrados.Count == 0)
            {
                Console.WriteLine($"\nNo se encontraron libros del género '{genero}'.");
            }
            else
            {
                Console.WriteLine($"\nLibros del género '{genero}':");
                foreach (var libro in librosFiltrados)
                {
                    Console.WriteLine(libro);
                }
            }

            Pausar();
        }

        static void MostrarAutoresUnicos()
        {
            Console.WriteLine("\n--- AUTORES ÚNICOS (HashSet) ---");

            if (autoresUnicos.Count == 0)
            {
                Console.WriteLine("No hay autores registrados.");
            }
            else
            {
                foreach (var autor in autoresUnicos.OrderBy(a => a))
                {
                    Console.WriteLine($"- {autor}");
                }
                Console.WriteLine($"\nTotal de autores únicos: {autoresUnicos.Count}");
            }

            Pausar();
        }

        static void MostrarGenerosUnicos()
        {
            Console.WriteLine("\n--- GÉNEROS ÚNICOS (HashSet) ---");

            if (generosUnicos.Count == 0)
            {
                Console.WriteLine("No hay géneros registrados.");
            }
            else
            {
                foreach (var genero in generosUnicos.OrderBy(g => g))
                {
                    Console.WriteLine($"- {genero}");
                }
                Console.WriteLine($"\nTotal de géneros únicos: {generosUnicos.Count}");
            }

            Pausar();
        }

        static void ContarPorGenero()
        {
            Console.WriteLine("\n--- CANTIDAD DE LIBROS POR GÉNERO ---");

            if (contadorPorGenero.Count == 0)
            {
                Console.WriteLine("No hay datos disponibles.");
            }
            else
            {
                foreach (var item in contadorPorGenero.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"{item.Key}: {item.Value} libro(s)");
                }
            }

            Pausar();
        }

        static void BuscarPorAutor()
        {
            Console.Write("\nIngrese el nombre del autor: ");
            string autor = Console.ReadLine()?.Trim();

            if (librosPorAutor.TryGetValue(autor, out List<Libro> listaLibros))
            {
                Console.WriteLine($"\nLibros de {autor}:");
                foreach (var libro in listaLibros)
                {
                    Console.WriteLine(libro);
                }
            }
            else
            {
                Console.WriteLine($"\nNo se encontraron libros del autor '{autor}'.");
            }

            Pausar();
        }

        /// <summary>
        /// Analiza el tiempo de ejecución de operaciones con Dictionary y HashSet
        /// </summary>
        static void AnalizarTiempoEjecucion()
        {
            Console.WriteLine("\n--- ANÁLISIS DE TIEMPO DE EJECUCIÓN ---");
            Console.WriteLine("Se realizarán 10.000 operaciones de búsqueda...\n");

            // Prueba con Dictionary (búsqueda por ISBN)
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10000; i++)
            {
                librosPorISBN.ContainsKey("978-0132350884");
            }
            sw.Stop();
            Console.WriteLine($"Dictionary (búsqueda por ISBN): {sw.ElapsedMilliseconds} ms");

            // Prueba con HashSet (verificación de autor)
            sw.Restart();
            for (int i = 0; i < 10000; i++)
            {
                autoresUnicos.Contains("Robert C. Martin");
            }
            sw.Stop();
            Console.WriteLine($"HashSet (verificación de autor): {sw.ElapsedMilliseconds} ms");

            Console.WriteLine("\nConclusión: Ambas estructuras ofrecen un rendimiento excelente (complejidad promedio O(1)).");
            Pausar();
        }

        static void Pausar()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}