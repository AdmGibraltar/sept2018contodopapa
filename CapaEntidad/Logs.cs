using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Logs
    {

        private string _Pantalla;
        private int _Id_Acs;
        private string _Campo;
        private string _Valor_Anterior;
        private string _Valor_Actualizado;
        private DateTime _Fecha;
        private string _Usuario;
        private string _Descripcion;
        private string _Codigo;
        
        public string Pantalla
        {
            get { return _Pantalla; }
            set { _Pantalla = value; }
        }

        public int Id_Acs
        {
            get { return _Id_Acs; }
            set { _Id_Acs = value; }
        }


        public string Campo
        {
            get { return _Campo; }
            set { _Campo = value; }
        }

        public string Valor_Anterior
        {
            get { return _Valor_Anterior; }
            set { _Valor_Anterior = value; }
        }

        public string Valor_Actualizado
        {
            get { return _Valor_Actualizado; }
            set { _Valor_Actualizado = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        
        public string Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
    }
}
