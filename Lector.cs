using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class Lector
    {
        private string nombre;
        private string apellido;
        private int dni;
        private List<Libro> librosPrestados;

        public Lector(string nombre, string apellido, int dni)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            librosPrestados = new List<Libro>();
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        public int Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public void AgregarALibrosPrestados(Libro libro)
        {
            if (librosPrestados.Count < 3)
            {
                librosPrestados.Add(libro);
            }
            else
            {
                throw new InvalidOperationException("No se pueden prestar más de 3 libros.");
            }
        }
       
        public List<Libro> ObtenerLibrosPrestados()
        {
            return librosPrestados;
        }
        public void MostrarLibrosPrestadosPorLector()
        {
            Console.WriteLine($"\n> Libros prestados a {nombre} {apellido}:");
            foreach (var libro in librosPrestados)
            {
                Console.WriteLine(libro.ToString());
            }
        }
        public override string ToString()
        {
            return $"Lector - Nombre: {nombre}, Apellido: {apellido}, DNI: {dni}";
        }

    }
}
