using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCapBibliotecaNodo
    {
        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        
        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        
        private int _Id_Biblioteca;
        public int Id_Biblioteca
        {
            get { return _Id_Biblioteca; }
            set { _Id_Biblioteca = value; }
        }
        
        private int _Id_BiblioNodo;
        public int Id_BiblioNodo
        {
            get { return _Id_BiblioNodo; }
            set { _Id_BiblioNodo = value; }
        }

        private string _BiblioNodo_Nombre;
        public string BiblioNodo_Nombre
        {
            get { return _BiblioNodo_Nombre; }
            set { _BiblioNodo_Nombre = value; }
        }

        private string _Id_BiblioNodo_Padre;
        public string Id_BiblioNodo_Padre
        {
            get { return _Id_BiblioNodo_Padre; }
            set { _Id_BiblioNodo_Padre = value; }
        }

        private string _Id_Recurso;
        public string Id_Recurso
        {
            get { return _Id_Recurso; }
            set { _Id_Recurso = value; }
        }
        
    }
}
