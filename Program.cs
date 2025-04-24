using System;
using System.Runtime.CompilerServices;

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            // agregamos lectores a la biblioteca}
            //Lector lector1 = new Lector("Juan", "Pérez", 42513654);
            //biblioteca.AltaLector(lector1);
            biblioteca.AltaLector(new Lector("Juan", "Pérez", 42513654));
            biblioteca.AltaLector(new Lector("Ana", "Gómez", 35123641));
            biblioteca.AltaLector(new Lector("Pedro", "López", 41262478));

            // agregamos libros a la biblioteca
            Libro libro1 = new Libro("Cien años de soledad", "Gabriel García Márquez", "Editorial X", true);
            Libro libro2 = new Libro("El amor en los tiempos del cólera", "Gabriel García Márquez", "Editorial Y", true);
            Libro libro3 = new Libro("El túnel", "Ernesto Sabato", "Editorial Z", true);

            // Intentamos agregar los libros y verificamos el resultado
            if (!biblioteca.AgregarLibro(libro1))
                Console.WriteLine("\nNo se pudo agregar el libro: Cien años de soledad");

            if (!biblioteca.AgregarLibro(libro1)) // Este intentará agregar un duplicado
                Console.WriteLine("\nNo se pudo agregar el libro: Cien años de soledad (duplicado)");

            if (!biblioteca.AgregarLibro(libro2))
                Console.WriteLine("\nNo se pudo agregar el libro: El amor en los tiempos del cólera");

            if (!biblioteca.AgregarLibro(libro3))
                Console.WriteLine("\nNo se pudo agregar el libro: El túnel");

            //otra forma mas simplificada
            var libros = new List<Libro>
            {
                new Libro("1984", "George Orwell", "Editorial A", true),
                new Libro("Don Quijote de la Mancha", "Miguel de Cervantes", "Editorial B", true),
                new Libro("La sombra del viento", "Carlos Ruiz Zafón", "Editorial C", true),
                new Libro("La sombra del viento", "Carlos Ruiz Zafón", "Editorial C", true) // duplicado

            };

            // intentamos agregar cada libro y verificamos el resultado
            foreach (var libro in libros)
            {
                if (!biblioteca.AgregarLibro(libro))
                {
                    Console.WriteLine($"\nNo se pudo agregar el libro: {libro.Titulo}.");
                }
            }

            //mostramos los libros y lectores hasta el momento
            biblioteca.listarLibros();
            biblioteca.listarLectores();

            // prestar libros - Salida: "PRESTAMO EXITOSO"
            string resultado = biblioteca.PrestarLibro("Cien años de soledad", 42513654);
            Console.WriteLine(resultado);
            resultado = biblioteca.PrestarLibro("1984", 42513654);
            Console.WriteLine(resultado);
            resultado = biblioteca.PrestarLibro("Don Quijote de la Mancha", 42513654);
            Console.WriteLine(resultado);

            // prestar un libro a un lector que ya tiene 3 libros prestados - Salida: "TOPE DE PRESTAMO ALCANZADO"
            resultado = biblioteca.PrestarLibro("La sombra del viento", 42513654);
            Console.WriteLine(resultado);


            //  prestar un libro inexistente - Salida: "LIBRO INEXISTENTE"
            resultado = biblioteca.PrestarLibro("Libro inexistente", 42513654);
            Console.WriteLine(resultado);

            //  prestar un libro a un lector inexistente - Salida: "LECTOR INEXISTENTE"
            resultado = biblioteca.PrestarLibro("El túnel", 99999999);
            Console.WriteLine(resultado);

            //  prestar un libro ya prestado - Salida: "LIBRO NO DISPONIBLE"
            resultado = biblioteca.PrestarLibro("Cien años de soledad", 35123641);
            Console.WriteLine(resultado);

            biblioteca.MostrarLibrosPrestadosPorLector();

            // agregamos un libro y lector desde la consola
            //biblioteca.AgregarLibroDesdeConsola();
            //biblioteca.AltaLectorDesdeConsola();

            // preguntar si quiere agregar libros y la cantidad que desea
            Console.WriteLine("\n¿Desea agregar libros a la biblioteca? (si/no)");
            string? respuestaLibros = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (respuestaLibros == "si")
            {
                Console.WriteLine("\n¿Cuántos libros desea agregar?");
                if (int.TryParse(Console.ReadLine(), out int cantidadLibros) && cantidadLibros > 0)
                {
                    for (int i = 0; i < cantidadLibros; i++)
                    {
                        Console.WriteLine($"\nIngresando libro {i + 1} de {cantidadLibros}:");
                        biblioteca.AgregarLibroDesdeConsola();
                    }
                }
                else
                {
                    Console.WriteLine("\nError: Debe ingresar un número válido mayor a 0 para la cantidad de libros.");
                }
            }

            // preguntar si quiere agregar lectores y la cantidad que desea
            Console.WriteLine("\n¿Desea agregar lectores a la biblioteca? (si/no)");
            string? respuestaLectores = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (respuestaLectores == "si")
            {
                Console.WriteLine("\n¿Cuántos lectores desea agregar?");
                if (int.TryParse(Console.ReadLine(), out int cantidadLectores) && cantidadLectores > 0)
                {
                    for (int i = 0; i < cantidadLectores; i++)
                    {
                        Console.WriteLine($"\nIngresando lector {i + 1} de {cantidadLectores}:");
                        biblioteca.AltaLectorDesdeConsola();
                    }
                }
                else
                {
                    Console.WriteLine("\nError: Debe ingresar un número válido mayor a 0 para la cantidad de lectores.");
                }
            }

            biblioteca.listarLibros();
            biblioteca.listarLectores();

            // eliminar un libro
            biblioteca.eliminarLibro("El túnel");

            // preguntar si desea eliminar un libro
            Console.WriteLine("\n¿Desea eliminar un libro de la biblioteca? (si/no)");
            string? respuestaEliminar = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (respuestaEliminar == "si")
            {
                Console.WriteLine("\nIngrese el título del libro que desea eliminar de la Biblioteca:");
                string? tituloEliminar = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(tituloEliminar))
                {
                    biblioteca.eliminarLibro(tituloEliminar); //ya imprime el mensaje correspondiente
                }
                else
                {
                    Console.WriteLine("\nError: El título no puede estar vacío");
                }
            }

            // devolver libro
            biblioteca.devolverLibroPrestado("Cien años de soledad", 42513654);

            // preguntar si desea devolver un libro
            Console.WriteLine("\n¿Desea devolver un libro prestado? (si/no)");
            string? respuestaDevolver = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (respuestaDevolver == "si")
            {
                string? tituloDevolver;
                do
                {
                    Console.WriteLine("\nIngrese el título del libro que desea devolver:");
                    tituloDevolver = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(tituloDevolver))
                    {
                        Console.WriteLine("\nError: El título no puede estar vacío. Inténtelo de nuevo.");
                    }
                } while (string.IsNullOrWhiteSpace(tituloDevolver)); // Repetir hasta que el título sea válido

                Console.WriteLine("Ingrese el DNI del lector que devuelve el libro:");
                if (int.TryParse(Console.ReadLine(), out int dniDevolver))
                {
                    biblioteca.devolverLibroPrestado(tituloDevolver, dniDevolver);
                }
                else
                {
                    Console.WriteLine("\nError: El DNI debe ser un número válido");
                }
            }

        }

    }
}
