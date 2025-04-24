using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class Biblioteca
    {
        private List<Libro> libros;
        private List<Lector> lectores;

        public Biblioteca()
        {
            libros = new List<Libro>();
            lectores = new List<Lector>();
        }
        public List<Libro> Libros
        {
            get { return libros; }
            set { libros = value; }
        }
        public List<Lector> Lectores
        {
            get { return lectores; }
            set { lectores = value; }
        }

        private Libro? buscarLibro(string titulo)
        {
            Libro? libroBuscado = null;
            int i = 0;
            while (i < libros.Count && !libros[i].Titulo.Equals(titulo))
                i++;
            if (i != libros.Count)
                libroBuscado = libros[i];
            return libroBuscado;
        }

        public bool AgregarLibro(Libro libro)
        {
            if (libro == null)
            {
                Console.WriteLine("\nError: El libro no puede ser nulo");
                return false;
            }

            // convertimos las propiedades a mayúsculas para evitar posibles errores
            libro.Titulo = libro.Titulo.ToUpperInvariant();
            libro.Autor = libro.Autor.ToUpperInvariant();
            libro.Editorial = libro.Editorial.ToUpperInvariant();

            if (libros.Any(l => l.Titulo == libro.Titulo && l.Autor == libro.Autor))
            {
                Console.WriteLine($"\nError: El libro \"{libro.Titulo}\" ya está registrado en la biblioteca");
                return false;
            }

            libros.Add(libro);
            Console.WriteLine($"\nEl libro \"{libro.Titulo}\" fue agregado con éxito");
            return true;
        }

        public void AgregarLibroDesdeConsola()
        {
            string titulo, autor, editorial;
            // Validar título
            do
            {
                Console.WriteLine("\nIngrese el título del libro:");
                titulo = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(titulo))
                {
                    Console.WriteLine("\nError: El título no puede estar vacío");
                }
            } while (string.IsNullOrWhiteSpace(titulo));
            // Validar autor
            do
            {
                Console.WriteLine("Ingrese el autor del libro:");
                autor = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(autor))
                {
                    Console.WriteLine("\nError: El autor no puede estar vacío");
                }
            } while (string.IsNullOrWhiteSpace(autor));
            // Validar editorial
            do
            {
                Console.WriteLine("Ingrese la editorial del libro:");
                editorial = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(editorial))
                {
                    Console.WriteLine("\nError: La editorial no puede estar vacía");
                }
            } while (string.IsNullOrWhiteSpace(editorial));

            // Intentar agregar el libro
            try
            {
                AgregarLibro(new Libro(titulo, autor, editorial, true));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al agregar el libro: {ex.Message}");
            }
        }

        public bool eliminarLibro(string titulo)
        {
            // Buscar el libro por título
            Libro? libro = libros.FirstOrDefault(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
            if (libro == null)
            {
                Console.WriteLine($"\nEl libro \"{titulo}\" no se encuentra en la biblioteca");
                return false;
            }
            
            if (!libro.Estado)// Verificar si el libro está prestado
            {
                Console.WriteLine($"\nEl libro \"{titulo}\" no se puede eliminar porque está prestado");
                return false;
            }
            
            libros.Remove(libro);// Eliminar el libro
            Console.WriteLine($"\nEl libro \"{titulo}\" fue eliminado correctamente");
            return true;
        }

        public void AltaLector(Lector lector)
        {
            if (lector == null)
            {
                throw new ArgumentNullException(nameof(lector), "\nEl lector no puede ser nulo");
            }

            // convertimos las propiedades a mayúsculas para evitar posibles errores
            lector.Nombre = lector.Nombre.ToUpperInvariant();
            lector.Apellido = lector.Apellido.ToUpperInvariant();

            if (lectores.Any(l => l.Dni == lector.Dni))
            {
                throw new InvalidOperationException("\nEl lector ya se encuentra registrado");
            }

            lectores.Add(lector);
        }
        public void AltaLectorDesdeConsola()
        {
            string nombre, apellido;
            int dni;
            // Validar nombre
            do
            {
                Console.WriteLine("\nIngrese el nombre del lector:");
                nombre = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.WriteLine("\nError: El nombre no puede estar vacío.");
                }
            } while (string.IsNullOrWhiteSpace(nombre));
            // Validar apellido
            do
            {
                Console.WriteLine("Ingrese el apellido del lector:");
                apellido = Console.ReadLine()?.ToUpperInvariant() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(apellido))
                {
                    Console.WriteLine("\nError: El apellido no puede estar vacío.");
                }
            } while (string.IsNullOrWhiteSpace(apellido));
            // Validar DNI
            do
            {
                Console.WriteLine("Ingrese el DNI del lector:");
                if (!int.TryParse(Console.ReadLine(), out dni) || dni <= 0)
                {
                    Console.WriteLine("\nError: El DNI debe ser un número válido y mayor a 0.");
                    dni = 0; // Reiniciar el valor para que el bucle continúe
                }
            } while (dni <= 0);

            // Intentar agregar el lector
            try
            {
                AltaLector(new Lector(nombre, apellido, dni));
                Console.WriteLine("\nLector agregado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al agregar el lector: {ex.Message}");
            }
        }

        //Desarrollar el método prestarLibro de la clase Biblioteca la cual recibe por parámetro el título de un libro
        //y el dni del lector que lo solicita y retorna un string con los posibles valores
        public string PrestarLibro(string titulo, int dni)
        {         
            Libro? libro = libros.FirstOrDefault(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));// Buscar el libro por título
            if (libro == null)
            {
                return $"\nLIBRO INEXISTENTE \"{titulo}\"";
            }
            
            if (!libro.Estado)// Verificar si el libro ya está prestado
            {
                return $"\nEl libro \"{titulo}\" NO ESTA DISPONIBLE";
            }

            Lector? lector = lectores.FirstOrDefault(l => l.Dni == dni); // Buscar el lector por DNI
            if (lector == null)
            {
                return $"\nEl DNI {dni} es de un LECTOR INEXISTENTE";
            }
            
            if (lector.ObtenerLibrosPrestados().Count >= 3)// Verificar si el lector ya tiene el máximo de libros prestados (3 en este caso)
            {
                return $"\nTOPE DE PRESTAMO ALCANZADO para el lector {lector.Nombre} {lector.Apellido}";
            }

            // Prestar el libro
            libro.Estado = false; // Cambiar el estado del libro a "prestado"
            lector.AgregarALibrosPrestados(libro); // Agregar el libro a la lista de libros prestados del lector
            //libros.Remove(libro); // Quitar el libro de la lista de libros disponibles en la biblioteca

            return $"\nPRESTAMO EXITOSO - Libro \"{titulo}\" prestado con éxito a {lector.Nombre} {lector.Apellido}";
        }

        public void devolverLibroPrestado(string titulo, int dni)
        {
            
            Lector? lector = lectores.FirstOrDefault(l => l.Dni == dni);// Buscar el lector por DNI
            if (lector == null)
            {
                Console.WriteLine($"\nEl lector con DNI {dni} no está registrado en la biblioteca");
                return;
            }

            // Buscar el libro en la lista de libros prestados del lector
            Libro? libro = lector.ObtenerLibrosPrestados().FirstOrDefault(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
            if (libro == null)
            {
                Console.WriteLine($"\nEl libro \"{titulo}\" no está prestado al lector con DNI {dni}");
                return;
            }
            
            libro.Estado = true;// Cambiar el estado del libro a disponible
            lector.ObtenerLibrosPrestados().Remove(libro);// Eliminar el libro de la lista de libros prestados del lector
            //libros.Add(libro); Agregar el libro a la lista de libros disponibles en la biblioteca

            Console.WriteLine($"\nEl libro \"{titulo}\" fue devuelto correctamente por el lector {lector.Nombre} {lector.Apellido}");
        }

        public void listarLibros()
        {
            if (libros.Count == 0)
            {
                Console.WriteLine("\nNo hay libros registrados en la biblioteca");
                return;
            }

            Console.WriteLine("\n> Listado de libros:");
            foreach (var libro in libros)
            {
                Console.WriteLine(libro.ToString());
            }
        }

        public void listarLectores()
        {
            if (lectores.Count == 0)
            {
                Console.WriteLine("\nNo hay lectores registrados en la biblioteca");
                return;
            }

            Console.WriteLine("\n> Listado de lectores:");
            foreach (var lector in lectores)
            {
                Console.WriteLine(lector.ToString());
            }
        }

        public void MostrarLibrosPrestadosPorLector()
        {
            foreach (var lector in lectores)
            {
                lector.MostrarLibrosPrestadosPorLector();
            }
        }
       
        
    }
}
