using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class Libro
    {
        private string titulo;
        private string autor;
        private string editorial;
        private bool estado; // true no prestado(disponible) | false prestado(no disponible) (con esto no hace falta SACAR el libro de la lista de biblioteca)

        public Libro(string titulo, string autor, string editorial, bool estado)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.editorial = editorial;
            this.estado = estado;
        }
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }
        public string Editorial
        {
            get { return editorial; }
            set { editorial = value; }
        }
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        public override string ToString()
        {
            return $"Libro - Título: {titulo}, Autor: {autor}, Editorial: {editorial}";
        }
    }
}
