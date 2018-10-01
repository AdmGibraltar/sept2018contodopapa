using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RemisionesEntrega
    {
        int _Id_Rem;
        string _Tipo;
        string _Estatus;
        DateTime _Fecha;
        DateTime _Fecha2;
        int _Pedido;
        int _Num_Cliente;
        string _Cliente;
        int _Numero;
        /*FILTRO*/
        string _filtro_Nombre;
        string _filtro_Id_Cte;
        string _filtro_Id_Cte2;       
        string _filtro_FecIni;
        string _filtro_FecFin;        

        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public int Id_Rem
        {
            get { return _Id_Rem; }
            set { _Id_Rem = value; }
        }

        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
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

        public string Filtro_FecIni
        {
            get { return _filtro_FecIni; }
            set { _filtro_FecIni = value; }
        }

        public string Filtro_FecFin
        {
            get { return _filtro_FecFin; }
            set { _filtro_FecFin = value; }
        }
    }
}
