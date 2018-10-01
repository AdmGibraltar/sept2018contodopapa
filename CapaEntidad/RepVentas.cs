using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CapaEntidad
{
    public class RepVentasParams
    {
        /*"@Id_Emp",
		"@Id_Cd",
		"@Anio",
		"@Cliente",
		"@Tipo",
		"@NivelCliente",
		"@NivelProducto",
		"@Reporte",
		"@Id_U"*/

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

        private int _Anio;
        public int Anio
        {
            get { return _Anio; }
            set { _Anio = value; }
        }

        private string _Cliente;
        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private string _Territorio;
        public string Territorio
        {
            get { return _Territorio; }
            set { _Territorio = value; }
        }

        private string _Producto;
        public string Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private int _Tipo;
        public int Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }

        private int _NivelCliente;
        public int NivelCliente
        {
            get { return _NivelCliente; }
            set { _NivelCliente = value; }
        }

        private int _NivelProducto;
        public int NivelProducto
        {
            get { return _NivelProducto; }
            set { _NivelProducto = value; }
        }

        private int _Reporte;
        public int Reporte
        {
            get { return _Reporte; }
            set { _Reporte = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }
        
    }

    public class RepVentas
    {
        private int _Id_Cd;

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        

        private int _Id_Cte;

        public int Id_Cte
        {
            get { return _Id_Cte; }
            set { _Id_Cte = value; }
        }

        private string _Cte_Nombre;

        public string Cte_NomComercial
        {
            get { return _Cte_Nombre; }
            set { _Cte_Nombre = value; }
        }

        private int _Id_Ter;

        public int Id_Ter
        {
            get { return _Id_Ter; }
            set { _Id_Ter = value; }
        }

        private string _Ter_Nombre;

        public string Ter_Nombre
        {
            get { return _Ter_Nombre; }
            set { _Ter_Nombre = value; }
        }

        private int _Id_Prd;

        public int Id
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Nombre;

        public string Nombre
        {
            get { return _Prd_Nombre; }
            set { _Prd_Nombre = value; }
        }

        private double _mes1;
        public double Mes1
        {
            get { return _mes1; }
            set { _mes1 = value; }
        }

        private double _mes2;
        public double Mes2
        {
            get { return _mes2; }
            set { _mes2 = value; }
        }

        private double _mes3;
        public double Mes3
        {
            get { return _mes3; }
            set { _mes3 = value; }
        }

        private double _mes4;
        public double Mes4
        {
            get { return _mes4; }
            set { _mes4 = value; }
        }

        private double _mes5;
        public double Mes5
        {
            get { return _mes5; }
            set { _mes5 = value; }
        }

        private double _mes6;
        public double Mes6
        {
            get { return _mes6; }
            set { _mes6 = value; }
        }

        private double _mes7;
        public double Mes7
        {
            get { return _mes7; }
            set { _mes7 = value; }
        }

        private double _mes8;
        public double Mes8
        {
            get { return _mes8; }
            set { _mes8 = value; }
        }

        private double _mes9;
        public double Mes9
        {
            get { return _mes9; }
            set { _mes9 = value; }
        }

        private double _mes10;
        public double Mes10
        {
            get { return _mes10; }
            set { _mes10 = value; }
        }

        private double _mes11;
        public double Mes11
        {
            get { return _mes11; }
            set { _mes11 = value; }
        }

        private double _mes12;
        public double Mes12
        {
            get { return _mes12; }
            set { _mes12 = value; }
        }

        private double _Total;
        public double Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        
    }
}
