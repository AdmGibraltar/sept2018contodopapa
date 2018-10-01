using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class RepExcesos
    {
        private int _Salida;
        public int Salida
        {
            get { return _Salida; }
            set { _Salida = value; }
        }

        private int _Rota;
        public int Rota
        {
            get { return _Rota; }
            set { _Rota = value; }
        }

        private int _Indicador;
        public int Indicador
        {
            get { return _Indicador; }
            set { _Indicador = value; }
        }

        private int _Proveedor;
        public int Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }
        
        private int _Centro;
        public int Centro
        {
            get { return _Centro; }
            set { _Centro = value; }
        }

        private int _Dias;
        public int Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }

        private int _Tproducto;
        public int Tproducto
        {
            get { return _Tproducto; }
            set { _Tproducto = value; }
        }

        private int _Id_U;
        public int Id_U
        {
            get { return _Id_U; }
            set { _Id_U = value; }
        }

        private int _Id_Cd;
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }

        private int _Id_Emp;
        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }


        private int _Id_Pvd;
        public int Id_Pvd
        {
            get { return _Id_Pvd; }
            set { _Id_Pvd = value; }
        }

        private string _Pvd_Nombre;
        public string Pvd_Nombre
        {
            get { return _Pvd_Nombre; }
            set { _Pvd_Nombre = value; }
        }

        private double _Costo;
        public double Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }

        private int _Exceso;
        public int Exceso
        {
            get { return _Exceso; }
            set { _Exceso = value; }
        }

        private int _Disponible;
        public int Disponible
        {
            get { return _Disponible; }
            set { _Disponible = value; }
        }

        private int _Id_Prd;
        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }

        private string _Prd_Descripcion;
        private int _DiasVer;
        private string _url;
        private int _ProveedorVer;

        public int ProveedorVer
        {
            get { return _ProveedorVer; }
            set { _ProveedorVer = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public int DiasVer
        {
            get { return _DiasVer; }
            set { _DiasVer = value; }
        }
        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }
    }
}
