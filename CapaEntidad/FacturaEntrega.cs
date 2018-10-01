using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaEntrega
    {
        int _Id_Fac;        
        string _Estatus;
        DateTime _Fecha;
        DateTime _Fecha2;
        int _Pedido;
        int _Num_Cliente;
        string _Cliente;
        string _Numero;
        /*FILTRO*/
        string _filtro_Nombre;
        string _filtro_Id_Cte;
        string _filtro_Id_Cte2;
        DateTime? _filtro_FecIni;
        DateTime? _filtro_FecFin;
        public string DbName;

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public int Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }
       
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        public DateTime Fecha2
        {
            get { return _Fecha2; }
            set { _Fecha2 = value; }
        }

        public int Pedido
        {
            get { return _Pedido; }
            set { _Pedido = value; }
        }

        public int Num_Cliente
        {
            get { return _Num_Cliente; }
            set { _Num_Cliente = value; }
        }

        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        public string Filtro_Nombre
        {
            get { return _filtro_Nombre; }
            set { _filtro_Nombre = value; }
        }

        public string Filtro_Id_Cte
        {
            get { return _filtro_Id_Cte; }
            set { _filtro_Id_Cte = value; }
        }

        public string Filtro_Id_Cte2
        {
            get { return _filtro_Id_Cte2; }
            set { _filtro_Id_Cte2 = value; }
        }

        public DateTime? Filtro_FecIni
        {
            get { return _filtro_FecIni; }
            set { _filtro_FecIni = value; }
        }

        public DateTime? Filtro_FecFin
        {
            get { return _filtro_FecFin; }
            set { _filtro_FecFin = value; }
        }
    }
}
