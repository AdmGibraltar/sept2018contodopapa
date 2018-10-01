using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Excesos
    {
        private int _Id_Prv;

        public int Id_Prv
        {
            get { return _Id_Prv; }
            set { _Id_Prv = value; }
        }
        private string _Prv_Nombre;

        public string Prv_Nombre
        {
            get { return _Prv_Nombre; }
            set { _Prv_Nombre = value; }
        }
        private double _Costo;

        public double Costo
        {
            get { return _Costo; }
            set { _Costo = value; }
        }
        private double _Cantidad;

        public double Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        private double _Disponible;

        public double Disponible
        {
            get { return _Disponible; }
            set { _Disponible = value; }
        }

        private string _Url;
        private int _Id_Prd;

        public int Id_Prd
        {
            get { return _Id_Prd; }
            set { _Id_Prd = value; }
        }
        private string _Prd_Descripcion;
        private int _OC;

        public int OC
        {
            get { return _OC; }
            set { _OC = value; }
        }

        public string Prd_Descripcion
        {
            get { return _Prd_Descripcion; }
            set { _Prd_Descripcion = value; }
        }

        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }


        private int _Dias;

        public int Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }
        private int _Id_Cd;

        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        private int _Id_Fac;

        public int Id_Fac
        {
            get { return _Id_Fac; }
            set { _Id_Fac = value; }
        }
        private int _Id_Es;

        public int Id_Es
        {
            get { return _Id_Es; }
            set { _Id_Es = value; }
        }
        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }
        private double _CostoUnitario;

        public double CostoUnitario
        {
            get { return _CostoUnitario; }
            set { _CostoUnitario = value; }
        }
    }
}
