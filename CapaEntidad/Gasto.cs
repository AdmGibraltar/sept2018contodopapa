using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class Gasto
    {
        private int _Id_Emp;
        private int _Id_Cd;
        private int _Año;
        private int _Mes;
        private double _VarFlet;
        private double _VarFletPagado;
        private double _VarFletDevolucion;
        private double _FijGenerales;
        private double _FijAdministracion;
        private double _FijOcupacion;
        private double _FijAlmacen;
        private double _FijServicio;
        private double _FijCobranza;
        private double _UCS;

        public int Id_Emp
        {
            get { return _Id_Emp; }
            set { _Id_Emp = value; }
        }
        public int Id_Cd
        {
            get { return _Id_Cd; }
            set { _Id_Cd = value; }
        }
        public int Año
        {
            get { return _Año; }
            set { _Año = value; }
        }
        public int Mes
        {
            get { return _Mes; }
            set { _Mes = value; }
        }
        public double VarFlet
        {
            get { return _VarFlet; }
            set { _VarFlet = value; }
        }
        public double VarFletPagado
        {
            get { return _VarFletPagado; }
            set { _VarFletPagado = value; }
        }
        public double VarFletDevolucion
        {
            get { return _VarFletDevolucion; }
            set { _VarFletDevolucion = value; }
        }
        public double FijGenerales
        {
            get { return _FijGenerales; }
            set { _FijGenerales = value; }
        }
        public double FijAdministracion
        {
            get { return _FijAdministracion; }
            set { _FijAdministracion = value; }
        }
        public double FijOcupacion
        {
            get { return _FijOcupacion; }
            set { _FijOcupacion = value; }
        }
        public double FijAlmacen
        {
            get { return _FijAlmacen; }
            set { _FijAlmacen = value; }
        }
        public double FijServicio
        {
            get { return _FijServicio; }
            set { _FijServicio = value; }
        }
        public double FijCobranza
        {
            get { return _FijCobranza; }
            set { _FijCobranza = value; }
        }
        public double UCS
        {
            get { return _UCS; }
            set { _UCS = value; }
        }
    }
}
