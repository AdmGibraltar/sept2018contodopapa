using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RutaServicio
    {
        int _Id_Cap;
        int _Id_Cliente;
        int _Aparatos;       
        DateTime _Fecha;
        int _Id_Ruta;
        string _Cte_NomComercial;
        string _Rut_Descripcion;
        int _Num_Semana;
        DateTime _Fecha_InicioSemana;
        DateTime _Fecha_FinSemana;
               
        public int Id_Cap
        {
            get { return _Id_Cap; }
            set { _Id_Cap = value; }
        }        

        public int Id_Cliente
        {
            get { return _Id_Cliente; }
            set { _Id_Cliente = value; }
        }

        public int Aparatos
        {
            get { return _Aparatos; }
            set { _Aparatos = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }       

        public int Id_Ruta
        {
            get { return _Id_Ruta; }
            set { _Id_Ruta = value; }
        }

        public string Cte_NomComercial
        {
            get { return _Cte_NomComercial; }
            set { _Cte_NomComercial = value; }
        }

        public string Rut_Descripcion
        {
            get { return _Rut_Descripcion; }
            set { _Rut_Descripcion = value; }
        }

        public int Num_Semana
        {
            get { return _Num_Semana; }
            set { _Num_Semana = value; }
        }

        public DateTime Fecha_InicioSemana
        {
            get { return _Fecha_InicioSemana; }
            set { _Fecha_InicioSemana = value; }
        }

        public DateTime Fecha_FinSemana
        {
            get { return _Fecha_FinSemana; }
            set { _Fecha_FinSemana = value; }
        }  
    }
}
